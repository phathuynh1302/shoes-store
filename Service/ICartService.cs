using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PRN211_ShoesStore.Service
{
	public interface ICartService
	{
		IEnumerable<CartItem> GetCartItem();

		CartItem GetCartItemById(int CartItemId);

		IEnumerable<CartItem> GetCartItemByName(string CartItemname);

		bool UpdateCartItem(int cartItemId, int cartId, int quantity, int shoesId);

		public IEnumerable<CartItemDetails> GetCartItemDetails();

		public void addToCartItem(int UserId, int shoesId, decimal price, double sizeId, int quantity);

		void DeleteCartItem(int cartItemId, int cartId);
	}
}