using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Repository.vH.Interface
{
    public interface IShoesRepository : vH.Interface.IRepository<Shoes>
    {
        ICollection<Shoes> GetAll();
        void RemoveColorsFromShoes(Shoes shoes);
        void RemoveCategoriesFromShoes(Shoes shoes);

    }
}
