using System.Collections;
using System.Collections.Generic;

namespace MyJetWallet.Brands
{
    public interface IBrand
    {
        string Id { get; }

        public IReadOnlyList<string> DomainsPool { get; }
    }
}