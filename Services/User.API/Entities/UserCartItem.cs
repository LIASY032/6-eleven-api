using System;
namespace User.API.Entities
{
	public class UserCartItem
	{
        public int Id { get; set; }
        public string? Title{ get; set; }
        public double? price{ get; set; }
        public int Count{ get; set; }
    }
}

