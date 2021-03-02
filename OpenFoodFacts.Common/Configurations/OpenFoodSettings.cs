namespace OpenFoodFacts.Common.Configurations
{
    public class OpenFoodSettings
    {
        public string OpenFoodFactsBaseUrl { get; set; }
        public string OpenFoodDataBaseTextUrl { get; set; }
        public string OpenFoodImageUrlBase { get; set; }
        public int ImportedProductsMaxValue { get; set; }
        public string CronExpression { get; set; }
    }
}
