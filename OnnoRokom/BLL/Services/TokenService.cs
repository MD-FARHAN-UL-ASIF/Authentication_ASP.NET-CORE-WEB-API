using AutoMapper.Configuration;
using BLL.IServices;
using DAL.EF.Models;
using DAL.iINTERFACES;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<UserRole> _userRoleRepo;
        private readonly IBaseRepo<Role> _roleRepo;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public TokenService(IBaseRepo<User> userRepo,
            IBaseRepo<UserRole> userRoleRepo,
            IBaseRepo<Role> roleRepo,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _userRepo = userRepo;
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
            _configuration = configuration;
        }

        public async Task<string> GetToken(string email)
        {
            var existingUser = (await _userRepo.Find(x => x.Email == email)).FirstOrDefault();
            if (existingUser == null)
                return null;

            var roles = await GetUserRoles(existingUser.Id);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, existingUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, existingUser.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<List<string>> GetUserRoles(int userId)
        {
            var userRoles = await _userRoleRepo.query().Include(ur => ur.Role).Where(ur => ur.UserId == userId).ToListAsync();
            var roles = userRoles.Select(ur => ur.Role.RoleName).ToList();
            return roles;
        }
    }
}
