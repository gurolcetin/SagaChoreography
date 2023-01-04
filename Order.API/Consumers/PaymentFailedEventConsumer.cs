﻿using MassTransit;
using Order.API.Models.Context;
using Order.API.Models.Enums;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        readonly ApplicationDbContext _context;
        public PaymentFailedEventConsumer(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            Models.Order order = await _context.FindAsync<Models.Order>(context.Message.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Fail;
                await _context.SaveChangesAsync();
                Console.WriteLine(context.Message.Message);
            }
        }
    }
}
