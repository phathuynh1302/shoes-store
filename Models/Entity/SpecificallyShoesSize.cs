using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("SpecificallyShoesSize")]
    public class SpecificallyShoesSize
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }




        public int specificallyShoesId { get; set; }

        [ForeignKey("specificallyShoesId")]
        public SpecificallyShoes shoes { get; set; }

        public int sizeId { get; set; }

        [ForeignKey("sizeId")]
        public Size size { get; set; }
    }
}
