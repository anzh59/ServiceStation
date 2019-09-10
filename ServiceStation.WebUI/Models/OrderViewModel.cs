using ServiceStation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceStation.WebUI.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public OrderServiceViewModel[] OrderServices { get; set; }
        public IEnumerable<ServiceType> ServiceTypes { get; set; }
    }

    public class OrderServiceViewModel
    {
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public string Note { get; set; }
        public bool Checked { get; set; }
    }
}