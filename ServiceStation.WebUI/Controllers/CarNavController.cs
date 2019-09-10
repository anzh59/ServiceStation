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
    public class CarNavController : Controller
    {
        private ICarRepository _repository;

        public CarNavController(ICarRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult CarMenu(string clientId = null, string carId = null)
        {
            ViewBag.SelectedCar = carId;

            var cars = _repository.Cars.Where(x => clientId == null || x.ClientId.ToString() == clientId)
                .ToList();

            List<CarViewModel> resultCars = new List<CarViewModel>();

            foreach (var car in cars)
            {
                var model = _repository.Models.FirstOrDefault(m => m.Id == car.ModelId);
                var make = _repository.Makes.FirstOrDefault(m => m.Id == model.MakeId);

                var carVM = new CarViewModel
                {
                    Car = car,
                    Model = model,
                    Make = make
                };

                resultCars.Add(carVM);
            }

            return PartialView(resultCars);
        }
    }
}