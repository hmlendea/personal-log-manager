using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace PersonalLogManager.IntegrationTests.Infrastructure
{
    public sealed class PersonalLogApiFixture : IDisposable
    {
        public const string ApiKey = "integration-test-api-key";

        private readonly string dataStorePath = Path.Combine(
            Path.GetTempPath(),
            "PersonalLogManager.IntegrationTests",
            Guid.NewGuid().ToString("N"),
            "logs.json");

        private readonly string logFilePath = Path.Combine(
            Path.GetTempPath(),
            "PersonalLogManager.IntegrationTests",
            Guid.NewGuid().ToString("N"),
            "application.log");

        private readonly WebApplicationFactory<Program> factory;

        public PersonalLogApiFixture()
        {
            factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureAppConfiguration((_, configuration) =>
                    configuration.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["dataStoreSettings:logStorePath"] = dataStorePath,
                        ["securitySettings:apiKey"] = ApiKey,
                        ["nuciLoggerSettings:logFilePath"] = logFilePath,
                        ["nuciLoggerSettings:isFileOutputEnabled"] = "false"
                    }));
            });

            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });
            Client.DefaultRequestHeaders.TryAddWithoutValidation("X-Forwarded-For", "127.0.0.1");
            Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {PersonalLogApiFixture.ApiKey}");
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            factory.Dispose();

            string? directory = Path.GetDirectoryName(dataStorePath);

            if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
        }
    }
}
