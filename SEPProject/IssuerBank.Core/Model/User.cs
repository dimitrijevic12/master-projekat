using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Model
{
    public abstract class User
    {
        public Guid Id { get; private set; }

        protected User(Guid id)
        {
            Id = id;
        }

        protected User()
        {
        }
    }
}
