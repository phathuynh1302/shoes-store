using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class ShoesService2 : vH.Interface.IShoesService
    {
        private readonly IShoesRepository _shoesRepository;
        public ShoesService2(IShoesRepository ShoesRepository)
        {
            this._shoesRepository = ShoesRepository;
        }
        public void AddShoes(Shoes shoes) => this._shoesRepository.Add(shoes);
        public Shoes GetShoesByShoesId(int id) => this._shoesRepository.GetFirstOrDefault(item => item.id == id);
        public void UpdateShoes(Shoes shoes)
        {
            _shoesRepository.RemoveCategoriesFromShoes(shoes);
            _shoesRepository.RemoveColorsFromShoes(shoes);
            this._shoesRepository.Update(shoes);
        }

        public void RemoveShoes(Shoes shoes) => this._shoesRepository.Remove(shoes);

        public Shoes GetShoesByName(string shoesName) => this._shoesRepository.GetFirstOrDefault(item => item.name.Equals(shoesName));

        public IEnumerable<Shoes> GetAllShoes() => (List<Shoes>)this._shoesRepository.GetAll();

        public void UpdateShoes(Shoes shoes, List<CategoryShoes> categoryShoes, List<ShoesColor> shoesColors)
        {
            _shoesRepository.RemoveCategoriesFromShoes(shoes);
            _shoesRepository.RemoveColorsFromShoes(shoes);
            shoes.CategoryShoes.Clear();
            shoes.ShoesColors.Clear();
            foreach (CategoryShoes item in categoryShoes)
            {
                shoes.CategoryShoes.Add(item);
            }
            foreach (ShoesColor item in shoesColors)
            {
                shoes.ShoesColors.Add(item);
            }
            this._shoesRepository.Update(shoes);
        }
    }
}
