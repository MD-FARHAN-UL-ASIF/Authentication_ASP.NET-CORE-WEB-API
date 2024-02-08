using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ICategoryService
    {
        Task<Response> Get();
        Task<Response> GetById(int id);
        Task<Response> Create(CategoryDTO categoryDTO);
        Task<Response> Update(CategoryDTO categoryDTO);
        Task<Response> Delete(int id);
    }
}
