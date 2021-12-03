using Albelli.Business.Helper;
using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using Albelli.Business.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albelli.Business.Services
{
    public class OrderApiService : IOrderApiService
    {
        private readonly IMapper _mapper;
        private readonly IOrderDataService _orderDataService;

        public OrderApiService(IMapper mapper, IOrderDataService orderDataService)
        {
            _mapper = mapper;
            _orderDataService = orderDataService;
        }

        public async Task<GetOrderOutput> GetOrder(int orderId)
        {
            try
            {
                var output = await _orderDataService.GetOrder(orderId);
                return output;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<SubmitOrderOutput> SaveOrder(SubmitOrderInput model)
        {
            try
            {
                //Merge if multiple products has been inserted
                model = OrderHelper.MergeProducts(model);

                var products = await _orderDataService.GetProductTypes();

                //Calculate the minimum bin width and OrderItems
                var width = 0.0;
                var items = new List<OrderItemModel>();

                foreach (var item in model.Items)
                {
                    var product = products.FirstOrDefault(x => OrderHelper.NameBuilder(x.Name) == OrderHelper.NameBuilder(item.Product));
                    if (product == null)
                        continue;

                    items.Add(new OrderItemModel()
                    {
                        ProductTypeId = product.Id,
                        Quantity = item.Quantity
                    });

                    width += product.CalculateBin(item.Quantity);
                }

                var ordermodel = new OrderModel()
                {
                    MinimumBinWidth = width,
                    Items = items
                };

                var order = await _orderDataService.SaveOrder(ordermodel);

                return new SubmitOrderOutput()
                {
                    OrderId = order.Id,
                    MinimumBinWidth = order.MinimumBinWidth
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
