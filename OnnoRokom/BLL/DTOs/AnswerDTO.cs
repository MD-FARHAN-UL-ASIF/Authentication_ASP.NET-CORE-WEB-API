using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
