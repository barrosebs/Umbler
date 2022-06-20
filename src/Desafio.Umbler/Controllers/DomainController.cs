using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Models.ViewModel;
using Desafio.Umbler.Repository;

namespace Desafio.Umbler.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        //private readonly DatabaseContext _db;
        private readonly DomainRepository _domainService;
        public DomainController(DomainRepository domainService)
        {
            _domainService = domainService;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            ViewModelDomain domain = await _domainService.GetDomain(domainName);
            return Ok(domain);
        }
    }
}
