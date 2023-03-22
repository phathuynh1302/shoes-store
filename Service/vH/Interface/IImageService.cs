using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IImageService
    {
        public void AddImage(Image image);
        public List<Image> GetAllImage();
        public Image GetImageByImageId(int id);
        public Image GetImageByName(string imageName);
        public void UpdateImage(Image image);
        public void RemoveImage(Image image);
    }
}
