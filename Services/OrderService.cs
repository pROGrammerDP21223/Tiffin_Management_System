using System.Numerics;
using Microsoft.EntityFrameworkCore;
using TiffinManagement.Models;
using TiffinManagement.Repository;

namespace TiffinManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            //return await _context.Orders.FindAsync(orderId);
           var order =  await _context.Orders.FindAsync(orderId);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == order.UserId);
            var plan = await _context.TiffinPlans.FirstOrDefaultAsync(p => p.TiffinPlanId == order.TiffinPlanId);
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == order.OrderId);

            return new OrderDto
            {
                OrderId = order.OrderId,
                OrderStatus = order.Status,
                StartDate = order.StartDate,

                UserName = user?.FullName,

                PlanName = plan?.Name,

                PaymentStatus = payment?.PaymentStatus,

                IsDelivered = delivery?.IsDelivered
            };
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
           var  orders = await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
            var orderDetailsList = new List<OrderDto>();
            foreach (var order in orders)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == order.UserId);
                var plan = await _context.TiffinPlans.FirstOrDefaultAsync(p => p.TiffinPlanId == order.TiffinPlanId);
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == order.OrderId);

                orderDetailsList.Add(new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderStatus = order.Status,
                    StartDate = order.StartDate,

                    UserName = user?.FullName,

                    PlanName = plan?.Name,

                    PaymentStatus = payment?.PaymentStatus,

                    IsDelivered = delivery?.IsDelivered
                });
            }

            return orderDetailsList;


        }
        public async Task<OrderDetailsDto> GetOrderDetailsAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null) return null;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == order.UserId);
            var plan = await _context.TiffinPlans.FirstOrDefaultAsync(p => p.TiffinPlanId == order.TiffinPlanId);
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == order.OrderId);

            return new OrderDetailsDto
            {
                OrderId = order.OrderId,
                OrderStatus = order.Status,
                StartDate = order.StartDate,

                UserName = user?.FullName,
                UserEmail = user?.Email,

                PlanName = plan?.Name,
                PricePerDay = plan?.PricePerDay ?? 0,

                PaymentAmount = payment?.Amount,
                PaymentStatus = payment?.PaymentStatus,
                TransactionId = payment?.TransactionId,

                IsDelivered = delivery?.IsDelivered,
                DeliveryDate = delivery?.DeliveryDate
            };
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
       
            var orders = await _context.Orders.ToListAsync();

            var orderDetailsList = new List<OrderDto>();
            foreach (var order in orders)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == order.UserId);
                var plan = await _context.TiffinPlans.FirstOrDefaultAsync(p => p.TiffinPlanId == order.TiffinPlanId);
                var payment = await _context.Payments.FirstOrDefaultAsync(p => p.OrderId == order.OrderId);
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == order.OrderId);

                orderDetailsList.Add(new OrderDto
                {
                    OrderId = order.OrderId,
                    OrderStatus = order.Status,
                    StartDate = order.StartDate,

                    UserName = user?.FullName,

                    PlanName = plan?.Name,

                    PaymentStatus = payment?.PaymentStatus,

                    IsDelivered = delivery?.IsDelivered
                });
            }

            return orderDetailsList;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }

}
