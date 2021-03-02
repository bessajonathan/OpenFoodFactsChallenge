using OpenFoodFacts.Application.ApiDetails.ViewModels;
using OpenFoodFacts.Application.Service.Interfaces;
using OpenFoodFacts.Domain.Enums;
using OpenFoodFacts.Domain.Service.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Service
{
    public class ApiApplicationService : IApiApplicationService
    {
        private readonly IOpenFoodService _openFoodService;
        private readonly IJobService _jobService;

        public ApiApplicationService(IOpenFoodService openFoodService,IJobService jobService)
        {
            _openFoodService = openFoodService;
            _jobService = jobService;
        }
        public async Task<ApiDetailsViewModel> GetApiDetails()
        {
            DateTime? lastCronExecution = _jobService.GetLasCronExecution();

            var writeStatus = await _openFoodService.VerifyWriteStatus() ? EDatabaseStatus.Ok : EDatabaseStatus.Failed;
            var readerStatus = await _openFoodService.VerifyReaderStatus() ? EDatabaseStatus.Ok : EDatabaseStatus.Failed;
            var process = Process.GetCurrentProcess();

            return new ApiDetailsViewModel(writeStatus,readerStatus,lastCronExecution, Environment.TickCount, process.PrivateMemorySize64);
        }
    }
}
