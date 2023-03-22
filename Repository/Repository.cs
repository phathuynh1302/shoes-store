using Microsoft.EntityFrameworkCore.ChangeTracking;
using PRN211_ShoesStore.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        AppDbContext _appDbContext;


        public Repository(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        //Get entities with expression
        public IEnumerable<T> GetData(Expression<Func<T, bool>> expression = null)
        {
            try
            {
                if (expression == null)
                {
                    return _appDbContext.Set<T>().ToList();
                }
                return _appDbContext.Set<T>().Where(expression).ToList();
            }
            catch (Exception ex)
            {
                // handle the exception or log it
                Console.WriteLine(ex.ToString());
                // return an empty collection or re-throw the exception
                return Enumerable.Empty<T>();
            }
        }


        public T GetById(int id)
        {
            try
            {
                return _appDbContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool Insert(T entity)
        {
            try
            {
                _appDbContext.Set<T>().Add(entity);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                EntityEntry entityEntry = _appDbContext.Entry<T>(entity);
                entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete (T entity)
        {
            try
            {
                EntityEntry entityEntry = _appDbContext.Entry<T>(entity);
                entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public IEnumerable<Shoes> GetShoesByCategoryId(int categoryId)
		{
			try
			{
				var shoes = from cs in _appDbContext.categoryShoes
							where cs.categoryId == categoryId && cs.status == true
							select cs.shoes;

				return shoes.ToList();
			}
			catch (Exception ex)
			{
				// handle the exception or log it
				Console.WriteLine(ex.ToString());
				// return an empty collection or re-throw the exception
				return Enumerable.Empty<Shoes>();
			}
		}
	}
}
