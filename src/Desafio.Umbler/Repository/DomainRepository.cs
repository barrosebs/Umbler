using Desafio.Umbler.Interface;
using Desafio.Umbler.Models;
using Desafio.Umbler.Models.ViewModel;
using DnsClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Repository
{
    public class DomainRepository : IDomain<Domain>
    {
        private readonly DatabaseContext _db;

        public DomainRepository(DatabaseContext db)
        {
           _db = db;
        }

        public async Task<ViewModelDomain> GetDomain(string domainName)
        {
            Domain domain = await _db.Domains.FirstOrDefaultAsync(d => d.Name == domainName);
            if (domain == null)
            {
                var response = await WhoisClient.QueryAsync(domainName);

                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domainName, QueryType.ANY);
                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                var hostResponse = await WhoisClient.QueryAsync(ip);

                domain = new Domain
                {
                    Name = domainName,
                    Ip = ip,
                    UpdatedAt = DateTime.Now,
                    WhoIs = response.Raw,
                    Ttl = record?.TimeToLive ?? 0,
                    HostedAt = hostResponse.OrganizationName
                };

                _db.Domains.Add(domain);
            }

            if (DateTime.Now.Subtract(domain.UpdatedAt).TotalMinutes > domain.Ttl)
            {
                await TimeDomain(domain);
            }

            await _db.SaveChangesAsync();
            ViewModelDomain domainViewModel = new ViewModelDomain();
            domainViewModel.Name = domain.Name;
            domainViewModel.Ip = domain.Ip;
            domainViewModel.UpdatedAt = domain.UpdatedAt.ToString();
            domainViewModel.HostedAt = domain.HostedAt;

            return domainViewModel;
        }



        public async Task<Domain> TimeDomain(Domain entity)
        {
            var response = await WhoisClient.QueryAsync(entity.Name);

            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(entity.Name, QueryType.ANY);
            var record = result.Answers.ARecords().FirstOrDefault();
            var address = record?.Address;
            var ip = address?.ToString();

            var hostResponse = await WhoisClient.QueryAsync(ip);

            entity.Ip = ip;
            entity.UpdatedAt = DateTime.Now;
            entity.WhoIs = response.Raw;
            entity.Ttl = record?.TimeToLive ?? 0;
            entity.HostedAt = hostResponse.OrganizationName;
            return entity;
        }
    }
}
