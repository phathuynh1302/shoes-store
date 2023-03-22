using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;

namespace PRN211_ShoesStore.Repository.vH
{
    public class ColorRepository2 : vH.Repository<Color>, vH.Interface.IColorRepository
    {
        private readonly AppDbContext AppDbContext;
        public ColorRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this.AppDbContext = appDbContext;
        }
    }
}
