using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public abstract class User
    {
        protected Guid Id { get; set; }

        protected User(Guid id)
        {
            Id = id;
        }
    }
}
