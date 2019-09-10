using System.Web.Mvc;

namespace ServiceStation.Domain.Entities
{
    public class Model
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int MakeId { get; set; }
    }
}
