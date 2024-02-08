using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
