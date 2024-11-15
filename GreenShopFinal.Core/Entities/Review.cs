using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Entities.Base;
using GreenShopFinal.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenShopFinal.Core.Entities
{
    public class Review : BaseEntity
    {

        [ForeignKey(nameof(BaseUser))]
        public string UserId { get; set; }
        public BaseUser BaseUser { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public Rating Rating { get; set; }

    }
}
