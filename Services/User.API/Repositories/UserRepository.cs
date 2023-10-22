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
        public async Task<IEnumerable<IdentityError>> Register(UserDTO userDto)
        {
            var user =await  _userManager.FindByEmailAsync(userDto.Email);
            if (user != null ) {
                _logger.LogWarning($"User with email {userDto.Email} was found");

                return null;
            }
            var result = await _userManager.CreateAsync(_mapper.Map<UserData>(userDto), userDto.Password);
            if (result.Succeeded)
            {
                if (userDto.IsAmin) { 

                await _userManager.AddToRoleAsync(_user, "Admin");
                }
                else { 

                await _userManager.AddToRoleAsync(_user, "User");
		}
            }

            return result.Errors;

        }

        public async Task<AuthResponseDTO> Login(UserDTO loginDto)
        {
            _logger.LogInformation($"looking for user with email {loginDto.Email}");
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || isValidUser == false)
            {

                _logger.LogWarning($"User with email {loginDto.Email} was not found");

                return null;
            }

            return new AuthResponseDTO
            {
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
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

