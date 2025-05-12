using Microsoft.AspNetCore.Mvc;
using TiffinManagement.Models;
using TiffinManagement.Services;

namespace TiffinManagement.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        // GET: api/Delivery/OrderId
        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<Delivery>> GetDeliveryByOrderId(int orderId)
        {
            var delivery = await _deliveryService.GetDeliveryByOrderIdAsync(orderId);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        // POST: api/Delivery
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            var createdDelivery = await _deliveryService.CreateDeliveryAsync(delivery);
            return CreatedAtAction(nameof(GetDeliveryByOrderId), new { orderId = createdDelivery.OrderId }, createdDelivery);
        }

        // PUT: api/Delivery/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {
            if (id != delivery.DeliveryId)
            {
                return BadRequest();
            }

            await _deliveryService.UpdateDeliveryStatusAsync(id, delivery.IsDelivered);
            return NoContent();
        }
    }

}
