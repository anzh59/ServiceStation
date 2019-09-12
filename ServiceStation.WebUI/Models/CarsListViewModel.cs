using System.Collections.Generic;

using ServiceStation.Domain.Entities;

namespace ServiceStation.WebUI.Models
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public string CurrentClientId { get; set; }
    }
}