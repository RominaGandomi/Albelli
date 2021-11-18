using Albelli.Business.Helper;
using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace OrderTest
{
    [TestClass]
    public class BinCalculatorTest
    {
        List<ProductTypeModel> _products = new List<ProductTypeModel>();

        [TestInitialize]
        public void SetUp()
        {
            _products.Add(new ProductTypeModel() { Name = "photoBook", Width = 19 });
            _products.Add(new ProductTypeModel() { Name = "calendar", Width = 10 });
            _products.Add(new ProductTypeModel() { Name = "canvas", Width = 16 });
            _products.Add(new ProductTypeModel() { Name = "cards", Width = 4.7 });
            _products.Add(new ProductTypeModel() { Name = "mug", Width = 94 });
        }

        [TestMethod]
        public void DublicateProductsTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 5 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });

            var result = OrderHelper.CalculateBinWidth(OrderHelper.MergeProducts(order), _products);

            var expectedValue = (6 * 16) + (94);
            Assert.AreEqual(expectedValue,result);
        }

        [TestMethod]
        public void NullProductTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "null", Quantity = 5 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });

            var result = OrderHelper.CalculateBinWidth(OrderHelper.MergeProducts(order), _products);

            var expectedValue = (1 * 16) + (94);
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void MugOrderTest()
        {
            var order = new SubmitOrderInput();
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 2 });
            order.Items.Add(new OrderInformation() { Product = "canvas", Quantity = 1 });
            order.Items.Add(new OrderInformation() { Product = "mug", Quantity = 3 });

            var result = OrderHelper.CalculateBinWidth(OrderHelper.MergeProducts(order), _products);

            var expectedValue = (1 * 16) + (2 * 94);
            Assert.AreEqual(expectedValue, result);
        }
    }

}
