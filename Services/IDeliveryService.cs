using TiffinManagement.Models;

namespace TiffinManagement.Services
{
    public interface IDeliveryService
    {
        Task<Delivery> CreateDeliveryAsync(Delivery delivery);
        Task<Delivery> GetDeliveryByOrderIdAsync(int orderId);
        Task UpdateDeliveryStatusAsync(int deliveryId, bool isDelivered);
    }
}
