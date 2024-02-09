using AutoMapper;
using BLL.DTOs;
using BLL.IServices;
using DAL;
using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public AnswerService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Answer, AnswerDTO>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<Response> Create(AnswerDTO answerDTO)
        {
            try
            {
                var answer = _mapper.Map<Answer>(answerDTO);
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).Create(answer);
                return new Response(HttpStatusCode.Created, "Answer created successfully", _mapper.Map<AnswerDTO>(data));
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                var isSuccess = await DataAccessFactory.AnswerData(_dbContextOptions).Delete(id);
                if (isSuccess)
                    return new Response(HttpStatusCode.OK, "Answer deleted successfully");
                else
                    return new Response(HttpStatusCode.NotFound, "Answer not found");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Get()
        {
            try
            {
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).Get();
                var mapped = _mapper.Map<List<AnswerDTO>>(data);
                return new Response(HttpStatusCode.OK, "Answer retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> GetById(int id)
        {
            try
            {
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).Get(id);
                if (data != null)
                {
                    var mapped = _mapper.Map<AnswerDTO>(data);
                    return new Response(HttpStatusCode.OK, "Answer retrieved successfully", mapped);
                }
                else
                {
                    return new Response(HttpStatusCode.NotFound, "Answer not found");
                }
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> GetByQuestionId(int questionId)
        {
            try
            {
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).GetByQuestionId(questionId);
                var mapped = _mapper.Map<List<AnswerDTO>>(data);
                return new Response(HttpStatusCode.OK, "Answers retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> GetByUserId(int userId)
        {
            try
            {
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).GetByUserId(userId);
                var mapped = _mapper.Map<List<AnswerDTO>>(data);
                return new Response(HttpStatusCode.OK, "Answer retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Update(AnswerDTO answerDTO)
        {
            try
            {
                var answer = _mapper.Map<Answer>(answerDTO);
                var data = await DataAccessFactory.AnswerData(_dbContextOptions).Update(answer);
                return new Response(HttpStatusCode.OK, "Answer updated successfully", _mapper.Map<AnswerDTO>(data));
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
