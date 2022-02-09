using System.Collections.Generic;

namespace WebShop.Core.DTOs
{
    public class NewSupplyResponse
    {
        public List<NewSupplyResponseProduct> newSupplyResponseProduct { get; set; }

        public NewSupplyResponse(List<NewSupplyResponseProduct> newSupplyResponseProduct)
        {
            this.newSupplyResponseProduct = newSupplyResponseProduct;
        }
    }
}
