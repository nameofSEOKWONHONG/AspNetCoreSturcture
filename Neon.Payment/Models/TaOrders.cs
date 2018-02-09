using System;
using System.Collections.Generic;

namespace Neon.Payment.Models
{
    public partial class TaOrders
    {
        public int OrdId { get; set; }
        public int OrdUserId { get; set; }
        public decimal OrdCost { get; set; }
        public string RegId { get; set; }
        public string RegIp { get; set; }
        public DateTime? RegDt { get; set; }
    }
}
