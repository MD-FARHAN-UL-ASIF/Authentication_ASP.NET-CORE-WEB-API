using BLL.DTOs;


namespace BLL.IServices
{
    public interface IAnswerService
    {
        Task<Response> Get();
        Task<Response> GetById(int id);
        Task<Response> Create(AnswerDTO answerDTO);
        Task<Response> Update(AnswerDTO answerDTO);
        Task<Response> Delete(int id);
        Task<Response> GetByUserId(int userId);
        Task<Response> GetByQuestionId(int questionId);

    }
}
