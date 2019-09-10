using ServiceStation.Domain.Entities;
using System.Collections.Generic;

namespace ServiceStation.WebUI.Models
{
    public class OrdersListViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public string CurrentCarId { get; set; }
    }
}