using System;
namespace User.API.DTO
{
	public class AuthResponseDTO
	{
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreAhToken { get; set; }
        public Boolean isAdmin{ get; set; }
        public List<string> devices{ get; set; }
    }
}

