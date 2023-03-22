using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN211_ShoesStore.Models.Entity
{
	[Table("CartItem")]
	public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		public int userId { get; set; }

		[ForeignKey("userId")]
        public User User { get; set; }

        public decimal Price { get; set; }
    }
}