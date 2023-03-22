using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("SpecificallyShoesSale   ")]
    public class SpecificallyShoesSale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int specificallyShoesId { get; set; }

        public int saleId { get; set; }

        [ForeignKey("specificallyShoesId")]
        public SpecificallyShoes shoes { get; set; }

        [ForeignKey("saleId")]
        public Sale sale { get; set; }

        
    }
}
