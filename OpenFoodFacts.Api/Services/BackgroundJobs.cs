using Hangfire;
using MediatR;
using OpenFoodFacts.Application.Job.Command;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenFoodFacts.Common.Configurations;
using OpenFoodFacts.Common.Exceptions;

namespace OpenFoodFacts.API.Services
{
    public class BackgroundJobs
    {
        private readonly string CronExpression;

        private readonly IMediator _mediator;

        public BackgroundJobs(IMediator mediator, IConfiguration configuration)
        {
            var settings = configuration.GetSection("OpenFoodSettings").Get<OpenFoodSettings>();

            if (string.IsNullOrEmpty(settings.CronExpression))
                throw new NotFoundException("Expressão Cron não definida");

            CronExpression = settings.CronExpression;
            _mediator = mediator;
        }

        public void StartJobs()
        {
            RecurringJob.AddOrUpdate(() => DownloadProducts(), CronExpression);
        }

        public async Task DownloadProducts()
        {
            await _mediator.Send(new DownloadProductsCommand());
        }
    }
}
