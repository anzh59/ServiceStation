using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceStation.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double Amount { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        InProgress = 1,
        Completed = 2,
        Canceled = 3
    }
}
