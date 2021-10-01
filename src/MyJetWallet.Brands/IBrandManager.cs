using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyJetWallet.Brands
{
    public interface IBrandManager
    {
        Task<IBrand> AddBrandAsync(string brandId, IEnumerable<string> domains);
        Task SetDomainPoolToBrand(string brandId, IEnumerable<string> domainPool);
        Task RemoveBrand(string brandId);
    }
}