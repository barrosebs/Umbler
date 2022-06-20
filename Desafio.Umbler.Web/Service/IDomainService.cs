using Desafio.Umbler.Models.ViewModel;

namespace Desafio.Umbler.Web.Service
{
    public interface IDomainService
    {
        Task<ViewModelDomain> GetDomain(string domainName);
    }
}
