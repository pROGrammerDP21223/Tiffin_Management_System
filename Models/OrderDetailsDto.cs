namespace TiffinManagement.Models
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime StartDate { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string PlanName { get; set; }
        public decimal PricePerDay { get; set; }

        public decimal? PaymentAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }

        public bool? IsDelivered { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
