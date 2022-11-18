using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WorldCupSweepstakes.Models;
using WorldCupSweepstakes.Models.OddsApi;
using WorldCupSweepstakes.Settings;

namespace WorldCupSweepstakes.Services
{
    public class OddsService : IOddsService
    {
        public readonly OddsApiSettings _settings;

        public OddsService(OddsApiSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<OddsBuckets> GetCurrentOddsBuckets()
        {
            var data = await GetApiResponse();

            var teamList = data.Bookmakers.FirstOrDefault(x => x.Key == _settings.ChosenBookmaker)?.Markets.FirstOrDefault(y => y.Key == _settings.MarketType)?.Outcomes
                .Select(z => new Team { Name = z.Name, Odds = z.Price }).OrderBy(p => p.Odds);



            var teamsCount = teamList.Count() / 2;

            return new OddsBuckets
            {
                HighTier = teamList.Take(teamsCount)?.ToList(),
                LowTier = teamList.TakeLast(teamsCount)?.ToList()
            };
        }

        private async Task<OddsApiResponse> GetApiResponse()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(_settings.UrlPath);

            var urlParams = $"v4/sports/soccer_fifa_world_cup_winner/odds/?regions=uk&apiKey={_settings.ApiKey}";

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(urlParams);

            var result = await response.Content.ReadAsAsync<OddsApiResponse[]>();

            client.Dispose();

            return result.First();
        }
    }
}
