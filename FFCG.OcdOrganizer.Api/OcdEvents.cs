using FFCG.OcdOrganizer.Api.DemoStuff;
using Nancy;

namespace FFCG.OcdOrganizer.Api
{
    public class OcdEvents : NancyModule
    {
        public OcdEvents(ILogger logger, IOcdEventsRepository repository) : base("/Events")
        {
            Get["/"] = p =>
            {
                logger.Log(Request.Url);

                return Response.AsJson(repository.Get());
            };

            Get["/{id}"] = p =>
            {
                logger.Log(p.id);
                var id = (int) p.id;

                return Response.AsJson(repository.Get(id));
            };
        }
    }
}
