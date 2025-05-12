namespace TiffinManagement.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }
    }

}
