using System.ComponentModel.DataAnnotations;

namespace ServiceStation.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        
        public int ModelId { get; set; }
        
        public int ClientId { get; set; }

        [DisplayFormat(DataFormatString = "{0:####}", ApplyFormatInEditMode = true)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter the VIN")]
        public string VIN { get; set; }
    }
}
