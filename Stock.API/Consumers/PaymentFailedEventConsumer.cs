using MassTransit;
using Shared.Events;
using Stock.API.Services;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Stock.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        readonly MongoDbService _mongoDbService;

        public PaymentFailedEventConsumer(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var collection = _mongoDbService.GetCollection<Models.Stock>();

            foreach (var item in context.Message.OrderItems)
            {
                Models.Stock stock = await (await collection.FindAsync(s => s.ProductId == item.ProductId)).FirstOrDefaultAsync();
                if (stock != null)
                {
                    stock.Count += item.Count;
                    await collection.FindOneAndReplaceAsync(s => s.ProductId == item.ProductId, stock);
                }
            }
        }
    }
}
