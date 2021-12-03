using Albelli.Business.Helper;
using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace OrderTest
{
    [TestClass]
    public class BinCalculatorTest
    {
        IList<ProductTypeModel> _products = new List<ProductTypeModel>();

        [TestInitialize]
        public void SetUp()
        {

            _products.Add(new PhotoBook() { Name = "PhotoBook", Width = 19 });
            _products.Add(new Calendar() { Name = "Calendar", Width = 10 });
            _products.Add(new Canvas() { Name = "Canvas", Width = 16 });
            _products.Add(new Cards() { Name = "Cards", Width = 4.7 });
            _products.Add(new Mug() { Name = "Mug", Width = 94 });
        }

        [TestMethod]
        public void DublicateProductsTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 5 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });

            var expectedValue = (6 * 16) + (94);
            Assert.AreEqual(expectedValue, Calculate(order));
        }

        [TestMethod]
        public void NullProductTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "null", Quantity = 5 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });


            var expectedValue = (1 * 16) + (94);
            Assert.AreEqual(expectedValue, Calculate(order));
        }

        [TestMethod]
        public void MugOrderTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 3 });


            var expectedValue = (1 * 16) + (2 * 94);
            Assert.AreEqual(expectedValue, Calculate(order));
        }


        public double Calculate(SubmitOrderInput order)
        {
            var width = 0.0;

            order = OrderHelper.MergeProducts(order);
            var items = new List<OrderItemModel>();

            foreach (var item in order.Items)
            {
                var product = _products.FirstOrDefault(x => OrderHelper.NameBuilder(x.Name) == OrderHelper.NameBuilder(item.Product));
                if (product == null)
                    continue;

                width += product.CalculateBin(item.Quantity);
            }

            return width;

        }
    }

}
