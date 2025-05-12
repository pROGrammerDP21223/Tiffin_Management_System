namespace TiffinManagement.Models
{
    public class AnalyticsDto
    {
        public int TotalOrders { get; set; }
        public Dictionary<string, int> OrdersByStatus { get; set; }
        public Dictionary<string, int> PopularTiffinPlans { get; set; }
        public decimal TotalRevenue { get; set; }
        public double DeliverySuccessRate { get; set; }
        public int NewOrdersThisWeek { get; set; }
        public int NewCustomers { get; set; }
        public int ReturningCustomers { get; set; }
    }
}
