using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;

namespace GreenShopFinal.Data.Repositories.Concretes
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(GreenShopFinalDbContext greenShopFinalDbContext) : base(greenShopFinalDbContext)
        {

        }
    }
}
