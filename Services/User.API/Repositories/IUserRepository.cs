using System;
using Microsoft.AspNetCore.Identity;
using User.API.DTO;
using User.API.Entities;

namespace User.API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityError>> Register(UserDTO userDto);
        Task<AuthResponseDTO> Login(UserDTO loginDto);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);
        Task<UserData> GetUserById(string id);
        Task AddDevices(string device);
        Task AddItem();
    }
}

