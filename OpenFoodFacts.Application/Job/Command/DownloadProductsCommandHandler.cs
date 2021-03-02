using MediatR;
using OpenFoodFacts.Application.Service.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Job.Command
{
    public class DownloadProductsCommandHandler : IRequestHandler<DownloadProductsCommand,Unit>
    {
        private readonly IJobApplicationService _jobApplicationService;

        public DownloadProductsCommandHandler(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }
        public async Task<Unit> Handle(DownloadProductsCommand request, CancellationToken cancellationToken)
        {
            await _jobApplicationService.DownloadData();

            return Unit.Value;
        }
    }
}
