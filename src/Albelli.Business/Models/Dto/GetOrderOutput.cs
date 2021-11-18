using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Models.Dto
{
    public class GetOrderOutput
    {
        public int OrderId { get; set; }
        public double MinimumBinWidth { get; set; }
        public List<OrderDetail> Items { get; set; }
    }

    public class OrderDetail
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}
