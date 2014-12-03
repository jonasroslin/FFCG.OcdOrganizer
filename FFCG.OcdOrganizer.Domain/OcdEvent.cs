using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCG.OcdOrganizer.Domain
{
    public class OcdEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Comment { get; set; }
        public List<Participant> Participants { get; set; }

        public OcdEvent()
        {
            Participants = new List<Participant>();
        }
    }

    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }
}
