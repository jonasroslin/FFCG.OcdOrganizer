using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace FFCG.OcdOrganizer.Api
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class DefaultLogger : ILogger
    {
        public void Log(string message)
        {
            
        }
    }

    public class OcdEvents : NancyModule
    {
        public OcdEvents(ILogger logger, IOcdEventsRepository repository) : base("/Events")
        {
            Get["/"] = p =>
            {
                var message = Request.Url;
                logger.Log(message);
                return Response.AsJson(repository.Get());
            };
        }
    }
}
