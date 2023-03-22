using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class SizeService2 : vH.Interface.ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        public SizeService2(ISizeRepository SizeRepository)
        {
            this._sizeRepository = SizeRepository;
        }
        public void AddSize(Size size) => this._sizeRepository.Add(size);
        public Size GetSizeBySizeId(int id) => this._sizeRepository.GetFirstOrDefault(item => item.id == id);
        public void UpdateSize(Size size) => this._sizeRepository.Update(size);
        public void RemoveSize(Size size) => this._sizeRepository.Remove(size);

        public Size GetSizeByName(string sizeNumber) => this._sizeRepository.GetFirstOrDefault(item => item.sizeNumber.Equals(sizeNumber));

        public List<Size> GetAllSize() => (List<Size>)this._sizeRepository.GetAll();
    }
}
