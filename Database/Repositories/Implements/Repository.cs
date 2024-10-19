using Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.Implements
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context context;
        private readonly DbSet<T> dbSet;

        public Repository(Context context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T item)
        {
            dbSet.Add(item);
            context.SaveChanges();
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            T item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
            context.SaveChanges();
        }
    }
}
