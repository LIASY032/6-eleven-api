using System;
using System.ComponentModel.DataAnnotations;
using User.API.Entities;

namespace User.API.DTO
{
	public class UserDTO
	{
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public bool IsAmin { get; set; } = false;
        public IList<UserCartItem> Carts { get; set; }
    }
}

