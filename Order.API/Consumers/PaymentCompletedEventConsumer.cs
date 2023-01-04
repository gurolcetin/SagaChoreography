using MassTransit;
using Order.API.Models.Context;
using Order.API.Models.Enums;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public PaymentCompletedEventConsumer(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            Models.Order order = await _applicationDbContext.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Completed;
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
