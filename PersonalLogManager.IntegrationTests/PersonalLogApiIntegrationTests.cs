using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using PersonalLogManager.IntegrationTests.Infrastructure;

namespace PersonalLogManager.IntegrationTests
{
    [TestFixture]
    public class PersonalLogApiIntegrationTests
    {
        private const string FirstDate = "2026-09-18";
        private const string SecondDate = "2026-09-19";
        private const string TextTemplate = "Text";
        private const string WaterDrinkingTemplate = "WaterDrinking";

        private PersonalLogApiFixture fixture = null!;

        [SetUp]
        public void SetUp()
        {
            fixture = new PersonalLogApiFixture();
        }

        [TearDown]
        public void TearDown()
        {
            fixture.Dispose();
        }

        [Test]
        public async Task GivenNoAuthorisationHeader_WhenPostingLog_ThenReturnsUnauthorized()
        {
            fixture.Client.DefaultRequestHeaders.Remove("Authorization");

            using HttpRequestMessage request = new(HttpMethod.Post, "/PersonalLog")
            {
                Content = JsonContent.Create(new { date = FirstDate })
            };

            HttpResponseMessage response = await fixture.Client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task GivenInvalidAuthorisationHeader_WhenGettingLogs_ThenReturnsUnauthorized()
        {
            using HttpRequestMessage request = new(HttpMethod.Get, "/PersonalLog?count=1");
            request.Headers.Authorization = new("Bearer", "invalid-api-key");

            HttpResponseMessage response = await fixture.Client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task GivenApiKeyWithoutBearerScheme_WhenGettingLogs_ThenReturnsSuccess()
        {
            fixture.Client.DefaultRequestHeaders.Remove("Authorization");

            using HttpRequestMessage request = new(HttpMethod.Get, "/PersonalLog?count=1");
            request.Headers.TryAddWithoutValidation("Authorization", PersonalLogApiFixture.ApiKey);

            HttpResponseMessage response = await fixture.Client.SendAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GivenMissingDate_WhenPostingLog_ThenReturnsBadRequest()
        {
            HttpResponseMessage response = await fixture.Client.PostAsJsonAsync(
                "/PersonalLog",
                new { template = TextTemplate });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(100001)]
        public async Task GivenCountOutsideThePermittedRange_WhenGettingLogs_ThenReturnsBadRequest(int count)
        {
            HttpResponseMessage response = await fixture.Client.GetAsync($"/PersonalLog?count={count}");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task GivenNoLogs_WhenGettingLogs_ThenReturnsAnEmptySuccessResponse()
        {
            JsonDocument response = await GetJsonAsync("/PersonalLog?count=10");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(0));
            Assert.That(response.RootElement.GetProperty("count").GetInt32(), Is.EqualTo(0));
        }

        [Test]
        public async Task GivenOptionalFieldsAreOmitted_WhenCreatingAndRetrievingLog_ThenDefaultsArePersisted()
        {
            string identifier = await CreateLogAsync(FirstDate, template: TextTemplate);

            JsonDocument response = await GetJsonAsync($"/PersonalLog/{identifier}");
            JsonElement log = response.RootElement;

            Assert.That(log.GetProperty("date").GetString(), Is.EqualTo(FirstDate));
            Assert.That(log.GetProperty("time").ValueKind, Is.EqualTo(JsonValueKind.Null));
            Assert.That(log.GetProperty("timeZone").ValueKind, Is.EqualTo(JsonValueKind.Null));
            Assert.That(log.GetProperty("template").GetString(), Is.EqualTo(TextTemplate));
            Assert.That(log.GetProperty("data").EnumerateObject(), Is.Empty);
        }

        [Test]
        public async Task GivenAStoredLog_WhenQueryingWithDefaultCount_ThenReturnsOneLog()
        {
            await CreateLogAsync(FirstDate, template: TextTemplate);
            await CreateLogAsync(SecondDate, template: TextTemplate);

            JsonDocument response = await GetJsonAsync("/PersonalLog");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(1));
            Assert.That(response.RootElement.GetProperty("count").GetInt32(), Is.EqualTo(1));
        }

        [Test]
        public async Task GivenLogsWithDifferentDates_WhenFilteringByDateRegex_ThenReturnsOnlyMatchingLogs()
        {
            await CreateLogAsync(FirstDate, template: TextTemplate);
            await CreateLogAsync(SecondDate, template: TextTemplate);

            JsonDocument response = await GetJsonAsync("/PersonalLog?date=2026-09-1.&count=10");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(2));
        }

        [Test]
        public async Task GivenARegexWithoutExplicitAnchors_WhenFilteringByDate_ThenMatchesTheWholeField()
        {
            await CreateLogAsync(FirstDate, template: TextTemplate);

            JsonDocument response = await GetJsonAsync("/PersonalLog?date=2026-09&count=10");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(0));
        }

        [Test]
        public async Task GivenDifferentTimes_WhenFilteringByTime_ThenReturnsOnlyTheMatchingTime()
        {
            await CreateLogAsync(FirstDate, "09:15", template: TextTemplate);
            await CreateLogAsync(FirstDate, "14:30", template: TextTemplate);

            JsonDocument response = await GetJsonAsync("/PersonalLog?time=09:15&count=10");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(1));
        }

        [Test]
        public async Task GivenDifferentTemplates_WhenFilteringByTemplate_ThenReturnsOnlyTheMatchingTemplate()
        {
            await CreateLogAsync(FirstDate, template: TextTemplate);
            await CreateLogAsync(SecondDate, template: WaterDrinkingTemplate);

            JsonDocument response = await GetJsonAsync($"/PersonalLog?template={WaterDrinkingTemplate}&count=10");

            Assert.That(response.RootElement.GetProperty("logs").GetArrayLength(), Is.EqualTo(1));
        }

        [Test]
        public async Task GivenMultipleLogs_WhenLimitingCount_ThenReturnsTheNewestLogsFirst()
        {
            await CreateLogAsync(FirstDate, template: TextTemplate);
            await CreateLogAsync(SecondDate, template: TextTemplate);

            JsonDocument response = await GetJsonAsync("/PersonalLog?count=1");
            string logText = response.RootElement.GetProperty("logs")[0].GetString()!;

            Assert.That(logText, Does.Contain(SecondDate));
        }

        [Test]
        public async Task GivenRomanianLocalisation_WhenQueryingLogs_ThenReturnsRomanianText()
        {
            await CreateLogAsync(
                SecondDate,
                template: WaterDrinkingTemplate,
                data: new Dictionary<string, string>
                {
                    ["amount"] = "300",
                    ["amount_currency"] = "ml"
                });

            JsonDocument response = await GetJsonAsync("/PersonalLog?localisation=ro-RO&count=10");
            string logText = response.RootElement.GetProperty("logs")[0].GetString()!;

            Assert.That(logText, Does.Contain("Am băut"));
        }

        [Test]
        public async Task GivenAStoredLog_WhenUpdatingSelectedFields_ThenPreservesOmittedFieldsAndMergesData()
        {
            string identifier = await CreateLogAsync(
                FirstDate,
                "09:15",
                "Europe/Bucharest",
                WaterDrinkingTemplate,
                new Dictionary<string, string>
                {
                    ["amount"] = "300",
                    ["amount_currency"] = "ml"
                });

            HttpResponseMessage updateResponse = await fixture.Client.PutAsJsonAsync(
                $"/PersonalLog/{identifier}",
                new
                {
                    date = SecondDate,
                    data = new Dictionary<string, string>
                    {
                        ["amount"] = "350",
                        ["note"] = "hydration"
                    }
                });

            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            JsonDocument response = await GetJsonAsync($"/PersonalLog/{identifier}");
            JsonElement log = response.RootElement;

            Assert.That(log.GetProperty("date").GetString(), Is.EqualTo(SecondDate));
            Assert.That(log.GetProperty("time").GetString(), Is.EqualTo("09:15"));
            Assert.That(log.GetProperty("timeZone").GetString(), Is.EqualTo("Europe/Bucharest"));
            Assert.That(log.GetProperty("data").GetProperty("amount").GetString(), Is.EqualTo("350"));
            Assert.That(log.GetProperty("data").GetProperty("amount_currency").GetString(), Is.EqualTo("ml"));
            Assert.That(log.GetProperty("data").GetProperty("note").GetString(), Is.EqualTo("hydration"));
            Assert.That(log.GetProperty("updatedDateTime").ValueKind, Is.Not.EqualTo(JsonValueKind.Null));
        }

        [Test]
        public async Task GivenRouteIdentifierAndConflictingBodyIdentifier_WhenUpdatingLog_ThenUsesTheRouteIdentifier()
        {
            string routeIdentifier = await CreateLogAsync(FirstDate, template: TextTemplate);
            string otherIdentifier = await CreateLogAsync(SecondDate, template: TextTemplate);

            HttpResponseMessage updateResponse = await fixture.Client.PutAsJsonAsync(
                $"/PersonalLog/{routeIdentifier}",
                new
                {
                    identifier = otherIdentifier,
                    date = SecondDate
                });

            Assert.That(updateResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            JsonDocument routeLog = await GetJsonAsync($"/PersonalLog/{routeIdentifier}");
            JsonDocument otherLog = await GetJsonAsync($"/PersonalLog/{otherIdentifier}");

            Assert.That(routeLog.RootElement.GetProperty("date").GetString(), Is.EqualTo(SecondDate));
            Assert.That(otherLog.RootElement.GetProperty("date").GetString(), Is.EqualTo(SecondDate));
        }

        [Test]
        public async Task GivenAnUnknownIdentifier_WhenRetrievingLog_ThenReturnsNotFoundOrServerError()
        {
            HttpResponseMessage response = await fixture.Client.GetAsync("/PersonalLog/L000000000");

            Assert.That(response.StatusCode, Is.AnyOf(HttpStatusCode.NotFound, HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task GivenAnUnknownIdentifier_WhenDeletingLog_ThenReturnsNotFoundOrServerError()
        {
            HttpResponseMessage response = await fixture.Client.DeleteAsync("/PersonalLog/L000000000");

            Assert.That(response.StatusCode, Is.AnyOf(HttpStatusCode.NotFound, HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task GivenAnUnknownIdentifier_WhenUpdatingLog_ThenReturnsNotFoundOrServerError()
        {
            HttpResponseMessage response = await fixture.Client.PutAsJsonAsync(
                "/PersonalLog/L000000000",
                new { date = SecondDate });

            Assert.That(response.StatusCode, Is.AnyOf(HttpStatusCode.NotFound, HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task GivenUnsupportedTemplate_WhenQueryingLogs_ThenReturnsServerError()
        {
            HttpResponseMessage createResponse = await fixture.Client.PostAsJsonAsync(
                "/PersonalLog",
                new
                {
                    date = FirstDate,
                    template = "UnsupportedTemplate"
                });

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            HttpResponseMessage response = await fixture.Client.GetAsync("/PersonalLog?count=10");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        private async Task<string> CreateLogAsync(
            string date,
            string? time = null,
            string? timeZone = null,
            string? template = null,
            Dictionary<string, string>? data = null)
        {
            HttpResponseMessage createResponse = await fixture.Client.PostAsJsonAsync(
                "/PersonalLog",
                new
                {
                    date,
                    time,
                    timeZone,
                    template,
                    data
                });

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            JsonDocument response = await GetJsonAsync($"/PersonalLog?date={date}&count=100");
            string logText = response.RootElement.GetProperty("logs")[0].GetString()!;

            return logText[..10];
        }

        private async Task<JsonDocument> GetJsonAsync(string path)
        {
            HttpResponseMessage response = await fixture.Client.GetAsync(path);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            return (await response.Content.ReadFromJsonAsync<JsonDocument>())!;
        }
    }
}
