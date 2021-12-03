using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albelli.Business.Helper
{
    public static class OrderHelper
    {

        public static string NameBuilder(string name)
        {
            return name.Trim().ToLower().Replace(" ", "");
        }

        public static SubmitOrderInput MergeProducts(SubmitOrderInput model)
        {
            model.Items = model.Items.GroupBy(x => x.Product).Select(t => new OrderInformation
            {
                Product = t.Key,
                Quantity = t.Sum(ta => ta.Quantity),
            }).ToList();

            return model;
        }
    }
}
