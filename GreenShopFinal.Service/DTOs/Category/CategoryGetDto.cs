namespace GreenShopFinal.Service.DTOs
{
    public record CategoryGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }

    }
}
