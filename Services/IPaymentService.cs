using TiffinManagement.Models;

namespace TiffinManagement.Services
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
        Task UpdatePaymentStatusAsync(int paymentId, string status);
    }
}
