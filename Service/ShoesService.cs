using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Service
{
    public class ShoesService : IShoesService
    {
        IRepository<Shoes> _shoesRepository;
        IRepository<Category> _categoryRepository;
        
        //constructor
        public ShoesService(IRepository<Shoes> shoesRepository, IRepository<Category> categoryRepository)
        {
            _shoesRepository = shoesRepository;
            _categoryRepository = categoryRepository;
        }

        // shoes == doi giay
        //Get list of shoes
        public IEnumerable<Shoes> GetShoes()
        {
            return _shoesRepository.GetData();
        }

        //Get shoes by id
        public Shoes GetShoesById(int shoesId)
        {
            return _shoesRepository.GetById(shoesId);
        }

        //Get shoes by name
        public IEnumerable<Shoes> GetShoesByName(string shoesname)
        {
            // Build an expression that filters the data based on the name
            return _shoesRepository.GetData(s => s.name == shoesname);
        }

        //Update [NumberInStock] when buying [quantity] shoes (MinusNumberInStockWithQuantity)
        // Ex: stock: 100                         quantity:2  --> stock: 88
        public bool UpdateShoes(Shoes shoes)
        {
            //ham nay chua code
            return false;
        }

		//Get Shoes by category
		public IEnumerable<Shoes> GetShoesByCategoryId(int categoryId)
		{
			List<Shoes> shoes = _shoesRepository.GetShoesByCategoryId(categoryId).ToList();
            return shoes;

		}
	}
}
