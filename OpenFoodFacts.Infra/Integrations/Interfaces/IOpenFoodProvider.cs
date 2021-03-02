using System.Threading.Tasks;

namespace OpenFoodFacts.Infra.Integrations.Interfaces
{
    public interface IOpenFoodProvider
    {
        Task<string[]> GetFileNames();
        void DownloadFile(string fileName);
        void DeleteFile(string fileName);
    }
}
