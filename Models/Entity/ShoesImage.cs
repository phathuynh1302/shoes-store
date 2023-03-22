using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("ShoesImage")]
    public class ShoesImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int imageId { get; set; }

        [ForeignKey("imageId")]
        public Image image { get; set; }

        public int shoesId { get; set; }

        public bool status { get; set; }

        [ForeignKey("shoesId")]
        public Shoes shoes { get; set; }
    }
}
