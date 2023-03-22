/*namespace PRN211_ShoesStore.Service
{
    public class OrderService
    {
    }
}*/

using Microsoft.AspNetCore.Http;
using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace PRN211_ShoesStore.Service
{
    public class OrderService : IOrderService
    {
        //private UserRepository _userRepository;
        IRepository<CartItem> _cartItemRepository;
        IRepository<SpecificallyShoes> _specificallyShoes;
        IRepository<CartItemDetails> _cartItemDetailsRepository;
        IRepository<Order> _OrderRepository;
        IRepository<OrderDetail> _OrderDetailRepository;
		IRepository<Shoes> _ShoesRepository;
		//constructor
		public OrderService(IRepository<CartItem> cartItemRepository, 
                            IRepository<Shoes> shoesRepository, 
                            IRepository<SpecificallyShoes> specificallyShoes, 
                            IRepository<CartItemDetails> cartItemDetailsRepository,
                            IRepository<Order> OrderRepository,
                            IRepository<OrderDetail> OrderDetailRepository,
			                IRepository<Shoes> ShoesRepository
                            //UserRepository userRepository
                            )
        {
            _cartItemRepository = cartItemRepository;
            _specificallyShoes = specificallyShoes;
            _cartItemDetailsRepository = cartItemDetailsRepository;
            _OrderRepository = OrderRepository;
            _OrderDetailRepository = OrderDetailRepository;
            _ShoesRepository = ShoesRepository;
            //_userRepository = userRepository;


        }

        public Order CreateOrder(int userID, decimal price)
        {
            Order order = new Order();
            order.userId = userID;
            order.price = price;
            order.createDate = DateTime.Now;
            order.status = true;
            _OrderRepository.Insert(order);
            return order;

		}

		public bool CreateOrderDetail(long Qunatity, double price, int specificallyShoesId, int orderId)
		{
			OrderDetail orderDetail = new OrderDetail();
			orderDetail.quantity = Qunatity;
            orderDetail.price = price;
            orderDetail.orderId = orderId;
            orderDetail.status = true;
            
			orderDetail.specificallyShoesId= specificallyShoesId;
			return _OrderDetailRepository.Insert(orderDetail);
		}

		public List<CartItemDetails> checkQuantity(int UserID)
        {
            bool check = true;
			IEnumerable<CartItem> item = _cartItemRepository.GetData(s => s.userId == UserID);
            List<CartItemDetails> itemDetails = (List<CartItemDetails>)_cartItemDetailsRepository.GetData(s => s.cartItemId == item.FirstOrDefault().Id);
            List<CartItemDetails> outItem = new List<CartItemDetails>();
			foreach (var x in itemDetails)
            {
				SpecificallyShoes Sshoe = _specificallyShoes.GetById(x.specificallyShoesId);
                Shoes shoes = _ShoesRepository.GetById(Sshoe.shoesId);
				if (Sshoe.quantity - x.Quantity < 0)
                {
                    outItem.Add(x);
				}
                else
                {
                    if (outItem.Count() == 0)
                    {
                        Sshoe.quantity = Sshoe.quantity - x.Quantity;
                        shoes.quantity = Sshoe.quantity;
						_specificallyShoes.Update(Sshoe);
                        _ShoesRepository.Update(shoes);
                        _cartItemDetailsRepository.Delete(x);

                    }
				}
			}
            return outItem;
        }

        


        /*public IEnumerable<CartItemDetails> GetCartItemDetails()
        {
            List<CartItemDetails> cartItems = _cartItemDetailsRepository.GetData().ToList();
            List<Shoes> shoesList = _specificallyShoes.GetData().ToList();
            List<Shoes> specificallyShoesList = _specificallyShoes.GetData().ToList();
            List<CartItem> carts = _cartItemRepository.GetData().ToList();
            var res = from cartItem in cartItems
                      join shoes in shoesList on cartItem.shoesId equals shoes.id
                      join product in specificallyShoesList on shoes.id equals product.id
                      join cart in carts on cartItem.cartItemId equals cart.Id
                      select new CartItemDetails(cartItem.Id, cart, shoes, cartItem.ShoesName, shoes.image, cartItem.Quantity, cartItem.Price);


            return res;
        }*/
    }
}
