using System;
using System.Collections.Generic;
using System.Linq;
using FFCG.OcdOrganizer.Domain;

namespace FFCG.OcdOrganizer.Api.DemoStuff
{
    public class OcdEventInMemory : IOcdEventsRepository
    {
        private readonly List<OcdEvent> _ocdEvents;

        public OcdEventInMemory()
        {
            _ocdEvents = new List<OcdEvent>();

            var participants = new List<Participant>
            {
                new Participant {Id = 1, Comment = "Jag kommer!", Name = "Jonas Roslin"},
                new Participant {Id = 2, Comment = "Blir några minuter sen", Name = "David Hedman"}
            };

            _ocdEvents = new List<OcdEvent>
            {
                new OcdEvent
                {
                    Id = 1,
                    Name = "Årets första",
                    Start = new DateTime(2015, 01, 21).AddHours(17),
                    End = new DateTime(2015, 01, 21).AddHours(21),
                    Comment = "Vi kör en klassisk kodkväll på nya kontoret",
                    Participants = participants
                },
                new OcdEvent
                {
                    Id = 1,
                    Name = "Vårhack",
                    Start = new DateTime(2015, 04, 26).AddHours(17),
                    End = new DateTime(2015, 04, 26).AddHours(21),
                    Comment = "Vi kör en klassisk kodkväll på nya kontoret",
                    Participants = participants
                }
            };
        }

        public OcdEvent Get(int id)
        {
            return _ocdEvents.Single(x => x.Id == id);
        }

        public List<OcdEvent> Get()
        {
            return _ocdEvents;
        }
    }
}