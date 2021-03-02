using System.Threading.Tasks;

namespace OpenFoodFacts.Application.Service.Interfaces
{
    public interface IJobApplicationService
    {
        Task DownloadData();
    }
}
