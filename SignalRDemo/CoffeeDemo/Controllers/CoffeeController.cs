using CoffeeDemo.Hubs;
using CoffeeDemo.Models;
using CoffeeDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeDemo.Controllers
{
    public class CoffeeController : Controller
    {
        private readonly OrderService orderService;

        private readonly IHubContext<CoffeeHub> cofeeHub;

        public CoffeeController(
            OrderService _orderService,
            IHubContext<CoffeeHub> _hubContext)
        {
            orderService = _orderService;
            cofeeHub = _hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> OrderCoffee([FromBody] Order order)
        {
            await cofeeHub.Clients.All.SendAsync("NewOrder", order);
            var orderId = orderService.NewOrder();

            return Accepted(orderId);
        }
    }
}
