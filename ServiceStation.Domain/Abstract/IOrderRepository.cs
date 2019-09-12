using System.Collections.Generic;
using System.Linq;

using ServiceStation.Domain.Entities;

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
