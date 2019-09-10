using System.Web.Mvc;

namespace ServiceStation.Domain.Entities
{
    public class Make
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
