using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface ICategoryService
    {
        public void AddCategory(Category category);
        public List<Category> GetAllCategory();
        public Category GetCategoryByCategoryId(int id);
        public Category GetCategoryByName(string categoryName);
        public void UpdateCategory(Category category);
        public void RemoveCategory(Category category);
    }
}
