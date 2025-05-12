using TiffinManagement.Models;

namespace TiffinManagement.Services
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDto> GetAnalyticsAsync(DateTime fromDate, DateTime toDate);
    }
}
