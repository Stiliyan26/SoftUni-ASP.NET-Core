using JwtAuth.Constants;

namespace JwtAuth.Models
{
    public class GetTokenRequest
    {
        public string UserName { get; set; } = Consts.UserName;
        public string Password { get; set; } = Consts.Password;
    }
}
