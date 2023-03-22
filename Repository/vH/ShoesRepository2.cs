using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace PRN211_ShoesStore.Repository.vH
{
    public class ShoesRepository2 : vH.Repository<Shoes>, vH.Interface.IShoesRepository
    {
        private readonly AppDbContext _appDbContext;
        public ShoesRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public ICollection<Shoes> GetAll()
        {
            return _appDbContext.shoes.OrderByDescending(s=>s.id)
                        .Include(cs => cs.CategoryShoes)
                            .ThenInclude(c => c.category)
                        .Include(sc => sc.ShoesColors)
                            .ThenInclude(c => c.color)
                        .Include(sc => sc.SpecificallyShoes)
                            .ThenInclude(ss => ss.SpecificallyShoesSize)
                                .ThenInclude(sss => sss.size)
                        .Include(sc => sc.SpecificallyShoes)
                            .ThenInclude(ss => ss.ColorSpecificallyShoes)
                                .ThenInclude(sss => sss.color)
                        .Include(sc => sc.SpecificallyShoes)
                            .ThenInclude(ss=>ss.OrderDetails)
                        .ToList();
        }

        public void RemoveCategoriesFromShoes(Shoes shoes)
        {
            IEnumerable<CategoryShoes> categoryShoes = this._appDbContext.categoryShoes.Where(item => item.shoesId == shoes.id);
            this._appDbContext.categoryShoes.RemoveRange(categoryShoes);
            this._appDbContext.SaveChanges();
        }

        public void RemoveColorsFromShoes(Shoes shoes)
        {
            IEnumerable<ShoesColor> shoesColor = this._appDbContext.shoesColors.Where(item => item.shoesId == shoes.id);
            this._appDbContext.shoesColors.RemoveRange(shoesColor);
            this._appDbContext.SaveChanges();
        }
    }
}
