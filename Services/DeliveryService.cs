using Microsoft.EntityFrameworkCore;
using TiffinManagement.Models;
using TiffinManagement.Repository;

namespace TiffinManagement.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly AppDbContext _context;

        public DeliveryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task<Delivery> GetDeliveryByOrderIdAsync(int orderId)
        {
            return await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == orderId);
        }

        public async Task UpdateDeliveryStatusAsync(int deliveryId, bool isDelivered)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery != null)
            {
                delivery.IsDelivered = isDelivered;
                await _context.SaveChangesAsync();
            }
        }
    }

}
