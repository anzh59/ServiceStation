using ServiceStation.Domain.Abstract;
using ServiceStation.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ServiceStation.WebUI.Controllers
{
    [Authorize]
    public class NavController : Controller
    {
        private IClientRepository _repository;

        public NavController(IClientRepository repo)
        {
            _repository = repo;
        }
        public PartialViewResult Menu(int? clientid = null, string clientName = "")
        {
            ViewBag.SelectedClient = clientid;
            ViewBag.ClientName = clientName;

            IEnumerable<Client> clients = _repository.Clients
                .Where(x => string.IsNullOrEmpty(clientName) || $"{x.FirstName} {x.LastName}"
                    .ToUpperInvariant().Contains(clientName.ToUpperInvariant()))
                .OrderBy(x => $"{x.FirstName} {x.LastName}");

            return PartialView(clients);
        }

        public ViewResult EditClient(int id)
        {
            var client = _repository.Clients
                .FirstOrDefault(p => p.Id == id);
            return View(client);
        }

        [HttpPost]
        public ActionResult EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveClient(client);
                TempData["message"] = string.Format("{0} has been saved", client.FirstName);
                return RedirectToAction("List", "Car");
            }
            else
            {
                return View(client);
            }
        }
        
        public ViewResult CreateClient()
        {
            return View("EditClient", new Client());
        }
        
        public ActionResult DeleteClient(int id)
        {
            Client deletedClient = _repository.DeleteClient(id);
            if (deletedClient != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedClient.FirstName);
            }
            return RedirectToAction("List", "Car");
        }
    }
}