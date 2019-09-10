using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.Domain.Entities
{
    public class OrderService
    {
        public int OrderId { get; set; }
        public int ServiceTypeId { get; set; }
        public string Note { get; set; }
    }
}
