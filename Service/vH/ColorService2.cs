using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class ColorService2 : vH.Interface.IColorService
    {
        private readonly IColorRepository _colorRepository;
        public ColorService2(IColorRepository ColorRepository)
        {
            this._colorRepository = ColorRepository;
        }
        public void AddColor(Color color) => this._colorRepository.Add(color);
        public Color GetColorByColorId(int id) => this._colorRepository.GetFirstOrDefault(item => item.Id == id);
        public void UpdateColor(Color color) => this._colorRepository.Update(color);
        public void RemoveColor(Color color) => this._colorRepository.Remove(color);

        public Color GetColorByName(string colorName) => this._colorRepository.GetFirstOrDefault(item => item.Name.Equals(colorName));

        public List<Color> GetAllColor() => (List<Color>)this._colorRepository.GetAll();
    }
}
