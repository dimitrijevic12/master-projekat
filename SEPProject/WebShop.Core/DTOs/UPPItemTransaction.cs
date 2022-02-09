namespace WebShop.Core.DTOs
{
    public class UPPItemTransaction
    {
        public NewReviewedSupplyResponse newReviewedSupplyResponse { get; set; }
        public bool acceptedByProcurementManagementChief { get; set; }
        public bool acceptedByEmployeeMenager { get; set; }

        public UPPItemTransaction(NewReviewedSupplyResponse newReviewedSupplyResponse, bool acceptedByProcurementManagementChief, bool acceptedByEmployeeMenager)
        {
            this.newReviewedSupplyResponse = newReviewedSupplyResponse;
            this.acceptedByProcurementManagementChief = acceptedByProcurementManagementChief;
            this.acceptedByEmployeeMenager = acceptedByEmployeeMenager;
        }
    }
}
