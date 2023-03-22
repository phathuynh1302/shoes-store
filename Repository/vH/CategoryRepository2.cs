using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;

namespace PRN211_ShoesStore.Repository.vH
{
    public class CategoryRepository2: vH.Repository<Category>, vH.Interface.ICategoryRepository
    {
        private readonly AppDbContext AppDbContext;
        public CategoryRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this.AppDbContext = appDbContext;
        }
    }
}
