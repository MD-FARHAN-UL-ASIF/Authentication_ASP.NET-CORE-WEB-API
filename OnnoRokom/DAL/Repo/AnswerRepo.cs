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
    public class AnswerRepo : Repo, IAnswerRepo<Answer, int, Answer>
    {
        public AnswerRepo(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public async Task<Answer> Create(Answer obj)
        {
            db.Answers.Add(obj);
            if (await db.SaveChangesAsync() > 0) return obj;
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var exobj = await Get(id);
            if (exobj != null)
            {
                db.Answers.Remove(exobj);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Answer>> Get()
        {
            return await db.Answers.ToListAsync();
        }

        public async Task<Answer> Get(int id)
        {
            return await db.Answers.FindAsync(id);
        }

        public async Task<List<Answer>> GetByQuestionId(int questionId)
        {
            return await db.Answers.Where(q => q.QuestionId == questionId).ToListAsync();
        }

        public async Task<List<Answer>> GetByUserId(int userId)
        {
            return await db.Answers.Where(q => q.UserId == userId).ToListAsync();
        }

        public async Task<Answer> Update(Answer obj)
        {
            var exObj = await Get(obj.Id);
            if (exObj != null)
            {
                exObj.Body = obj.Body;
                exObj.CreationDate = obj.CreationDate;
                exObj.QuestionId = obj.QuestionId;
                exObj.UserId = obj.UserId;

                db.Answers.Update(exObj);
                if (await db.SaveChangesAsync() > 0) return obj;
            }
            return null;
        }
    }
}
