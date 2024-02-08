using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IQuestionService
    {
        Task<Response> Get();
        Task<Response> GetById(int id);
        Task<Response> Create(QuestionDTO questionDTO);
        Task<Response> Update(QuestionDTO questionDTO);
        Task<Response> Delete(int id);
        Task<Response> GetByUserId(int userId);
        Task<Response> GetByCategoryId(int categoryId);

    }
}
