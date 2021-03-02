using MediatR;
using OpenFoodFacts.Application.ApiDetails.ViewModels;

namespace OpenFoodFacts.Application.ApiDetails.Queries
{
    public class ApiDetailsQuery:IRequest<ApiDetailsViewModel>
    {
    }
}
