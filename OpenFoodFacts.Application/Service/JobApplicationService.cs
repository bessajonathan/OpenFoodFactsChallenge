using OpenFoodFacts.Application.Service.Interfaces;
using OpenFoodFacts.Domain.Service.Interfaces;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Service
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobService _jobService;

        public JobApplicationService(IJobService jobService)
        {
            _jobService = jobService;
        }
        public async Task DownloadData()
        {
            await _jobService.DownloadData();
        }
    }
}
