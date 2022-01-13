using PCC.Core.Interface.Repository;
using PCC.Core.Model;

namespace PCC.Core.Interfaces.Repository
{
    public interface IBankRepository : IRepository<Bank>
    {
        public Bank GetByPAN(string pan);
    }
}