using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Repositories;
using GreenShopFinal.Data.Context;

namespace GreenShopFinal.Data.Repositories.Concretes
{
    public class BaseImageRepository : Repository<BaseImage>, IBaseImageRepository
    {
        public BaseImageRepository(GreenShopFinalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
