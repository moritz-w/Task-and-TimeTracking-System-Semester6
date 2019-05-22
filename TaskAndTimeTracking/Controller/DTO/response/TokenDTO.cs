using System;

namespace TaskAndTimeTracking.Controller.DTO.response
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpirationTimeUTC { get; set; }
    }
}