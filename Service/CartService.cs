using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Service
{
	public class CartService : ICartService
	{
		IRepository<CartItem> _cartItemRepository;
		IRepository<Shoes> _shoesRepository;
		IRepository<SpecificallyShoes> _specificallyShoes;
		IRepository<CartItemDetails> _cartItemDetailsRepository;
		IRepository<SpecificallyShoesSize> _sizeRepository;
		IRepository<Size> _sizesRepository;
		//constructor
		public CartService(IRepository<CartItem> cartItemRepository, IRepository<Shoes> shoesRepository, IRepository<SpecificallyShoes> specificallyShoes, IRepository<CartItemDetails> cartItemDetailsRepository, IRepository<SpecificallyShoesSize> sizeRepository, IRepository<Size> sizesRepository)
		{
			_cartItemRepository = cartItemRepository;
			_shoesRepository = shoesRepository;
			_specificallyShoes = specificallyShoes;
			_cartItemDetailsRepository = cartItemDetailsRepository;
			_sizeRepository = sizeRepository;
			_sizesRepository = sizesRepository;
		}

		public IEnumerable<CartItem> GetCartItem()
		{

			return _cartItemRepository.GetData();
		}

		public IEnumerable<CartItemDetails> GetCartItemDetails()
		{
			List<CartItemDetails> cartItems = _cartItemDetailsRepository.GetData().ToList();
			List<SpecificallyShoes> shoesList = _specificallyShoes.GetData().ToList();
			List<SpecificallyShoes> specificallyShoesList = _specificallyShoes.GetData().ToList();
			List<Shoes> abstractShoes = _shoesRepository.GetData().ToList();
			List<CartItem> carts = _cartItemRepository.GetData().ToList();
			var res = from cartItem in cartItems
					  join shoes in shoesList on cartItem.specificallyShoesId equals shoes.id
					  join product in specificallyShoesList on shoes.id equals product.id
					  join shoesAb in abstractShoes on product.shoes.id equals shoesAb.id
					  join cart in carts on cartItem.cartItemId equals cart.Id
					  select new CartItemDetails(cartItem.Id, cart, shoes, cartItem.ShoesName, shoesAb.image, cartItem.Quantity, cartItem.Price, cartItem.ShoesSize);


			return res;
		}

		public CartItem GetCartItemById(int CartItemId)
		{
			return _cartItemRepository.GetById(CartItemId);
		}

		public IEnumerable<CartItem> GetCartItemByName(string CartItemname)
		{
			/*return _cartItemRepository.GetData(s => s.SpecificallyShoesnName == CartItemname);*/
			return null;
		}

		public bool UpdateCartItem(int cartItemId, int cartId, int quantity, int shoesId)
		{
			if (quantity < 0)
			{
				return false;
			}
			CartItem cart = _cartItemRepository.GetById(cartId);
			CartItemDetails cartItem = _cartItemDetailsRepository.GetById(cartItemId);
			SpecificallyShoes shoes = _specificallyShoes.GetById(cartItem.specificallyShoesId);
			if (quantity > shoes.quantity)
			{
				return false;
			}
			cartItem.Quantity = quantity;
			cartItem.Price = quantity * shoes.price;
			var updateCartItem = _cartItemDetailsRepository.Update(cartItem);
			if (updateCartItem)
			{
				List<CartItemDetails> cartDetails = _cartItemDetailsRepository.GetData().Where(c => c.cartItemId == cartId).ToList();
				decimal total = 0;
				foreach (var item in cartDetails)
				{
					total += item.Price;
				}
				cart.Price = total;
			}
			return _cartItemRepository.Update(cart);
		}

		public void addToCartItem(int UserId, int shoesId, decimal price, double sizeId, int quantity)
		{

			List<SpecificallyShoes> specificallyShoes = _specificallyShoes.GetData(s => s.shoes.id == shoesId).ToList();
			CartItem cart = new CartItem();
			CartItemDetails cartItemDetails = new CartItemDetails();
			CartItem existCart = _cartItemRepository.GetData().ToList().Where(c => c.userId == UserId).FirstOrDefault();
			List<CartItemDetails> productsCartIsExisted = _cartItemDetailsRepository.GetData().ToList().Where(c => c.cartItemId == existCart.Id).ToList();

			if (specificallyShoes.Count == 0)
			{
				throw new Exception($"Shoes id {shoesId} is not existed.");
			}
			Size sizes = _sizesRepository.GetData().Where(s => s.sizeNumber.Equals(sizeId.ToString())).FirstOrDefault();
			Shoes shoes = new Shoes();
			long quantitySpecShoes = 0;
			int shoesSpecId = 0;
			foreach (var item in specificallyShoes)
			{
				List<SpecificallyShoesSize> specificallyShoesSize = _sizeRepository.GetData(s => s.shoes.id == item.id && s.size.id == sizes.id).ToList();
				if (specificallyShoesSize.Count > 0)
				{
					shoes = _shoesRepository.GetById(shoesId);
					if (shoes != null)
					{
						quantitySpecShoes = item.quantity;
						shoesSpecId = item.id;
					}
				}

			}



			//SpecificallyShoesSize specificallyShoesSize = new SpecificallyShoesSize();
			/*foreach (var item in specificallyShoes)
            {*/


			/*}*/
			//Size sizeOfShoes = _sizesRepository.GetData().Where(s => s.id == specificallyShoesSize.sizeId).FirstOrDefault();

			if (quantity < 0)
			{
				throw new Exception("Quantity can not be a negative number.");
			}
			bool checkQuantity = quantity < quantitySpecShoes;
			if (checkQuantity == false)
			{
				throw new Exception("This shoes is sold out");
			}

			if (existCart != null)
			{

				foreach (var productCartIsExisted in productsCartIsExisted)
				{
					if (productCartIsExisted.specificallyShoesId == shoesSpecId && productCartIsExisted.ShoesSize == Double.Parse(sizes.sizeNumber))
					{
						productCartIsExisted.Quantity += quantity;
						productCartIsExisted.Price = productCartIsExisted.Quantity * price;
						decimal totalPrice = 0;
						_cartItemDetailsRepository.Update(productCartIsExisted);
						List<CartItemDetails> cartDetails = _cartItemDetailsRepository.GetData().Where(c => c.cartItemId == existCart.Id).ToList();
						existCart.Price = 0;
						if (cartDetails.Count > 1)
						{
							foreach (var product in cartDetails)
							{
								totalPrice += product.Price;
							}
						}

						if (cartDetails.Count == 1)
						{
							totalPrice = productCartIsExisted.Quantity * price;
						}

						existCart.Price = totalPrice;
						_cartItemRepository.Update(existCart);
						return;

					}
				}
				cartItemDetails.Price = price;
				cartItemDetails.Quantity = quantity;
				cartItemDetails.ShoesName = shoes.name;
				cartItemDetails.ShoesImg = shoes.image;
				cartItemDetails.ShoesSize = Double.Parse(sizes.sizeNumber);
				cartItemDetails.specificallyShoesId = shoesSpecId;
				cartItemDetails.cartItemId = existCart.Id;
				_cartItemDetailsRepository.Insert(cartItemDetails);
				List<CartItemDetails> items = _cartItemDetailsRepository.GetData().Where(c => c.cartItemId == existCart.Id).ToList();
				existCart.Price = 0;
				foreach (var i in items)
				{
					existCart.Price += i.Price;
				}
				_cartItemRepository.Update(existCart);
				return;
			}
			cart.userId = UserId;
			cart.Price = price;
			bool res = _cartItemRepository.Insert(cart);
			if (res)
			{
				CartItem newCart = _cartItemRepository.GetData().First();
				cartItemDetails.Price = price;
				cartItemDetails.Quantity = quantity;
				cartItemDetails.ShoesName = shoes.name;
				cartItemDetails.ShoesImg = shoes.image;
				cartItemDetails.ShoesSize = Double.Parse(sizes.sizeNumber);
				cartItemDetails.specificallyShoesId = shoesSpecId;
				cartItemDetails.cartItemId = newCart.Id;
				_cartItemDetailsRepository.Insert(cartItemDetails);
			}
		}

		public void DeleteCartItem(int cartItemId, int cartId)
		{
			CartItemDetails cartItem = _cartItemDetailsRepository.GetData().ToList().Where(i => i.Id == cartItemId).FirstOrDefault();
			CartItem cart = _cartItemRepository.GetById(cartId);
			if (cartItem != null)
			{
				_cartItemDetailsRepository.Delete(cartItem);
			}

			List<CartItemDetails> list = _cartItemDetailsRepository.GetData().Where(i => i.cartItemId == cartId).ToList();
			decimal price = 0;
			if (list.Count > 0)
			{
				foreach (var item in list)
				{
					price += item.Price;
				}
				cart.Price = price;
				_cartItemRepository.Update(cart);
			}
			if (list.Count() == 0)
			{
				_cartItemRepository.Delete(cart);
			}
		}
	}
}