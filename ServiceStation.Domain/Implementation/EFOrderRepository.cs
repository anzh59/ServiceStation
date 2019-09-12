using System.Collections.Generic;
using System.Linq;

using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;

namespace ServiceStation.Domain.Implementation
{
    public class EFOrderRepository : IOrderRepository
    {
        private EFDbContext _context;

        public EFOrderRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders
        {
            get { return _context.Orders; }
        }

        public IQueryable<OrderService> OrderServices
        {
            get { return _context.OrderService; }
        }
        public IQueryable<ServiceType> ServiceTypes
        {
            get { return _context.ServiceTypes; }
        }

        public Order DeleteOrder(int orderId)
        {
            Order dbEntry = _context.Orders.Find(orderId);
            if (dbEntry != null)
            {
                _context.Orders.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveOrder(Order order, IEnumerable<OrderService> orderServices)
        {
            if (order.Id == 0)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();

                foreach (var service in orderServices)
                    service.OrderId = order.Id;

                _context.OrderService.AddRange(orderServices);
            }
            else
            {
                Order dbEntry = _context.Orders.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = order.Id;
                    dbEntry.Status = order.Status;
                    dbEntry.CarId = order.CarId;
                    dbEntry.Amount = order.Amount;
                    dbEntry.Date = order.Date;
                }

                _context.OrderService.RemoveRange(_context.OrderService.Where(x => x.OrderId == order.Id));
                _context.OrderService.AddRange(orderServices);
            }
            _context.SaveChanges();
        }
    }
}
