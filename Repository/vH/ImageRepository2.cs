using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;

namespace PRN211_ShoesStore.Repository.vH
{
    public class ImageRepository2 : vH.Repository<Image>, vH.Interface.IImageRepository
    {
        private readonly AppDbContext AppDbContext;
        public ImageRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this.AppDbContext = appDbContext;
        }
    }
}
