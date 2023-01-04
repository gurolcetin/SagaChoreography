using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class RabbitMQSettings
    {
        public const string Stock_OrderCreatedEventQueue = "stock-order-created-queue";
        public const string Payment_StockReservedEventQueue = "payment-stock-reserved-queue";
        public const string Order_PaymentCompletedEventQueue = "order-payment-completed-queue";
        public const string Order_PaymentFailedEventQueue = "order-payment-failed-queue";
        public const string Stock_PaymentFailedEventQueue = "stock-payment-failed-queue";
        public const string Order_StockNotReservedEventQueue = "order-stock-not-reserved-queue";
    }
}
