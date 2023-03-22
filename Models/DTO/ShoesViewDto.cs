using PRN211_ShoesStore.Models.DTO;
using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.DTO
{
	public class ShoesViewDto
    {
        public IEnumerable<Shoes> Shoes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public CreateShoesDto CreateShoesDto { get; set; }
        public CreateSpecificShoesDto CreateSpecificShoesDto { get; set; }
        public Dictionary<int, long> QuantitySoldByShoesId;
    }
}
