using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TiffinManagement.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int TiffinPlanId { get; set; } 
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public string Status { get; set; }
    }

}
