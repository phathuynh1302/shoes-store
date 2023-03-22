using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Models.DTO
{
    public class CreateSpecificShoesDto
    {
        public int id { get; set; }

        public string Name
        {
            get; set;
        }

        public decimal Price { get; set; }

        public long Quantity { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int ShoesId { get; set; }
        public bool Status { get; set; }
    }
}
