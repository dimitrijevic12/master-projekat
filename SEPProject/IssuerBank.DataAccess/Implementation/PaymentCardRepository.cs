using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
{
    public class PaymentCardRepository : Repository<PaymentCard>, IPaymentCardRepository
    {
        private AppDbContext dbContext;

        public PaymentCardRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public PaymentCard GetByPAN(string pan) => dbContext.PaymentCards.ToList()
            .Where(paymentCard => paymentCard.PAN.Equals(pan)).FirstOrDefault();
    }
}