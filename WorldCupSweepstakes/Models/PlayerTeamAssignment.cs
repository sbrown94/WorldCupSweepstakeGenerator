using System.Collections.Generic;

namespace WorldCupSweepstakes.Models
{
    public class PlayerTeamAssignment
    {
        public string PlayerName { get; set; }
        public List<string> Teams { get; set; }
    }
}
