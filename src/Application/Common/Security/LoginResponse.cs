using System;

namespace Application.Common.Security
{
    public class LoginResponse
    {
        public string uid { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public DateTime expiration { get; set; }
        public string refresh_token { get; set; }
    }
}
