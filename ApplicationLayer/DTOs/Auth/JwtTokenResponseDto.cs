namespace ApplicationLayer.DTOs.Auth
{
    public class JwtTokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiryTime { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public string Jti { get; set; } = string.Empty;
    }
}
