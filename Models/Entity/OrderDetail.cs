using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public long quantity { get; set; }

        [Required]
        public double price { get; set; }

        [Required]
        public bool status { get; set; }

        public int specificallyShoesId { get; set; }

        public int orderId { get; set; }

        [ForeignKey("specificallyShoesId")]
        public SpecificallyShoes shoes { get; set; }

        [ForeignKey("orderId")]
        public Order order { get; set; }

    }
}
