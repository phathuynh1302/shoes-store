using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;

namespace PRN211_ShoesStore.Repository.vH
{
    public class SizeRepository2 : vH.Repository<Size>, vH.Interface.ISizeRepository
    {
        private readonly AppDbContext AppDbContext;
        public SizeRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this.AppDbContext = appDbContext;
        }
    }
}
