using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using User.API.DTO;
using User.API.Entities;

namespace User.API.Repositories
{
	public class UserRepository:IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserData> _userManager;
        private readonly IConfiguration _configuration;
        private UserData _user;

        private readonly ILogger<UserRepository> _logger;
        public UserRepository(IMapper mapper, UserManager<UserData> userManager, IConfiguration configuration, ILogger<UserRepository> logger)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
            _logger = logger;
        }
        public Task<IEnumerable<IdentityError>> Register(UserDTO userDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> Login(UserDTO loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<UserData> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public Task AddDevices(string device)
        {
            throw new NotImplementedException();
        }

        public Task AddItem()
        {
            throw new NotImplementedException();
        }
    }
}

