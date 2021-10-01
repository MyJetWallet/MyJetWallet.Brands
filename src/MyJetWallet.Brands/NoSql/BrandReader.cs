using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Brands.NoSql;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Brands
{
    internal class BrandReader : IBrandReader
    {
        private readonly IMyNoSqlServerDataReader<BrandNoSql> _reader;
        private readonly ILogger<BrandReader> _logger;
        private bool _isInit = false;

        public BrandReader(IMyNoSqlServerDataReader<BrandNoSql> reader, ILogger<BrandReader> logger)
        {
            _reader = reader;
            _logger = logger;
        }

        public IReadOnlyList<IBrand> GetAll()
        {
            Init();
            return _reader.Get();
        }

        public IBrand GetById(string brandId)
        {
            Init();
            return _reader.Get(BrandNoSql.GeneratePartitionKey(), BrandNoSql.GenerateRowKey(brandId));
        }

        public IBrand GetByHost(string host)
        {
            Init();
            foreach (var brand in _reader.Get())
            {
                foreach (var domain in brand.DomainsPool)
                {
                    if (host.EndsWith(domain))
                        return brand;
                }
            }

            return null;
        }

        private void Init()
        {
            if (_isInit) return;

            var index = 0;
            while (!_reader.Get().Any() && index < 15)
            {
                Task.Delay(1000).GetAwaiter().GetResult();
                index++;
            }

            _isInit = true;

            if (!_reader.Get().Any())
            {
                _logger.LogError("Brand dictionary is empty!");
            }
        }
    }
}