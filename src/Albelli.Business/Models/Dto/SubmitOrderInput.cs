using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Models.Dto
{
    public class SubmitOrderInput
    {
        public SubmitOrderInput()
        {
            Items = new List<OrderInformation>();
        }
        public List<OrderInformation> Items {get; set;}
    }
    public class OrderInformation
    {
        public int Quantity { get; set; }
        public string Product { get; set; }
    }
}
