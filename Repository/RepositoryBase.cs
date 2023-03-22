using Microsoft.EntityFrameworkCore;
using PRN211_ShoesStore.Models;
using System.Linq;

namespace PRN211_ShoesStore.Repository
{
    public class RepositoryBase<T> where T: class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public RepositoryBase()
        {
            _context = new AppDbContext();
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {

            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetByName(string name)
        {
            return _dbSet.Find(name);
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            //_dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
