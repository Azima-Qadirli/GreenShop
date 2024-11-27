namespace GreenShopFinal.Service.DTOs.Auth
{
    public record AdminLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
