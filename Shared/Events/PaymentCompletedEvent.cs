using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class PaymentCompletedEvent : IEvent
    {
        public int OrderId { get; set; }
    }
}
