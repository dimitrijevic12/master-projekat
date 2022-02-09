namespace WebShop.Core.DTOs
{
    public class NewReviewedSupplyResponse
    {
        public NewSupplyResponse newSupplyResponse { get; set; }
        public bool accepted { get; set; }

        public NewReviewedSupplyResponse(NewSupplyResponse newSupplyResponse, bool accepted)
        {
            this.newSupplyResponse = newSupplyResponse;
            this.accepted = accepted;
        }
    }
}
