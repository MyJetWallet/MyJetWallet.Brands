using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyJetWallet.Brands
{
    public interface IBrandManager
    {
        Task<IBrand> AddBrandAsync(string brandId, IEnumerable<string> domains);
        Task SetDomainPoolToBrandAsync(string brandId, IEnumerable<string> domainPool);
        Task RemoveBrandAsync(string brandId);

        Task<List<IBrand>> GetAllAsync();
    }
}