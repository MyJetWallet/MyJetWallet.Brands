using System;
using System.Collections.Generic;

namespace MyJetWallet.Brands
{
    public interface IBrandReader
    {
        IReadOnlyList<IBrand> GetAll();
        IBrand GetById(string brandId);
        IBrand GetByHost(string host);
    }
}