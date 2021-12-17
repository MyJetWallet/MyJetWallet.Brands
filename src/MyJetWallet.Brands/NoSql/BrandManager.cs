using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Brands.NoSql
{
    internal class BrandManager : IBrandManager
    {
        private readonly IMyNoSqlServerDataWriter<BrandNoSql> _writer;

        public BrandManager(IMyNoSqlServerDataWriter<BrandNoSql> writer)
        {
            _writer = writer;
        }
        
        public async Task<IBrand> AddBrandAsync(string brandId, IEnumerable<string> domains, string senderAddress)
        {
            var brand = await _writer.GetAsync(BrandNoSql.GeneratePartitionKey(),
                BrandNoSql.GenerateRowKey(brandId));

            if (brand != null)
            {
                throw new ArgumentException("Brand already exist", nameof(brand));
            }
            
            brand = BrandNoSql.Create(brandId, domains, senderAddress);

            await _writer.InsertAsync(brand);

            return brand;
        }

        public async Task<List<IBrand>> GetAllAsync()
        {
            var data = await _writer.GetAsync();

            return data.Cast<IBrand>().ToList();
        }

        public async Task SetDomainPoolToBrandAsync(string brandId, IEnumerable<string> domainPool)
        {
            var brand = await _writer.GetAsync(BrandNoSql.GeneratePartitionKey(),
                BrandNoSql.GenerateRowKey(brandId));

            if (brand == null)
            {
                throw new ArgumentException("Brand does not exist", nameof(brand));
            }

            brand.DomainsPool = domainPool.ToList();

            await _writer.InsertOrReplaceAsync(brand);
        }
        
        public async Task SetSenderAddressToBrandAsync(string brandId, string senderAddress)
        {
            var brand = await _writer.GetAsync(BrandNoSql.GeneratePartitionKey(),
                BrandNoSql.GenerateRowKey(brandId));

            if (brand == null)
            {
                throw new ArgumentException("Brand does not exist", nameof(brand));
            }

            brand.EmailSenderAddress = senderAddress;

            await _writer.InsertOrReplaceAsync(brand);
        }

        public async Task RemoveBrandAsync(string brandId)
        {
            await _writer.DeleteAsync(BrandNoSql.GeneratePartitionKey(), BrandNoSql.GenerateRowKey(brandId));
        }
    }
}