using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Item.API.DTO
{
	public class ProductDTO
	{
        public string ?Id { get; set; }
        public string? Image { get; set; }

        public string Title { get; set; }


        public string Info { get; set; }

        public decimal Price { get; set; }
        public string CollectionType { get; set; }
        public Boolean? WeeklyDeal { get; set; } = false;
    }
}

