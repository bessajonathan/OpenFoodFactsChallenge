using OpenFoodFacts.Domain.Enums;
using System;
using Newtonsoft.Json;
using NSwag.Annotations;

namespace OpenFoodFacts.Application.Product.ViewModels
{
    public class ProductViewModel
    {
        [JsonIgnore]
        [OpenApiIgnore]
        public long Code { get; set; }
        public EProductStatus Status { get; set; }
        public DateTime Imported_t { get; set; }
        public string Url { get; set; }
        public string Creator { get; set; }
        public string Created_t { get; set; }
        public string Last_modified_t { get; set; }
        public string Product_name { get; set; }
        public string Quantity { get; set; }
        public string Brands { get; set; }
        public string Categories { get; set; }
        public string Labels { get; set; }
        public string Cities { get; set; }
        public string Purchase_places { get; set; }
        public string Stores { get; set; }
        public string Ingredients_Text { get; set; }
        public string Traces { get; set; }
        public string Serving_Size { get; set; }
        public double? Serving_Quantity { get; set; }
        public double? Nutriscore_Score { get; set; }
        public string Nutriscore_Grade { get; set; }
        public string Main_Category { get; set; }
        public string Image_Url { get; set; }
    }
}
