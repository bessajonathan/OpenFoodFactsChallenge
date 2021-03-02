using System.Threading.Tasks;
using OpenFoodFacts.Application.ApiDetails.ViewModels;

namespace OpenFoodFacts.Application.Service.Interfaces
{
    public interface IApiApplicationService
    {
        Task<ApiDetailsViewModel> GetApiDetails();
    }
}
