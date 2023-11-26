using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

            if (_user.Status == StatusCode.Pending) {
                _logger.LogWarning($"User with email {loginDto.Email} is pending");

                return null;
            }

            foreach (UserCartItem item in _user.Carts) {
                var userItem = loginDto.Carts.Where(l => l.Id == item.Id);
  //              if (userItem.) { 
		//}
	    
	    }

            return new AuthResponseDTO
            {
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<string> CreateRefreshToken()
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.RefreshToken);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await CreateRefreshToken();
                return new AuthResponseDTO
                {
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken(),
                    isAdmin = _user.IsAmin,
                    devices = _user.Devices
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        public Task<UserData> GetUserById(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public Task AddDevices(string device)
        {
            _user.Devices.Add(device);
            return null;

        }

        public Task AddItem()
        {
            throw new NotImplementedException();
        }

        public string checkDeviceExisting(UserData userData, string device) {
            return userData.Devices.FirstOrDefault(device);
	
	}

        public Boolean checkPending(UserData userData, string device)
        {
            return userData.Status == StatusCode.Pending;

        }
    }
}

