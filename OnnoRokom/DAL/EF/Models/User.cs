using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Models
{
    public  class User
    {
        public User() 
        {
            UserRole = new HashSet<UserRole>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Designation { get; set; }


        [Required]
        public string Institution { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
