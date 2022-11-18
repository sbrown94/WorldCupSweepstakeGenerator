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

            if (!Validate(results))
                throw new Exception("fucked up");

            WriteToFile(results);

            Print(results);

            //write
            Console.ReadLine();
        }

        private void WriteToFile(List<PlayerTeamAssignment> results)
        {
            var str = new StringBuilder();

            foreach(var result in results)
            {
                str.AppendLine($"{result.PlayerName}: {result.Teams[0]}, {result.Teams[1]}");
            }

            System.IO.File.WriteAllText("output.txt", str.ToString());
        }

        private bool Validate(List<PlayerTeamAssignment> results)
        {
            var countedTeams = new List<string>();

            foreach(var result in results)
            {
                foreach(var team in result.Teams)
                {
                    if (countedTeams.Contains(team))
                        return false;

                    else
                        countedTeams.Add(team);
                }
            }

            return true;
        }

        private List<PlayerTeamAssignment> AssignTeams(OddsBuckets buckets)
        {
            var results = new List<PlayerTeamAssignment>();

            var rand = new Random(Guid.NewGuid().GetHashCode());

            foreach(var player in _settings.Names)
            {
                var highTierTeam = buckets.HighTier.ElementAt(rand.Next(buckets.HighTier.Count));
                var lowTierTeam = buckets.LowTier.ElementAt(rand.Next(buckets.LowTier.Count));

                buckets.HighTier.Remove(highTierTeam);
                buckets.LowTier.Remove(lowTierTeam);

                results.Add(
                    new PlayerTeamAssignment
                    {
                        PlayerName = player,
                        Teams = new List<string> { highTierTeam.Name, lowTierTeam.Name }
                    });
            }

            return results;
        }

        private void Print(List<PlayerTeamAssignment> results)
        {
            PrintXLines(100);


            Console.WriteLine("~~~~  F O O T B A L L ~~~~");
            Console.WriteLine("WELCOME! FOOTBALL SWEEPSTAKE 2022!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ReadLine();
            Console.WriteLine("Lets FOOTBALL");
            PrintXLines(5);

            Console.ReadLine();

            var index = 1;

            foreach(var result in results)
            {
                Console.WriteLine($"PLAYER number {index}: [{result.PlayerName}]");
                Console.ReadLine();
                Console.WriteLine("YOU GET !!");
                Console.WriteLine($"TEAM 1: [{result.Teams.First()}]");
                Console.WriteLine($"TEAM 2: [{result.Teams.Last()}]");
                Console.WriteLine("WOWWWW!!! CONGRATULATION");
                Console.ReadLine();
                PrintXLines(3);
                Console.WriteLine("NEXT PLAYER:");
                index++;
            }

            Console.WriteLine("that's the FOOTBALL ~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("GOOD LUCK!");
            Console.WriteLine("Program made by Scott age 28");
            Console.ReadLine();


        }

        private void PrintXLines(int x)
        {
            for(int i = 0; i < x;i ++)
            {
                Console.WriteLine();
            }
        }
    }
}
