using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
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
    public class CategoryService : ICategoryService
    {
        private static readonly IMapper mapper;
        private readonly DbContextOptions<DataContext> dbContextOptions;

        static CategoryService()
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
                c.CreateMap<CategoryDTO, Category>();
            });
            mapper = cfg.CreateMapper();
        }

        public CategoryService(DbContextOptions<DataContext> options)
        {
            dbContextOptions = options;
        }

        public async Task<Response> Get()
        {
            try
            {
                var data = await DataAccessFactory.CategoryData(dbContextOptions).Get();
                var mapped = mapper.Map<List<CategoryDTO>>(data);
                return new Response(HttpStatusCode.OK, "Categories retrieved successfully", mapped);
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
                var data = await DataAccessFactory.CategoryData(dbContextOptions).Get(id);
                var mapped = mapper.Map<CategoryDTO>(data);
                return new Response(HttpStatusCode.OK, "Category retrieved successfully", mapped);
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Create(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                var data = await DataAccessFactory.CategoryData(dbContextOptions).Create(category);
                return new Response(HttpStatusCode.Created, "Category created successfully", mapper.Map<CategoryDTO>(data));
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        public async Task<Response> Update(CategoryDTO categoryDTO)
        {
            try
            {
                var category = mapper.Map<Category>(categoryDTO);
                var data = await DataAccessFactory.CategoryData(dbContextOptions).Update(category);
                return new Response(HttpStatusCode.OK, "Category updated successfully", mapper.Map<CategoryDTO>(data));
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
                var isSuccess = await DataAccessFactory.CategoryData(dbContextOptions).Delete(id);
                if (isSuccess)
                    return new Response(HttpStatusCode.OK, "Category deleted successfully");
                else
                    return new Response(HttpStatusCode.NotFound, "Category not found");
            }
            catch (Exception ex)
            {
                return new Response(HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
