using Microsoft.Extensions.Hosting;
using Rabbit.Platform;

namespace ProductService.BLL
{
    public class ShopToProductMessagesConsumer : IHostedService
    {
        private readonly IMessageConsumer _consumer;
        private readonly IProductMainService _productMainService;

        public ShopToProductMessagesConsumer(IMessageConsumer consumer, 
            IProductMainService productMainService)
        {
            _consumer = consumer;
            _productMainService = productMainService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            do
            {
                await _consumer.ProcessMessageAsync<ShopChangeMessage>(
                    DeleteShopAllProduct,
                    RoutingKeys.ShopDeleted,
                    "shop.exchange",
                    "shop.queue"
                );
            } while (!cancellationToken.IsCancellationRequested);
            
        }

        private async Task DeleteShopAllProduct(ShopChangeMessage message)
        {
            await _productMainService.DeleteShopAllProductsAsync(message.ShopId);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
