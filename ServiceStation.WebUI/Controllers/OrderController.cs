using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;
using ServiceStation.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceStation.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private ICarRepository _carRepository;
        private IOrderRepository _orderRepository;
        public OrderController(ICarRepository carRepository, IOrderRepository orderRepository)
        {
            _carRepository = carRepository;
            _orderRepository = orderRepository;
        }

        public ViewResult OrderList(string carid = null)
        {
            OrdersListViewModel viewModel = new OrdersListViewModel
            {
                Orders = _orderRepository.Orders
                    .Where(c => (c.CarId.ToString() == carid))
                    .OrderBy(c => c.Id).ToList(),
                CurrentCarId = carid
            };
            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            if (_orderRepository.Orders.FirstOrDefault(x => x.Id == id).Status == OrderStatus.InProgress)
            {
                TempData["message"] = string.Format("Order was not deleted: order is in progress");
            }
            else
            {
                Order deletedOrder = _orderRepository.DeleteOrder(id);
                if (deletedOrder != null)
                {
                    TempData["message"] = string.Format("Order {0} was deleted", deletedOrder.Id);
                }
            }
            var carid = _orderRepository.Orders.FirstOrDefault(x => x.Id == id).CarId;
            return RedirectToAction("OrderList", new
            {
                carid = carid,
                clientid = _carRepository.Cars.FirstOrDefault(x => x.Id == carid).ClientId
            });
        }

        public ViewResult Create(int carId)
        {
            ViewBag.AllStatuses = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            var orderServices = new List<OrderServiceViewModel>();

            foreach (var service in _orderRepository.ServiceTypes)
            {
                orderServices.Add(new OrderServiceViewModel
                {
                    ServiceId = service.Id
                });
            }

            return View("EditOrder", new OrderViewModel()
            {
                Order = new Order
                {
                    CarId = carId
                },
                OrderServices = orderServices.ToArray(),
                ServiceTypes = _orderRepository.ServiceTypes
            });
        }

        public ViewResult EditOrder(int id)
        {
            ViewBag.AllStatuses = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            var orderServices = new List<OrderServiceViewModel>();

            foreach (var service in _orderRepository.ServiceTypes)
            {
                var orderService = _orderRepository.OrderServices.FirstOrDefault(x => x.OrderId == id && x.ServiceTypeId == service.Id);
                orderServices.Add(new OrderServiceViewModel
                {
                    OrderId = id,
                    ServiceId = service.Id,
                    Note = orderService?.Note,
                    Checked = orderService != null
                });
            }

            return View("EditOrder", new OrderViewModel()
            {
                Order = _orderRepository.Orders.FirstOrDefault(x => x.Id == id),
                OrderServices = orderServices.ToArray(),
                ServiceTypes = _orderRepository.ServiceTypes
            });
        }

        [HttpPost]
        public ActionResult EditOrder(OrderViewModel orderVM)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    Id = orderVM.Order.Id,
                    Amount = orderVM.Order.Amount,
                    CarId = orderVM.Order.CarId,
                    Date = orderVM.Order.Date,
                    Status = orderVM.Order.Status
                };

                var orderServices = new List<OrderService>();

                foreach (var service in orderVM.OrderServices)
                {
                    if (service.Checked)
                    {
                        var orderService = new OrderService
                        {
                            OrderId = orderVM.Order.Id,
                            ServiceTypeId = service.ServiceId,
                            Note = service.Note
                        };
                        orderServices.Add(orderService);
                    }
                }

                _orderRepository.SaveOrder(order, orderServices);
                TempData["message"] = string.Format("Order has been saved");

                return RedirectToAction("OrderList", new
                {
                    carid = orderVM.Order.CarId,
                    clientid = _carRepository.Cars.FirstOrDefault(x => x.Id == orderVM.Order.CarId).ClientId
                });
            }
            else
            {
                return View(orderVM);
            }
        }
    }
}