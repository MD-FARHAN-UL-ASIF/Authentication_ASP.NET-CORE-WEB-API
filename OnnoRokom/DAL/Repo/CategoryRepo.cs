using DAL.EF;
using DAL.EF.Models;
using DAL.iINTERFACES;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public class CategoryRepo : Repo, IRepo<Category, int, Category>
    {
        public CategoryRepo(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public async Task<Category> Create(Category obj)
        {
            db.Categories.Add(obj);
            if (await db.SaveChangesAsync() > 0) return obj;
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var exobj = await Get(id);
            if (exobj != null)
            {
                db.Categories.Remove(exobj);
                return await db.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Category>> Get()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await db.Categories.FindAsync(id);
        }

        public async Task<Category> Update(Category obj)
        {
            var exObj = await Get(obj.Id);
            if (exObj != null)
            {
                exObj.Name = obj.Name;
                db.Categories.Update(exObj);
                if (await db.SaveChangesAsync() > 0) return obj;
            }
            return null;
        }
    }
}
