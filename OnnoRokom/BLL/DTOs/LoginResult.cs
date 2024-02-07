using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class LoginResult
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Designation { get; set; }

        public string Institution { get; set; }

        public string AccessToken { get; set; }

        public List<string> Roles { get; set; }

    }
}
