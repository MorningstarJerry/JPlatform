using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Dto;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUserDto> CheckUserPassword(string userName, string password);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<bool> UserIsInRole(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);

        Task<List<ApplicationRoleDto>> GetRolesAsync();

        Task<ApplicationUserDto> GetUserById(string userId);

        Task<IdentityResult> UpdateUserAvatarAsync(string userId, string gsm);

    }
}