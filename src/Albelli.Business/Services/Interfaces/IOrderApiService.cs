using Albelli.Business.Models.Dto;
using System.Threading.Tasks;

namespace Albelli.Business.Services.Interfaces
{
    public interface IOrderApiService
    {
        public Task<GetOrderOutput> GetOrder(int orderId);
        public Task<SubmitOrderOutput> SaveOrder(SubmitOrderInput model);
    }
}
