using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        bool Insert(T entity);

        bool Update(T entity);

        IEnumerable<T> GetData(Expression<Func<T, bool>> expression = null);

        //shoes
        IEnumerable<Shoes> GetShoesByCategoryId(int categoryId);
        bool Delete(T entity);
    }
}
