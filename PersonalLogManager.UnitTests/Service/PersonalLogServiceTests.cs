using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using NuciDAL.Repositories;
using NuciLog.Core;

using PersonalLogManager.Api.Models;
using PersonalLogManager.DataAccess.DataObjects;
using PersonalLogManager.Service;
using PersonalLogManager.Service.Models;
using PersonalLogManager.Service.TextBuilding;

namespace PersonalLogManager.UnitTests.Service
{
    [TestFixture]
    public class PersonalLogServiceTests
    {
        Mock<IPersonalLogTextBuilderFactory> logTextBuilderFactoryMock;
        Mock<IFileRepository<PersonalLogEntity>> repositoryMock;
        Mock<ILogger> loggerMock;
        PersonalLogService service;

        [SetUp]
        public void SetUp()
        {
            logTextBuilderFactoryMock = new Mock<IPersonalLogTextBuilderFactory>();
            logTextBuilderFactoryMock
                .Setup(factory => factory.BuildLogText(It.IsAny<PersonalLog>(), It.IsAny<string>()))
                .Returns("log text");

            repositoryMock = new Mock<IFileRepository<PersonalLogEntity>>();
            repositoryMock
                .Setup(repository => repository.ContainsId(It.IsAny<string>()))
                .Returns(false);

            loggerMock = new Mock<ILogger>();

            service = new PersonalLogService(
                logTextBuilderFactoryMock.Object,
                repositoryMock.Object,
                loggerMock.Object);
        }

        // ── GetPersonalLogs ────────────────────────────────────

        [Test]
        public void GivenNoFilters_WhenGetPersonalLogs_ThenReturnsAllLogsUpToCount()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05"),
                BuildEntity("L000000002", "2020-03-01"),
                BuildEntity("L000000003", "2021-06-15")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(3));
        }

        [Test]
        public void GivenDateFilter_WhenGetPersonalLogs_ThenReturnsOnlyMatchingLogs()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05"),
                BuildEntity("L000000002", "2020-03-01")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Date = "2012-09-05",
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(1));
            Assert.That(response.Logs[0], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenDateFilterWithRegex_WhenGetPersonalLogs_ThenReturnsMatchingLogs()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05"),
                BuildEntity("L000000002", "2012-11-03"),
                BuildEntity("L000000003", "2021-06-15")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Date = "2012-.*",
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(2));
        }

        [Test]
        public void GivenTimeFilter_WhenGetPersonalLogs_ThenReturnsOnlyMatchingLogs()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05", time: "09:00"),
                BuildEntity("L000000002", "2012-09-05", time: null)
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Time = "09:00",
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(1));
            Assert.That(response.Logs[0], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenTemplateFilter_WhenGetPersonalLogs_ThenReturnsOnlyMatchingLogs()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05", template: "Text"),
                BuildEntity("L000000002", "2012-09-05", template: "AccountActivation")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Template = "Text",
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(1));
            Assert.That(response.Logs[0], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenDataFilter_WhenGetPersonalLogs_ThenReturnsOnlyMatchingLogs()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntityWithData("L000000001", "2012-09-05", new Dictionary<string, string>
                {
                    { "platform", "Nucilandia" }
                }),
                BuildEntityWithData("L000000002", "2012-09-05", new Dictionary<string, string>
                {
                    { "platform", "Astora" }
                })
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Data = new Dictionary<string, string> { { "platform", "Nucilandia" } },
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(1));
            Assert.That(response.Logs[0], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenCountLimit_WhenGetPersonalLogs_ThenReturnsNoMoreThanCount()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05"),
                BuildEntity("L000000002", "2020-03-01"),
                BuildEntity("L000000003", "2021-06-15")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Count = 2
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Has.Count.EqualTo(2));
        }

        [Test]
        public void GivenMultipleLogs_WhenGetPersonalLogs_ThenReturnsLogsOrderedByDateDescending()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05"),
                BuildEntity("L000000002", "2020-03-01"),
                BuildEntity("L000000003", "2021-06-15")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs[0], Does.StartWith("L000000003 "));
            Assert.That(response.Logs[1], Does.StartWith("L000000002 "));
            Assert.That(response.Logs[2], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenLogsWithSameDateAndDifferentTimes_WhenGetPersonalLogs_ThenOrdersByTimeDescending()
        {
            IEnumerable<PersonalLogEntity> entities =
            [
                BuildEntity("L000000001", "2012-09-05", time: "09:00"),
                BuildEntity("L000000002", "2012-09-05", time: "14:00")
            ];

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns(entities);

            GetLogRequest request = new()
            {
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs[0], Does.StartWith("L000000002 "));
            Assert.That(response.Logs[1], Does.StartWith("L000000001 "));
        }

        [Test]
        public void GivenRepositoryException_WhenGetPersonalLogs_ThenRethrowsException()
        {
            InvalidOperationException expectedException = new("Repository failure.");

            repositoryMock
                .Setup(repository => repository.GetAll())
                .Throws(expectedException);

            GetLogRequest request = new() { Count = 10 };

            Assert.That(
                () => service.GetPersonalLogs(request),
                Throws.InstanceOf<InvalidOperationException>()
                      .With.Message.EqualTo("Repository failure."));
        }

        [Test]
        public void GivenNoMatchingLogs_WhenGetPersonalLogs_ThenReturnsEmptyList()
        {
            repositoryMock
                .Setup(repository => repository.GetAll())
                .Returns([]);

            GetLogRequest request = new()
            {
                Date = "2099-01-01",
                Count = 10
            };

            GetLogResponse response = service.GetPersonalLogs(request);

            Assert.That(response.Logs, Is.Empty);
        }

        // ── StorePersonalLog ───────────────────────────────────

        [Test]
        public void GivenValidRequest_WhenStorePersonalLog_ThenAddsEntityToRepository()
        {
            StoreLogRequest request = new()
            {
                Date = "2012-09-05",
                Time = "09:00",
                TimeZone = "UTC",
                Template = "Text",
                Data = new Dictionary<string, string> { { "text", "Hello world" } }
            };

            service.StorePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.Add(It.Is<PersonalLogEntity>(entity =>
                    string.Equals(entity.Date, "2012-09-05") &&
                    string.Equals(entity.Time, "09:00") &&
                    string.Equals(entity.TimeZone, "UTC") &&
                    string.Equals(entity.Template, "Text"))),
                Times.Once());
        }

        [Test]
        public void GivenValidRequest_WhenStorePersonalLog_ThenSavesChangesToRepository()
        {
            StoreLogRequest request = new()
            {
                Date = "2012-09-05",
                Template = "Text"
            };

            service.StorePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.SaveChanges(),
                Times.Once());
        }

        [Test]
        public void GivenValidRequest_WhenStorePersonalLog_ThenGeneratesIdStartingWithL()
        {
            PersonalLogEntity addedEntity = null;

            repositoryMock
                .Setup(repository => repository.Add(It.IsAny<PersonalLogEntity>()))
                .Callback<PersonalLogEntity>(entity => addedEntity = entity);

            StoreLogRequest request = new()
            {
                Date = "2012-09-05",
                Template = "Text"
            };

            service.StorePersonalLog(request);

            Assert.That(addedEntity.Id, Does.StartWith("L"));
            Assert.That(addedEntity.Id, Has.Length.EqualTo(10));
        }

        [Test]
        public void GivenIdCollision_WhenStorePersonalLog_ThenRetriesUntilUniqueId()
        {
            int callCount = 0;

            repositoryMock
                .Setup(repository => repository.ContainsId(It.IsAny<string>()))
                .Returns(() => callCount++ < 2);

            StoreLogRequest request = new()
            {
                Date = "2012-09-05",
                Template = "Text"
            };

            service.StorePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.ContainsId(It.IsAny<string>()),
                Times.Exactly(3));
        }

        [Test]
        public void GivenRepositoryAddThrows_WhenStorePersonalLog_ThenRethrowsException()
        {
            InvalidOperationException expectedException = new("Add failure.");

            repositoryMock
                .Setup(repository => repository.Add(It.IsAny<PersonalLogEntity>()))
                .Throws(expectedException);

            StoreLogRequest request = new()
            {
                Date = "2012-09-05",
                Template = "Text"
            };

            Assert.That(
                () => service.StorePersonalLog(request),
                Throws.InstanceOf<InvalidOperationException>()
                      .With.Message.EqualTo("Add failure."));
        }

        // ── UpdatePersonalLog ──────────────────────────────────

        [Test]
        public void GivenValidRequest_WhenUpdatePersonalLog_ThenUpdatesEntityAndSaves()
        {
            PersonalLogEntity existingEntity = BuildEntity("L000000001", "2012-09-05");

            repositoryMock
                .Setup(repository => repository.Get("L000000001"))
                .Returns(existingEntity);

            UpdateLogRequest request = new()
            {
                Identifier = "L000000001",
                Date = "2020-03-01",
                Time = "14:00",
                TimeZone = "EET",
                Template = "AccountActivation"
            };

            service.UpdatePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.Update(It.Is<PersonalLogEntity>(entity =>
                    string.Equals(entity.Id, "L000000001") &&
                    string.Equals(entity.Date, "2020-03-01") &&
                    string.Equals(entity.Time, "14:00") &&
                    string.Equals(entity.TimeZone, "EET") &&
                    string.Equals(entity.Template, "AccountActivation"))),
                Times.Once());

            repositoryMock.Verify(
                repository => repository.SaveChanges(),
                Times.Once());
        }

        [Test]
        public void GivenPartialRequest_WhenUpdatePersonalLog_ThenOnlyUpdatesProvidedFields()
        {
            PersonalLogEntity existingEntity = BuildEntity("L000000001", "2012-09-05");
            existingEntity.Time = "09:00";
            existingEntity.TimeZone = "UTC";

            repositoryMock
                .Setup(repository => repository.Get("L000000001"))
                .Returns(existingEntity);

            UpdateLogRequest request = new()
            {
                Identifier = "L000000001",
                Date = "2020-03-01"
            };

            service.UpdatePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.Update(It.Is<PersonalLogEntity>(entity =>
                    string.Equals(entity.Date, "2020-03-01") &&
                    string.Equals(entity.Time, "09:00") &&
                    string.Equals(entity.TimeZone, "UTC"))),
                Times.Once());
        }

        [Test]
        public void GivenRepositoryException_WhenUpdatePersonalLog_ThenRethrowsException()
        {
            InvalidOperationException expectedException = new("Get failure.");

            repositoryMock
                .Setup(repository => repository.Get(It.IsAny<string>()))
                .Throws(expectedException);

            UpdateLogRequest request = new()
            {
                Identifier = "L000000001",
                Date = "2020-03-01"
            };

            Assert.That(
                () => service.UpdatePersonalLog(request),
                Throws.InstanceOf<InvalidOperationException>()
                      .With.Message.EqualTo("Get failure."));
        }

        // ── DeletePersonalLog ──────────────────────────────────

        [Test]
        public void GivenValidRequest_WhenDeletePersonalLog_ThenRemovesEntityFromRepository()
        {
            DeleteLogRequest request = new()
            {
                Identifier = "L000000001"
            };

            service.DeletePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.Remove("L000000001"),
                Times.Once());
        }

        [Test]
        public void GivenValidRequest_WhenDeletePersonalLog_ThenSavesChangesToRepository()
        {
            DeleteLogRequest request = new()
            {
                Identifier = "L000000001"
            };

            service.DeletePersonalLog(request);

            repositoryMock.Verify(
                repository => repository.SaveChanges(),
                Times.Once());
        }

        [Test]
        public void GivenRepositoryException_WhenDeletePersonalLog_ThenRethrowsException()
        {
            InvalidOperationException expectedException = new("Remove failure.");

            repositoryMock
                .Setup(repository => repository.Remove(It.IsAny<string>()))
                .Throws(expectedException);

            DeleteLogRequest request = new()
            {
                Identifier = "L000000001"
            };

            Assert.That(
                () => service.DeletePersonalLog(request),
                Throws.InstanceOf<InvalidOperationException>()
                      .With.Message.EqualTo("Remove failure."));
        }

        private static PersonalLogEntity BuildEntity(
            string id,
            string date,
            string time = null,
            string template = "Text")
        {
            return new()
            {
                Id = id,
                Date = date,
                Time = time,
                TimeZone = "UTC",
                Template = template,
                CreatedDT = "2012-09-05T00:00:00Z",
                Data = []
            };
        }

        private static PersonalLogEntity BuildEntityWithData(
            string id,
            string date,
            Dictionary<string, string> data)
        {
            return new()
            {
                Id = id,
                Date = date,
                Time = null,
                TimeZone = "UTC",
                Template = "Text",
                CreatedDT = "2012-09-05T00:00:00Z",
                Data = data
            };
        }
    }
}
