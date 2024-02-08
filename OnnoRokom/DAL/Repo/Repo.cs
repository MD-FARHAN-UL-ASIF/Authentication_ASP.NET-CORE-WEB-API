using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repo
{
    public class Repo
    {
        protected DataContext db;

        public Repo(DbContextOptions<DataContext> options)
        {
            db = new DataContext(options);
        }
    }
}
