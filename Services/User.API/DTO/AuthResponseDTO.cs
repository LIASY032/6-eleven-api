using System;
namespace User.API.DTO
{
	public class AuthResponseDTO
	{
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
        public Boolean isAdmin{ get; set; }
        public IList<string> devices{ get; set; }
    }
}

