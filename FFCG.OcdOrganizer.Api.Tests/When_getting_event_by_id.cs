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
    public class When_getting_event_by_id
    {
        private BrowserResponse _response;
        private Mock<ILogger> _logger;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var bootStrapper = new TestableBootStrapper();
            var repository = bootStrapper.RegisterMock<IOcdEventsRepository>();
            _logger = bootStrapper.RegisterMock<ILogger>();

            var browser = new Browser(bootStrapper);

            repository.Setup(x => x.Get(1)).Returns(new OcdEvent {Id = 1});

            _response = browser.Get("/Events/1", with => with.HttpRequest());
        }

        [Test]
        public void Should_return_event()
        {
            var ocdEvent = JSON.ParseJSON<OcdEvent>(_response.Body.AsString());
            ocdEvent.Id.Should().Be(1);
        }

        [Test]
        public void Should_return_status_code_OK()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void Should_log_request_url()
        {
            _logger.Verify(x => x.Log("1"));
        }
    }
}