using System;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        //Ne kadar zaman geçerli bu token için Expiration süresini belirliyoruz
        //Daha sonra bu süre dışında biz ek olarak refresh token falan da ekleyebılırız
    }
}
