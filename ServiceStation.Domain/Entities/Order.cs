using System;
using System.Collections.Generic;

namespace ServiceStation.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        InProgress = 1,
        Completed = 2,
        Canceled = 3
    }
}
