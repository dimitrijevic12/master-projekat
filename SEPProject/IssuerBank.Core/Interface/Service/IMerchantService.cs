using CSharpFunctionalExtensions;
using IssuerBank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Interface.Service
{
    public interface IMerchantService
    {
        public Result<Merchant> Create(string name);
    }
}
