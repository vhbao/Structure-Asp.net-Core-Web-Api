using System.Net.Http;
using System.Threading.Tasks;
using Wolf.Core.Models;

namespace Wolf.Core.Interfaces
{
    public interface IHttpClientBuilder
    {
        Task<ClientResponseInfo> GetAsync(string Uri);
        Task<ClientResponseInfo> PostAsync(string Uri, string JsonContent);
        Task<ClientResponseInfo> PutAsync(string Uri, string JsonContent);
        Task<ClientResponseInfo> DeleteAsync(string Uri);
        Task<ClientResponseInfo> DeleteAsJsonAsync(string Uri, string JsonContent);        
    }
}
