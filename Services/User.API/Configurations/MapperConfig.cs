using System;
using AutoMapper;
using User.API.DTO;
using User.API.Entities;

namespace Item.API.Configurations
{
	public class MapperConfig : Profile
    {
		public MapperConfig()
		{
			CreateMap<UserDTO, UserData>().ReverseMap();
			CreateMap<LoginDto, UserData>().ReverseMap();
			CreateMap<AuthResponseDTO, UserData>().ReverseMap();
		}
	}
}

