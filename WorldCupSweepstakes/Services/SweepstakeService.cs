using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldCupSweepstakes.Models;
using WorldCupSweepstakes.Settings;

namespace WorldCupSweepstakes.Services
{
    public class SweepstakeService : ISweepstakeService
    {
        public readonly IOddsService _oddsService;
        public readonly PlayersSettings _settings;

        public SweepstakeService(IOddsService oddsService, PlayersSettings settings)
        {
            _oddsService = oddsService ?? throw new ArgumentNullException(nameof(oddsService));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task StartAsync()
        {
            var buckets = await _oddsService.GetCurrentOddsBuckets();

            var results = AssignTeams(buckets);

            //write
            Console.ReadLine();
        }

        private List<PlayerTeamAssignment> AssignTeams(OddsBuckets buckets)
        {
            var results = new List<PlayerTeamAssignment>();

            var rand = new Random(Guid.NewGuid().GetHashCode());

            foreach(var player in _settings.Players)
            {
                var highTierTeam = buckets.HighTier.ElementAt(rand.Next(buckets.HighTier.Count-1));
                var lowTierTeam = buckets.LowTier.ElementAt(rand.Next(buckets.LowTier.Count - 1));

                results.Add(
                    new PlayerTeamAssignment
                    {
                        PlayerName = player,
                        Teams = new List<string> { highTierTeam.Name, lowTierTeam.Name }
                    });
            }

            return results;
        }
    }
}
