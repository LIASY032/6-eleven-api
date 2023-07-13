using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Item.API.Entities
{
    public class Product
	{
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }




        [BsonElement("image")]
        public string? Image{ get; set; }

        [BsonElement("title")]
        public string Title { get; set; }


        [BsonElement("info")]
        public string Info{ get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
        [BsonElement("collectionType")]
        public string CollectionType{ get; set; }
        [BsonElement("weeklyDeal")]
        public Boolean? WeeklyDeal { get; set; } = false;
        [BsonElement("__v")]
        public int? v{ get; set; } 








    }

}

