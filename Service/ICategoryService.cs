using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service
{
	public interface ICategoryService
	{
		public IEnumerable<Category> GetCategories();
	}
}
