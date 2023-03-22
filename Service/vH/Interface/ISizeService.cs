using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface ISizeService
    {
        public void AddSize(Size size);
        public List<Size> GetAllSize();
        public Size GetSizeBySizeId(int id);
        public Size GetSizeByName(string sizeName);
        public void UpdateSize(Size size);
        public void RemoveSize(Size size);
    }
}
