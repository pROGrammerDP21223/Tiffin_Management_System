namespace TiffinManagement.Models
{
    public class OrderDto
    {
     public int OrderId { get; set; }
      
        public string PlanName { get; set; }
        public string UserName { get; set; }

        public DateTime StartDate { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public bool? IsDelivered { get; set; }
    }

}
