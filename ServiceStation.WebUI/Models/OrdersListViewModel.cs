using System.Collections.Generic;

using ServiceStation.Domain.Entities;

namespace ServiceStation.WebUI.Models
{
    public class OrdersListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public string CurrentCarId { get; set; }
    }
}