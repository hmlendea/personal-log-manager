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
    public class PersonalLogControllerIntegrationTests
    {
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
        public async Task GivenValidLog_WhenCreatingQueryingRetrievingAndDeleting_ThenTheCompleteLifecycleSucceeds()
        {
            HttpResponseMessage createResponse = await fixture.Client.PostAsJsonAsync(
                "/PersonalLog",
                new
                {
                    apiKey = PersonalLogApiFixture.ApiKey,
                    date = "2026-09-19",
                    time = "14:30",
                    timeZone = "Europe/Bucharest",
                    template = "WaterDrinking",
                    data = new Dictionary<string, string>
                    {
                        ["amount"] = "300",
                        ["amount_currency"] = "ml"
                    }
                });

            string createResponseContent = await createResponse.Content.ReadAsStringAsync();
            Assert.That(
                createResponse.StatusCode,
                Is.EqualTo(HttpStatusCode.OK),
                createResponseContent);

            HttpResponseMessage queryResponse = await fixture.Client.GetAsync(
                "/PersonalLog?date=2026-09-19&count=10");

            Assert.That(queryResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            JsonDocument queryDocument = (await queryResponse.Content.ReadFromJsonAsync<JsonDocument>())!;
            string logText = queryDocument.RootElement.GetProperty("logs")[0].GetString()!;
            string identifier = logText[..10];

            HttpResponseMessage retrieveResponse = await fixture.Client.GetAsync(
                $"/PersonalLog/{identifier}");

            Assert.That(retrieveResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            JsonDocument retrieveDocument = (await retrieveResponse.Content.ReadFromJsonAsync<JsonDocument>())!;
            Assert.That(retrieveDocument.RootElement.GetProperty("id").GetString(), Is.EqualTo(identifier));

            HttpResponseMessage deleteResponse = await fixture.Client.DeleteAsync(
                $"/PersonalLog/{identifier}");

            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
