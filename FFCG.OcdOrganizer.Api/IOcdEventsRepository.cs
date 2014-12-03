using System.Collections.Generic;
using FFCG.OcdOrganizer.Domain;

namespace FFCG.OcdOrganizer.Api
{
    public interface IOcdEventsRepository
    {
        OcdEvent Get(int id);
        List<OcdEvent> Get();
    }
}