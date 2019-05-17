using System;
using System.Collections.Generic;
using System.Text;

namespace HackathonCardGenerator.Models
{
    public class Participant
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TeamName { get; set; }
        public ParticipantType Type { get; set; }
    }
}
