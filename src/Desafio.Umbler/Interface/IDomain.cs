using Desafio.Umbler.Models;
using Desafio.Umbler.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Desafio.Umbler.Interface
{
    public interface IDomain<TEntity> where TEntity : class
    {
        Task<ViewModelDomain> GetDomain(string domainName);
        Task<Domain> TimeDomain(TEntity entity);

    }
}
