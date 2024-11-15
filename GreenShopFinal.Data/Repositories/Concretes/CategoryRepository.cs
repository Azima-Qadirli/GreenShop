using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Data.Repositories.Concretes
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(GreenShopFinalDbContext greenShopFinalDbContext):base(greenShopFinalDbContext) 
        {
            
        }
    }
}
