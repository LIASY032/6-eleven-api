using System;
using AutoMapper;

namespace Item.API.Configurations
{
	public class MapperConfig : Profile
    {
		public MapperConfig()
		{
			CreateMap<Product, ProductDTO>().ReverseMap();
		}
	}
}

