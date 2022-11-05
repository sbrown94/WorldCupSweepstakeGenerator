using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async void Calculate()
        {
            var buckets = await _oddsService.GetCurrentOddsBuckets();


        }

        private void Split
    }
}
