using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IShoesService
    {
        public void AddShoes(Shoes shoes);
        public IEnumerable<Shoes> GetAllShoes();
        public Shoes GetShoesByShoesId(int id);
        public Shoes GetShoesByName(string shoesName);
        public void UpdateShoes(Shoes shoes);
        public void UpdateShoes(Shoes shoes, List<CategoryShoes> categoryShoes, List<ShoesColor> shoesColors);
        public void RemoveShoes(Shoes shoes);
    }
}
