using Albelli.Data.Entities;
using Albelli.Data.Interfaces.Services;
using Albelli.Data.Repositories;

namespace Albelli.Data.Services
{
    public class ProductTypeService : Repository<ProductType>, IProductTypeService
    {
        public ProductTypeService(AlbelliDbContext context) : base(context)
        {
        }
    }
}
