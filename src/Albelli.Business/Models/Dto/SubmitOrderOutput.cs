using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Models.Dto
{
    public class SubmitOrderOutput
    {
        public int OrderId { get; set; }
        public double MinimumBinWidth { get; set; }
    }
  
}
