using TiffinManagement.Models;

namespace TiffinManagement.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);



         Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId);

         Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, string status);

     Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId);
    }
}
