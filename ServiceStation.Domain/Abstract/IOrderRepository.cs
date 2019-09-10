using ServiceStation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Domain.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        IQueryable<OrderService> OrderServices { get; }
        IQueryable<ServiceType> ServiceTypes { get; }
        void SaveOrder(Order order, IEnumerable<OrderService> orderServices);
        Order DeleteOrder(int orderId);
    }
}
