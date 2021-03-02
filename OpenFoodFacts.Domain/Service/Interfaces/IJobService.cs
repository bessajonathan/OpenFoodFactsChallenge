using System;
using System.Threading.Tasks;

namespace OpenFoodFacts.Domain.Service.Interfaces
{
    public interface IJobService
    {
        Task DownloadData();
        DateTime? GetLasCronExecution();
    }
}
