using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN211_ShoesStore.Models.Entity
{
	[Table("CartItemDetails")]
	public class CartItemDetails
	{


		public CartItemDetails(int id, CartItem cartItem, SpecificallyShoes specificallyShoes, string shoesName, string image, int quantity, decimal price, double shoesSize)
		{
			Id = id;
			CartItem = cartItem;
			SpecificallyShoes = specificallyShoes;
			ShoesName = shoesName;
			ShoesImg = image;
			Quantity = quantity;
			Price = price;
			ShoesSize = shoesSize;
		}

		public CartItemDetails()
		{
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public int cartItemId { get; set; }

		public int specificallyShoesId { get; set; }

		[ForeignKey("cartItemId")]
		public CartItem CartItem { get; set; }

		[ForeignKey("specificallyShoesId")]
		public SpecificallyShoes SpecificallyShoes { get; set; }

		public double ShoesSize { get; set; }
		public string ShoesName { get; set; }
		public string ShoesImg { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}