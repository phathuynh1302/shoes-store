using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service
{
	public class CategoryService : ICategoryService
	{
		IRepository<Category> _categoryRepository;
		
		//constructor
		public CategoryService(IRepository<Category> categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}
		// CartItem == doi giay
		//Get list of CartItem
		public IEnumerable<Category> GetCategories()
		{
			return _categoryRepository.GetData();
		}

		
	}
}
