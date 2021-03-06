using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Interface.Repository
{
    public interface IRegisteredWebShopRepository : IRepository<RegisteredWebShop>
    {
        public RegisteredWebShop GetByEmail(string email);
    }
}