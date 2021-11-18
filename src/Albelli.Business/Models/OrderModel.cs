using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public double MinimumBinWidth { get; set; }

        public List<OrderItemModel> Items { get; set; }
    }
}
