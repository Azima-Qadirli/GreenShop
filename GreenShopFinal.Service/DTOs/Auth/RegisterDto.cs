﻿namespace GreenShopFinal.Service.DTOs.Auth
{
    public record RegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
