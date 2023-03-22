using PRN211_ShoesStore.Models.Entity;

namespace PRN211_ShoesStore.Models.DTO
{
    public class CategoryViewDto
    {
        public Category Category { get; set; }
        public long TotalSell { get; set; }
        public long TotalProduct { get; set; }
    }
}
