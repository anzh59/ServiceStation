using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ServiceStation.Domain.Entities
{
    public class Car
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ModelId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ClientId { get; set; }
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the VIN")]
        public string VIN { get; set; }
    }
}
