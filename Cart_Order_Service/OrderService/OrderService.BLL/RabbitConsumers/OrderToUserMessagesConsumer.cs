using Microsoft.Extensions.Hosting;
using OrderService.BLL.Abstractions;
using Rabbit.Platform;
using Rabbit.Platform.Enums;

namespace OrderService.BLL
{
    public class OrderToUserMessagesConsumer : IHostedService
    {
        private readonly IMessageConsumer _consumer;
        private readonly IOrderService _orderService;

        public OrderToUserMessagesConsumer(IMessageConsumer consumer,
            IOrderService orderService)
        {
            _consumer = consumer;
            _orderService = orderService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _consumer.ProcessMessageAsync<ChangeStatusOrderMessage>(
                ChangeStatusOrderAsync,
                RoutingKeyStatusOrder.OrderCreated.ToString(),
                "order.exchange",
                "order.queue"
            );
        }

        private async Task ChangeStatusOrderAsync(ChangeStatusOrderMessage message)
        {
            await _orderService.UpdateOrderStatus(message.OrderId, message.Status);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
