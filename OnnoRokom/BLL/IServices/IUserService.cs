using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IUserService
    {
        Task<Response> Register(UserRegistrationDTO userRegistrationDTO, string roleName);

        Task<Response> Login(UserLoginDTO loginDTO);

        Task<Response> GetUser(string userId);

        Task<Response> GetAllUser();
    }
}
