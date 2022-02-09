namespace WebShop.Core.DTOs
{
    public class NewSupplyResponseProduct
    {
        public string productName { get; set; }
        public double pricePerProduct { get; set; }

        public NewSupplyResponseProduct(string productName, double pricePerProduct)
        {
            this.productName = productName;
            this.pricePerProduct = pricePerProduct;
        }
    }
}
