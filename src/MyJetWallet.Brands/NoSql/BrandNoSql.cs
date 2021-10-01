using System;
using System.Collections.Generic;
using System.Linq;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Brands.NoSql
{
    public class BrandNoSql : MyNoSqlDbEntity, IBrand
    {
        public const string TableName = "jetwallet-brands";
        public static string GeneratePartitionKey() => "brands";
        public static string GenerateRowKey(string brandId) => brandId;
        
        public string Id { get; set; }
        public List<string> DomainsPool { get; set; }

        IReadOnlyList<string> IBrand.DomainsPool => DomainsPool;

        public static BrandNoSql Create(string brandId, IEnumerable<string> domainPool)
        {
            if (string.IsNullOrWhiteSpace(brandId))
            {
                throw new ArgumentException("Brand id cannot be empty", nameof(brandId));
            }
            
            return new BrandNoSql()
            {
                PartitionKey = GeneratePartitionKey(),
                RowKey = GenerateRowKey(brandId),
                Id = brandId,
                DomainsPool = domainPool?.ToList() ?? new List<string>()
            };
        }
    }
}