using System.Linq;
using System.Web.Mvc;

using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;
using ServiceStation.WebUI.Models;

namespace ServiceStation.WebUI.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private ICarRepository _repository;
        private IOrderRepository _orderRepository;

        public CarController(ICarRepository carRepository, IOrderRepository orderRepository)
        {
            _repository = carRepository;
            _orderRepository = orderRepository;
        }

        public ViewResult List(string clientid = null)
        {
            CarsListViewModel viewModel = new CarsListViewModel
            {
                Cars = _repository.Cars
                    .Where(c => c.ClientId.ToString() == clientid)
                    .OrderBy(c => c.Id),
                CurrentClientId = clientid
            };

            return View(viewModel);
        }

        public ViewResult EditCar(int id, int? selectedMake = null)
        {
            var car = _repository.Cars.FirstOrDefault(p => p.Id == id);
            var model = _repository.Models.FirstOrDefault(m => m.Id == car.ModelId);
            var make = _repository.Makes.FirstOrDefault(m => m.Id == (selectedMake ?? model.MakeId));

            ViewBag.AllModels = new SelectList(_repository.Models
                .Where(m => m.MakeId == make.Id), "Id", "Name", model.Id);
            ViewBag.AllMakes = new SelectList(_repository.Makes, "Id", "Name", make.Id);

            var carVM = new CarViewModel
            {
                Car = car,
                Model = model,
                Make = make
            };

            return View(carVM);
        }

        public ViewResult Create(int clientId, int? selectedMake = null)
        {
            ViewBag.AllMakes = new SelectList(_repository.Makes, "Id", "Name");
            ViewBag.AllModels = new SelectList(_repository.Models
                   .Where(m => m.MakeId == (selectedMake ?? _repository.Makes.First().Id)), "Id", "Name");

            var make = _repository.Makes.FirstOrDefault(m => m.Id == (selectedMake ?? m.Id));

            return View("EditCar", new CarViewModel()
            {
                Car = new Car
                {
                    ClientId = clientId
                },
                Make = make,
                Model = new Model(),
            });
        }

        [HttpPost]
        public ActionResult EditCar(CarViewModel carVM)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Id = carVM.Car.Id,
                    ClientId = carVM.Car.ClientId,
                    ModelId = carVM.Model.Id,
                    VIN = carVM.Car.VIN,
                    Year = carVM.Car.Year
                };

                _repository.SaveCar(car);
                TempData["message"] = string.Format("{0} has been saved", car.VIN);
                return RedirectToAction("List");
            }
            else
            {
                var model = _repository.Models.FirstOrDefault(m => m.Id == carVM.Model.Id);
                ViewBag.AllModels = new SelectList(_repository.Models
                    .Where(m => m.MakeId == carVM.Make.Id), "Id", "Name", model.Id);
                ViewBag.AllMakes = new SelectList(_repository.Makes, "Id", "Name", carVM.Make.Id);

                return View(carVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_orderRepository.Orders.Where(x => ((x.CarId == id) && (x.Status == OrderStatus.InProgress))).Count() > 0) 
            {
                TempData["message"] = string.Format("Car was not deleted: order in progress exists");
            }
            else
            {
                Car deletedCar = _repository.DeleteCar(id);
                if (deletedCar != null)
                {
                    TempData["message"] = string.Format("{0} was deleted", deletedCar.VIN);
                }
            }
            return RedirectToAction("List");
        }
    }
}