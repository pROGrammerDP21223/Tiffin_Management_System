using Microsoft.EntityFrameworkCore;
using TiffinManagement.Models;
using TiffinManagement.Repository;
using TiffinManagement.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly AppDbContext _context;

    public AnalyticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime fromDate, DateTime toDate)
    {
        // Total Orders count
        var totalOrders = await _context.Orders
            .Where(o => o.StartDate >= fromDate && o.StartDate <= toDate)
            .CountAsync();

        // Orders by Status
        var ordersByStatus = await _context.Orders
            .Where(o => o.StartDate >= fromDate && o.StartDate <= toDate)
            .GroupBy(o => o.Status)
            .Select(g => new { g.Key, Count = g.Count() })
            .ToDictionaryAsync(g => g.Key, g => g.Count);

        // Popular Tiffin Plans
        var popularPlans = await _context.Orders
            .Where(o => o.StartDate >= fromDate && o.StartDate <= toDate)
            .GroupBy(o => o.TiffinPlanId)
            .Select(g => new { TiffinPlanId = g.Key, Count = g.Count() })
            .ToListAsync(); // Fetch the data to memory

        var popularPlansWithNames = popularPlans
            .Join(_context.TiffinPlans,
                  g => g.TiffinPlanId,
                  p => p.TiffinPlanId,
                  (g, p) => new { p.Name, Count = g.Count })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToDictionary(x => x.Name, x => x.Count);

        // Total Revenue
        var totalRevenue = await _context.Payments
            .Where(p => p.PaymentStatus == "Success" && p.PaymentDate >= fromDate && p.PaymentDate <= toDate)
            .SumAsync(p => p.Amount);

        // Total Deliveries
        var totalDeliveries = await _context.Deliveries
            .Where(d => d.DeliveryDate >= fromDate && d.DeliveryDate <= toDate)
            .CountAsync();

        // Delivered Count
        var deliveredCount = await _context.Deliveries
            .Where(d => d.IsDelivered == true && d.DeliveryDate >= fromDate && d.DeliveryDate <= toDate)
            .CountAsync();

        // New Orders this Week
        var newOrdersThisWeek = await _context.Orders
            .Where(o => o.StartDate >= toDate.AddDays(-7))
            .CountAsync();

        // Distinct users within the period
        var usersInPeriod = await _context.Orders
            .Where(o => o.StartDate >= fromDate && o.StartDate <= toDate)
            .Select(o => o.UserId)
            .Distinct()
            .ToListAsync();

        int newCustomers = 0;
        int returningCustomers = 0;

        // Count new vs returning customers
        foreach (var userId in usersInPeriod)
        {
            var firstOrderDate = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.StartDate)
                .Select(o => o.StartDate)
                .FirstOrDefaultAsync();

            if (firstOrderDate >= fromDate && firstOrderDate <= toDate)
                newCustomers++;
            else
                returningCustomers++;
        }

        // Calculate Delivery Success Rate
        double deliverySuccessRate = totalDeliveries == 0 ? 0 : (double)deliveredCount / totalDeliveries * 100;

        // Return AnalyticsDto with the gathered data
        return new AnalyticsDto
        {
            TotalOrders = totalOrders,
            OrdersByStatus = ordersByStatus,
            PopularTiffinPlans = popularPlansWithNames,
            TotalRevenue = totalRevenue,
            DeliverySuccessRate = deliverySuccessRate,
            NewOrdersThisWeek = newOrdersThisWeek,
            NewCustomers = newCustomers,
            ReturningCustomers = returningCustomers
        };
    }
}
