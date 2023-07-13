using System;
using Microsoft.AspNetCore.Identity;

namespace User.API.Entities
{
    public enum StatusCode
    {
        Pending,
        Active
    }
    public class User:IdentityUser
	{
		public bool IsAmin { get; set; } = false;
        public IList<string>? Devices { get; set; } = new List<string>();
        public StatusCode? Status { get; set; } = StatusCode.Pending;

    }
}

   

