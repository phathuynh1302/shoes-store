using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("ShoesColor")]
    public class ShoesColor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int shoesId { get; set; }

        public int colorId { get; set; }

        [ForeignKey("shoesId")]
        public Shoes shoes { get; set; }

        public bool status { get; set; }

        [ForeignKey("colorId")]
        public Color color { get; set; }
    }
}
