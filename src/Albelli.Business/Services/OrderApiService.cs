using Albelli.Business.Helper;
using Albelli.Business.Models;
using Albelli.Business.Models.Dto;
using Albelli.Business.Services.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

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

        public GetOrderOutput GetOrder(int orderId)
        {
            var output = new GetOrderOutput();
            var order = _orderDataService.GetOrder(orderId);
            if (order == null) 
                return null;

            var details = _mapper.Map<List<OrderDetail>>(order.Items);
            output.MinimumBinWidth = order.MinimumBinWidth;
            output.OrderId = order.Id;
            output.Items = details;
            return output;
        }
        public SubmitOrderOutput SaveOrder(SubmitOrderInput model)
        {
            var output = new SubmitOrderOutput();


            model = OrderHelper.MergeProducts(model);
          
            var products = _orderDataService.GetProductTypes();
            var items = new List<OrderItemModel>();

            var ordermodel = new OrderModel() {
                MinimumBinWidth = OrderHelper.CalculateBinWidth(model, products) ,
                Items=items
            };
            var order = _orderDataService.SaveOrder(ordermodel);


            for (int i = 0; i < model.Items.Count; i++)
            {
                items.Add(new OrderItemModel()
                {
                    OrderId = order.Id,
                    ProductTypeId = products.First(x => OrderHelper.NameBuilder(x.Name) == OrderHelper.NameBuilder(model.Items[i].Product)).Id,
                    Quantity = model.Items[i].Quantity
                });
            }
            var orderItems = _orderDataService.SaveOrderItems(items);

            output.OrderId = order.Id;
            output.MinimumBinWidth = order.MinimumBinWidth;
            return output;
        }
       
    }
}
