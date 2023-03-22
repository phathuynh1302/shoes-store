using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace PRN211_ShoesStore.Service
{
    public interface IOrderService
    {
		/*Order CreateOrder(int userID, decimal price);
		bool CreateOrderDetail(long Qunatity, double price, int specificallyShoesId, int orderId);
		List<CartItemDetails> checkQuantity(int UserID);*/

		public void CheckOut(int? userId, int cartItemId, decimal totalPrice);

        public List<OrderDetail> ViewOrder(int? userId);
    }
}
