using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("CategoryShoes")]
    public class CategoryShoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }

        public int shoesId { get; set; }

        [ForeignKey("shoesId")]
        public Shoes shoes { get; set; }

        public bool status { get; set; }
    }
}
