using System.Collections.Generic;
using CsQuery.Utility;
using FFCG.OcdOrganizer.Api.DemoStuff;
using FFCG.OcdOrganizer.Domain;
using FluentAssertions;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using Browser = Nancy.Testing.Browser;

namespace FFCG.OcdOrganizer.Api.Tests
{
    [TestFixture]
    public class When_getting_events
    {
        private BrowserResponse _response;
        private Mock<ILogger> _logger;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var bootstrapper = new TestableBootStrapper();
            var repository = bootstrapper.RegisterMock<IOcdEventsRepository>();
            _logger = bootstrapper.RegisterMock<ILogger>();

            var browser = new Browser(bootstrapper);

            var ocdEvents = new List<OcdEvent>
            {
                new OcdEvent {Id = 1, Name = "Foo Bar"},
                new OcdEvent {Id = 2, Name = "Foo Baz"}
            };
            
            repository.Setup(x => x.Get()).Returns(ocdEvents);

            _response = browser.Get("/Events", with => { with.HttpRequest(); with.HostName("localhost"); });
        }

        [Test]
        public void Should_return_list_of_events()
        {
            var resultAsEvents = JSON.ParseJSON<List<OcdEvent>>(_response.Body.AsString());
            resultAsEvents.Should().HaveCount(2);
        }

        [Test]
        public void Should_log_request_url()
        {
            _logger.Verify(x => x.Log("http://localhost/Events"));
        }

        [Test]
        public void Should_return_status_code_OK()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
