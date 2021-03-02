using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OpenFoodFacts.Common.Configurations;
using OpenFoodFacts.Common.Exceptions;
using OpenFoodFacts.Domain.Entities;
using OpenFoodFacts.Domain.Enums;
using OpenFoodFacts.Domain.Repository.Interfaces;
using OpenFoodFacts.Domain.Service.Interfaces;
using OpenFoodFacts.Infra.Integrations.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;

namespace OpenFoodFacts.Domain.Service
{

    public class JobService : IJobService
    {
        private int ImportedProductsMaxValue, ImportedNumber;

        private readonly IOpenFoodProvider _openFoodProvider;
        private readonly IOpenFoodRepository _openFoodRepository;
        private readonly IOptions<OpenFoodSettings> _settings;

        public JobService(IOpenFoodProvider openFoodProvider, IOpenFoodRepository openFoodRepository, IOptions<OpenFoodSettings> settings)
        {

            if (settings.Value.ImportedProductsMaxValue <= 0)
                throw new NotFoundException("Numero máximo de arquivos importados não definido");

            if (string.IsNullOrEmpty(settings.Value.OpenFoodImageUrlBase))
                throw new NotFoundException("Url base para geração das imagens não foi definida");

            ImportedProductsMaxValue = settings.Value.ImportedProductsMaxValue;
            _openFoodProvider = openFoodProvider;
            _openFoodRepository = openFoodRepository;
            _settings = settings;
        }
        public async Task DownloadData()
        {
            var fileNames = await _openFoodProvider.GetFileNames();

            foreach (var fileName in fileNames)
            {
                if (ImportedNumber >= ImportedProductsMaxValue)
                    return;

                var fileHistory = await _openFoodRepository.GetFileHistory(fileName);

                if (fileHistory is null)
                {
                    _openFoodProvider.DownloadFile(fileName);
                    var json = await ReadJsonFile(fileName);

                    if (json is null || json.Count == 0)
                    {
                        _openFoodProvider.DeleteFile(fileName);
                        _openFoodRepository.AddFileDownloaded(new FileHistory(fileName, 0, 0));
                        continue;
                    }

                    await AddProducts(json.Take(ImportedProductsMaxValue).ToList(), fileName);
                    _openFoodRepository.AddFileDownloaded(new FileHistory(fileName, ImportedNumber, json.Count));
                }
                else
                {
                    if (fileHistory.LinesRead == fileHistory.TotalLines)
                        continue;

                    _openFoodProvider.DownloadFile(fileName);
                    var json = await ReadJsonFile(fileName);

                    await AddProducts(json.Skip(fileHistory.LinesRead).Take(ImportedProductsMaxValue).ToList(), fileName);
                    _openFoodRepository.AddFileDownloaded(new FileHistory(fileName, ImportedNumber, json.Count));

                    fileHistory.LinesRead += ImportedNumber;

                    _openFoodRepository.UpdateFileHistory(fileHistory);
                }
            }
        }

        public DateTime? GetLasCronExecution()
        {
            DateTime? lastCronExecution;
            using (var connection = JobStorage.Current.GetConnection())
            {
                var lastJob = connection.GetRecurringJobs().LastOrDefault();
                lastCronExecution = lastJob.LastExecution;
            }

            return lastCronExecution;
        }

        private async Task AddProducts(List<JObject> json, string fileName)
        {
            ImportedNumber = 0;
            var products = new List<Product>();

            foreach (var jsonLine in json)
            {
                if (ImportedNumber <= ImportedProductsMaxValue)
                {
                    products.Add(new Product
                    {
                        Code = (long)jsonLine["code"],
                        Status = EProductStatus.Published,
                        Imported_t = DateTime.Now,
                        Url = (string)jsonLine["url"],
                        Creator = (string)jsonLine["creator"],
                        Created_t = (string)jsonLine["created_t"],
                        Last_modified_t = (string)jsonLine["last_modified_t"],
                        Product_name = (string)jsonLine["product_name"],
                        Quantity = (string)jsonLine["quantity"],
                        Brands = (string)jsonLine["brands"],
                        Categories = (string)jsonLine["categories"],
                        Cities = (string)jsonLine["cities"],
                        Purchase_places = (string)jsonLine["purchase_places"],
                        Stores = (string)jsonLine["stores"],
                        Ingredients_Text = (string)jsonLine["ingredients_text"],
                        Traces = (string)jsonLine["traces"],
                        Serving_Size = (string)jsonLine["serving_size"],
                        Serving_Quantity = (double?)jsonLine["serving_quantity"],
                        Nutriscore_Score = (double?)jsonLine["nutriscore_score"],
                        Nutriscore_Grade = (string)jsonLine["nutriscore_grade"],
                        Main_Category = (string)jsonLine["main_category"],
                        Image_Url = GenerateImageUrl((string)jsonLine["code"])
                    });

                    ImportedNumber++;
                }

                _openFoodProvider.DeleteFile(fileName);
            }
            _openFoodRepository.AddNewProducts(products);
        }

        private string GenerateImageUrl(string code)
        {
            var url = _settings.Value.OpenFoodImageUrlBase;


            if (code.Length > 8)
            {
                string[] codeSplit = Regex.Split(code, "^(...)(...)(...)(.*)$").Where(code => !string.IsNullOrEmpty(code)).ToArray();

                string lastCode = codeSplit.Last();

                foreach (var number in codeSplit)
                {

                    url += number == lastCode ? $"{number}" : $"{number}/";
                }
            }
            else
            {
                url += code;
            }

            return url;
        }

        private async Task<List<JObject>> ReadJsonFile(string fileName)
        {

            var json = new List<JObject>();

            using (var file = new StreamReader(fileName.Replace(".gz", string.Empty)))
            {
                string line = string.Empty;

                while ((line = await file.ReadLineAsync()) != null)
                {
                    json.Add(JObject.Parse(line));
                }
            }

            return json;
        }
    }
}
