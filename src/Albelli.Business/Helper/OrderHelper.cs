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
        public static double CalculateBinWidth(SubmitOrderInput model, List<ProductTypeModel> products)
        {
            var width = 0.0;
            try
            {
                for (int i = 0; i < model.Items.Count; i++)
                {
                    var product = products.FirstOrDefault(x => x.Name == NameBuilder(model.Items[i].Product));
                    if (product == null)
                        continue;

                    width += NameBuilder(model.Items[i].Product) == "mug" ?
                        (Math.Ceiling(model.Items[i].Quantity / 4.0) * product.Width)
                        :
                         (model.Items[i].Quantity * product.Width);
                }
                return width;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
