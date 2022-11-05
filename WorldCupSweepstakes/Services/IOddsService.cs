using System.Threading.Tasks;
using WorldCupSweepstakes.Models;

namespace WorldCupSweepstakes.Services
{
    public interface IOddsService
    {
        Task<OddsBuckets> GetCurrentOddsBuckets();
    }
}
