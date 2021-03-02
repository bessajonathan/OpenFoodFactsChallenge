using OpenFoodFacts.Application.Product.ViewModels;
using OpenFoodFacts.Application.Service.Interfaces;
using OpenFoodFacts.Domain.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenFoodFacts.Common.Exceptions;

namespace OpenFoodFacts.Application.Service
{
    public class OpenFoodApplicationService : IOpenFoodApplicationService
    {
        private readonly IOpenFoodService _openFoodService;
        private readonly IMapper _mapper;

        public OpenFoodApplicationService(IOpenFoodService openFoodService, IMapper mapper)
        {
            _openFoodService = openFoodService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var products = await _openFoodService.GetProducts();

            if (products.Count() == 0)
                throw new NotFoundException("Ainda não existem produtos na base.");

            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductViewModel> GetProductByCode(string code)
        {
            var product = await _openFoodService.GetProductByCode(code);

            if (product is null)
                throw new NotFoundException($"Producto com código {code} não encontrado.");

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task ChangeProductStatus(string code)
        {
            var product = await _openFoodService.GetProductByCode(code);

            if (product is null)
                throw new NotFoundException($"Producto com código {code} não encontrado.");

            await _openFoodService.ChangeProductStatus(product);
        }

        public async Task UpdateProduct(ProductViewModel productVm, string code)
        {
            var product = await _openFoodService.GetProductByCode(code);

            if (product is null)
                throw new NotFoundException($"Producto com código {code} não encontrado.");

            product.Status = productVm.Status;
            product.Imported_t = productVm.Imported_t;
            product.Url = productVm.Url;
            product.Creator = productVm.Creator;
            product.Created_t = productVm.Created_t;
            product.Last_modified_t = productVm.Last_modified_t;
            product.Product_name = productVm.Product_name;
            product.Quantity = productVm.Quantity;
            product.Brands = productVm.Brands;
            product.Categories = productVm.Categories;
            product.Labels = productVm.Labels;
            product.Cities = productVm.Cities;
            product.Purchase_places = productVm.Purchase_places;
            product.Stores = productVm.Stores;
            product.Ingredients_Text = productVm.Ingredients_Text;
            product.Traces = productVm.Traces;
            product.Serving_Size = productVm.Serving_Size;
            product.Serving_Quantity = productVm.Serving_Quantity;
            product.Nutriscore_Grade = productVm.Nutriscore_Grade;
            product.Nutriscore_Score = productVm.Nutriscore_Score;
            product.Main_Category = productVm.Main_Category;
            product.Image_Url = productVm.Image_Url;

            await _openFoodService.UpdateProduct(product);
        }
    }
}
