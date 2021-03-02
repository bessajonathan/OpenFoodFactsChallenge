using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenFoodFacts.Common.Configurations;
using OpenFoodFacts.Common.Exceptions;
using OpenFoodFacts.Infra.Integrations.Interfaces;
using RestSharp;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OpenFoodFacts.Infra.Integrations.OpenFood
{
    public class OpenFoodProvider : IOpenFoodProvider
    {
        private readonly IOptions<OpenFoodSettings> _settings;
        private readonly ILogger<OpenFoodSettings> _logger;

        public OpenFoodProvider(IOptions<OpenFoodSettings> settings, ILogger<OpenFoodSettings> logger)
        {
            if (string.IsNullOrEmpty(settings.Value.OpenFoodDataBaseTextUrl) ||
                string.IsNullOrEmpty(settings.Value.OpenFoodFactsBaseUrl))
                throw new NotFoundException("Urls para download dos dados não foram informadas.");

            _settings = settings;
            _logger = logger;
        }
        public async Task<string[]> GetFileNames()
        {
            var client = new RestClient(_settings.Value.OpenFoodDataBaseTextUrl);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogError("GetFileNames - Erro ao buscar arquivo .text");

                throw new IntegrationException("GetFileNames - Erro ao buscar arquivo.text");
            }

            return response.Content.Split('\n')
                .Where(text => !string.IsNullOrEmpty(text)).ToArray();
        }

        public void DownloadFile(string fileName)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile($"{_settings.Value.OpenFoodFactsBaseUrl}{fileName}",$"{fileName}");

                Decompress();
            }
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(fileName);
            File.Delete(fileName.Replace(".gz",string.Empty));
        }

        private void Decompress()
        {
            var path = new DirectoryInfo(".\\");

            foreach (var file in path.GetFiles("*.gz"))
            {
                using (var fileStream = file.OpenRead())
                {
                    string fileName = file.FullName.Remove(file.FullName.Length - file.Extension.Length);

                    using (var decompress = File.Create(fileName))
                    {
                        using (var gzip = new GZipStream(fileStream,CompressionMode.Decompress))
                        {
                            gzip.CopyTo(decompress);
                            decompress.Close();
                        }
                    }

                    fileStream.Close();
                }
            }
        }
    }
}
