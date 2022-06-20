using Desafio.Umbler.Models;
using Desafio.Umbler.Models.ViewModel;

namespace Desafio.Umbler.Web.Service
{
    public class DomainService : IDomainService
    {
        public HttpClient _httpClient;
        public DomainService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViewModelDomain> GetDomain(string domainName)
        {
            try
            {
                var domain = await _httpClient.GetFromJsonAsync<ViewModelDomain>($"api/domain/{domainName}");
            
                return domain;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message)  ;
            }

        }
    }
}
