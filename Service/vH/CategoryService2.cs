using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class CategoryService2 : vH.Interface.ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService2(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public void AddCategory(Category category) => this._categoryRepository.Add(category);
        public Category GetCategoryByCategoryId(int id) => this._categoryRepository.GetFirstOrDefault(item => item.id==id);
        public void UpdateCategory(Category Category) => this._categoryRepository.Update(Category);
        public void RemoveCategory(Category Category) => this._categoryRepository.Remove(Category);

        public Category GetCategoryByName(string categoryName) => this._categoryRepository.GetFirstOrDefault(item => item.name.Equals(categoryName));

        public List<Category> GetAllCategory() => (List<Category>)this._categoryRepository.GetAll();
    }
}
