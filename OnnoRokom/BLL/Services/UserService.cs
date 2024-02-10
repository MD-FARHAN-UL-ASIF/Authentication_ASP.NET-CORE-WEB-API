using Azure.Core;
using BLL.DTOs;
using BLL.IServices;
using DAL.EF.Models;
using DAL.iINTERFACES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<UserRole> _userRoleRepo;
        private readonly IBaseRepo<Role> _roleRepo;
        private readonly ITokenService _tokenService;

        public UserService(IBaseRepo<User> userRepo,
            IBaseRepo<UserRole> userRoleRepo,
            IBaseRepo<Role> roleRepo,
            ITokenService tokenService)
        {
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
            _tokenService = tokenService;
        }

        public async Task<Response> Register(UserRegistrationDTO userRegistrationDTO, string roleName)
        {
            // Hash password using BCrypt
            string password = BCrypt.Net.BCrypt.HashPassword(userRegistrationDTO.Password);

            var existingUser = await _userRepo.Find(x => x.Email == userRegistrationDTO.Email);
            if (existingUser != null && existingUser.Count>0)
            {
                return new Response(HttpStatusCode.BadRequest, "Email already registered");
            }

            User user = new User()
            {
                Name = userRegistrationDTO.Name,
                Email = userRegistrationDTO.Email,
                PhoneNumber = userRegistrationDTO.PhoneNumber,
                Designation = userRegistrationDTO.Designation,
                Institution = userRegistrationDTO.Institution,
                Password = password
            };

            _userRepo.Add(user);
            if (await _userRepo.SaveChangesAsync(user))
            {
                var role = (await _roleRepo.Find(x => x.RoleName == roleName)).FirstOrDefault();

                if (role != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };

                    _userRoleRepo.Add(userRole);
                    await _userRoleRepo.SaveChangesAsync(userRole);

                    return new Response(HttpStatusCode.OK, "Registered Success", userRegistrationDTO);
                }
                else
                {
                    return new Response(HttpStatusCode.BadRequest, $"Role '{roleName}' not found");
                }
            }
            else
            {
                return new Response(HttpStatusCode.BadRequest, "Registration Failed..!");
            }
        }

        public async Task<Response> Login(UserLoginDTO loginDTO)
        {
            var existingUser = await _userRepo.Find(x => x.Email == loginDTO.Email);

            if (existingUser == null || existingUser.Count == 0)
            {
                return new Response(HttpStatusCode.Unauthorized, "Invalid user email");
            }

            var user = existingUser.First();
            var isVlidPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);

            if (!isVlidPassword)
            {
                return new Response(HttpStatusCode.Unauthorized, "Invalid password");
            }

            var accessToken = await _tokenService.GetToken(user.Email);

            if (string.IsNullOrEmpty(accessToken))
            {
                return new Response(HttpStatusCode.Unauthorized, "Invalid password or Email");
            }

            var userRoles = await GetUserRoles(user.Id);

            LoginResult loginResult = new LoginResult
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Designation = user.Designation,
                Institution = user.Institution,
                AccessToken = accessToken,
                Roles = userRoles
            };

            return new Response(HttpStatusCode.OK, "Authorized", loginResult);
        }

        private async Task<List<string>> GetUserRoles(int userId)
        {
            var userRoles = await _userRoleRepo.query().Include(ur => ur.Role).Where(ur => ur.UserId == userId).ToListAsync();
            var roles = userRoles.Select(ur => ur.Role.RoleName).ToList();
            return roles;
        }

        public async Task<Response> GetUser(string userId)
        {
            var userWithRoles = await _userRepo.query().Include(u => u.UserRole).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Id == Convert.ToInt32(userId));
            if (userWithRoles == null)
            {
                return new Response(HttpStatusCode.NotFound, "User not found");
            }

            var response = new
            {
                Id = userWithRoles.Id,
                Name = userWithRoles.Name,
                Email = userWithRoles.Email,
                PhoneNumber = userWithRoles.PhoneNumber,
                Designation = userWithRoles.Designation,
                Institution = userWithRoles.Institution,
                Roles = userWithRoles.UserRole.Select(ur => ur.Role.RoleName).ToList()
            };

            return new Response(HttpStatusCode.OK, "Success", response);
        }

        public async Task<Response> GetAllUser()
        {
            var _users = await _userRepo.GetAll();
            return new Response(HttpStatusCode.OK, "Success", _users);
        }

    }
}
