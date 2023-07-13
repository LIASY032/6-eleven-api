using System;
using AutoMapper;
using Item.API.DTO;
using Item.API.Entities;

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

