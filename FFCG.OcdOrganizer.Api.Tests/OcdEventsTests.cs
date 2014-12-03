using System.Collections.Generic;
using System.Linq;
using CsQuery.Utility;
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
        private BrowserResponse _result;
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

            _result = browser.Get("/Events", with => { with.HttpRequest(); with.HostName("localhost"); });
        }

        [Test]
        public void Get_should_return_list_of_events()
        {
            var resultAsEvents = JSON.ParseJSON<List<OcdEvent>>(_result.Body.AsString());
            resultAsEvents.Should().HaveCount(2);
        }

        [Test]
        public void Get_should_logg_request_url()
        {
            _logger.Verify(x => x.Log("http://localhost/Events"));
        }

        [Test]
        public void Get_should_return_status_code_OK()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
    [TestFixture]
    public class OcdEventsTests
    {
        private Mock<IOcdEventsRepository> _cardRepository;
        private List<OcdEvent> _ocdEvents;
        private Browser _browser;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var bootstrapper = new TestableBootStrapper();
            _cardRepository = bootstrapper.RegisterMock<IOcdEventsRepository>();

            _ocdEvents = new List<OcdEvent>
            {
                new OcdEvent {Id = 1, Name = "Foo Bar"},
                new OcdEvent {Id = 2, Name = "Foo Baz"}
            };

            _cardRepository.Setup(x => x.Get(1)).Returns(_ocdEvents.First());
            _cardRepository.Setup(x => x.Get()).Returns(_ocdEvents);
            _browser = new Browser(bootstrapper);
        }

        [Test]
        public void Get_should_return_list_of_events()
        {
            var result = _browser.Get("/Events", with => with.HttpRequest());

            var resultAsCard = JSON.ParseJSON<List<OcdEvent>>(result.Body.AsString());

            resultAsCard.Should().HaveCount(2);
        }
    }
}
