using ServiceStation.Domain.Entities;
using System.Collections.Generic;

namespace ServiceStation.WebUI.Models
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string CurrentClientId { get; set; }
    }
}