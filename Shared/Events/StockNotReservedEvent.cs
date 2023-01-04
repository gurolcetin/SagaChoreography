using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockNotReservedEvent : IEvent
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public string Message { get; set; }
    }
}
