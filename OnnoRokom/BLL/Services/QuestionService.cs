using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.IServices;
using DAL;
using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public QuestionService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Question, QuestionDTO>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<Response> Create(QuestionDTO questionDTO)
        {
                try
                {
                    var question = _mapper.Map<Question>(questionDTO);
                    var data = await DataAccessFactory.QuestionData(_dbContextOptions).Create(question);
                    return new Response(HttpStatusCode.Created, "Question created successfully", _mapper.Map<QuestionDTO>(data));
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
                var isSuccess = await DataAccessFactory.QuestionData(_dbContextOptions).Delete(id);
                if (isSuccess)
                    return new Response(HttpStatusCode.OK, "Question deleted successfully");
                else
                    return new Response(HttpStatusCode.NotFound, "Question not found");
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
                var data = await DataAccessFactory.QuestionData(_dbContextOptions).Get();
                var mapped = _mapper.Map<List<QuestionDTO>>(data);
                return new Response(HttpStatusCode.OK, "Questions retrieved successfully", mapped);
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
                var data = await DataAccessFactory.QuestionData(_dbContextOptions).Get(id);
                if (data != null)
                {
                    var mapped = _mapper.Map<QuestionDTO>(data);
                    return new Response(HttpStatusCode.OK, "Question retrieved successfully", mapped);
                }
                else
                {
                    return new Response(HttpStatusCode.NotFound, "Question not found");
                }
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
                var data = await DataAccessFactory.QuestionData(_dbContextOptions).GetByUserId(userId);
                var mapped = _mapper.Map<List<QuestionDTO>>(data);
                return new Response(HttpStatusCode.OK, "Questions retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Update(QuestionDTO questionDTO)
        {
            try
            {
                var question = _mapper.Map<Question>(questionDTO);
                var data = await DataAccessFactory.QuestionData(_dbContextOptions).Update(question);
                return new Response(HttpStatusCode.OK, "Question updated successfully", _mapper.Map<QuestionDTO>(data));
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> GetByCategoryId(int categoryId)
        {
            try
            {
                var data = await DataAccessFactory.QuestionData(_dbContextOptions).GetByCategoryId(categoryId);
                var mapped = _mapper.Map<List<QuestionDTO>>(data);
                return new Response(HttpStatusCode.OK, "Questions retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

    }
}
