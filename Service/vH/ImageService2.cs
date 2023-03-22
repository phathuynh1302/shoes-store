using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class ImageService2 : vH.Interface.IImageService
    {
        private readonly IImageRepository _ImageRepository;
        public ImageService2(IImageRepository ImageRepository)
        {
            this._ImageRepository = ImageRepository;
        }
        public void AddImage(Image Image) => this._ImageRepository.Add(Image);
        public Image GetImageByImageId(int id) => this._ImageRepository.GetFirstOrDefault(item => item.id == id);
        public void UpdateImage(Image Image) => this._ImageRepository.Update(Image);
        public void RemoveImage(Image Image) => this._ImageRepository.Remove(Image);

        public Image GetImageByName(string ImageName) => this._ImageRepository.GetFirstOrDefault((System.Linq.Expressions.Expression<System.Func<Image, bool>>)(item => (bool)item.image.Equals((object)ImageName)));

        public List<Image> GetAllImage() => (List<Image>)this._ImageRepository.GetAll();
    }
}
