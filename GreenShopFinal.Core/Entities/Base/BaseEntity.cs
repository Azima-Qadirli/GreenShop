﻿namespace GreenShopFinal.Core.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
