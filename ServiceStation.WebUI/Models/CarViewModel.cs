using ServiceStation.Domain.Entities;

namespace ServiceStation.WebUI.Models
{
    public class CarViewModel
    {
        public Car Car { get; set; }
        public Model Model { get; set; }
        public Make Make { get; set; }
    }
}