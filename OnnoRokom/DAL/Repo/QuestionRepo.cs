using DAL.EF;
using DAL.EF.Models;
using DAL.iINTERFACES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class QuestionRepo : Repo, IQuestionRepo <Question, int, Question>
    {
        public QuestionRepo(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public async Task<Question> Create(Question obj)
        {
            db.Questions.Add(obj);
            if (await db.SaveChangesAsync() > 0) return obj;
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var exobj = await Get(id);
            if (exobj != null)
            {
                db.Questions.Remove(exobj);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Question>> Get()
        {
            return await db.Questions.ToListAsync();
        }

        public async Task<Question> Get(int id)
        {
            return await db.Questions.FindAsync(id);
        }

        public async Task<Question> Update(Question obj)
        {
            var exObj = await Get(obj.Id);
            if (exObj != null)
            {
                exObj.Title = obj.Title;
                exObj.Body = obj.Body;
                exObj.CreatedDate = obj.CreatedDate;
                exObj.CategoryId = obj.CategoryId;
                exObj.UserId = obj.UserId;

                db.Questions.Update(exObj);
                if (await db.SaveChangesAsync() > 0) return obj;
            }
            return null;
        }

        public async Task<List<Question>> GetByUserId(int userId)
        {
            return await db.Questions.Where(q => q.UserId == userId).ToListAsync();
        }

        public async Task<List<Question>> GetByCategoryId(int categoryId)
        {
            return await db.Questions.Where(q => q.CategoryId == categoryId).ToListAsync();
        }

    }
}
