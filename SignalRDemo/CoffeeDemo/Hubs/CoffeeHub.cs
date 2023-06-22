using CoffeeDemo.Models;
using CoffeeDemo.Services;
using Microsoft.AspNetCore.SignalR;

namespace CoffeeDemo.Hubs
{
    public class CoffeeHub : Hub
    {
        private readonly OrderService orderService;

        public CoffeeHub(OrderService _orderService)
        {
            orderService = _orderService;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult result;

            do
            {
                result = orderService.GetUpdate(orderId);

                if (result.New)
                {
                    await Clients.Caller.SendAsync("ReceiveOrderUpdate", result.Update);
                }
            } while (!result.Finished);


            await Clients.Caller.SendAsync("Finished");
        }
    }
}
