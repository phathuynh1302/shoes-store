using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("ColorSpecificallyShoes")]
    public class ColorSpecificallyShoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int specificallyShoesId { get; set; }

        [ForeignKey("specificallyShoesId")]
        public SpecificallyShoes shoes { get; set; }

        public int colorId { get; set; }

        public bool status { get; set; }

        [ForeignKey("colorId")]
        public Color color { get; set; }

    }
}
