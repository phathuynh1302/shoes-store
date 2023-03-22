using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IColorService
    {
        public void AddColor(Color color);
        public List<Color> GetAllColor();
        public Color GetColorByColorId(int id);
        public Color GetColorByName(string colorName);
        public void UpdateColor(Color color);
        public void RemoveColor(Color color);
    }
}
