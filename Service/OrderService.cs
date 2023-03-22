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

        public void CheckOut(int? userId, int cartItemId, decimal totalPrice)
        {
            if (totalPrice < 0)
            {
                throw new Exception("You can not checkout because cart is empty.");
            }

            List<CartItemDetails> cartItemDetails = _cartItemDetailsRepository.GetData(cd => cd.CartItem.Id == cartItemId).ToList();
            foreach (var cartItem in cartItemDetails)
            {
                SpecificallyShoes SpecShoes =  _specificallyShoes.GetById(cartItem.specificallyShoesId);
                if (cartItem.Quantity > SpecShoes.quantity)
                {
                    throw new Exception("Your shoes quantity is larger than quantity's shoes available.");
                }
            }
            Order order = new Order();
            order.userId = userId.Value;
            order.price = totalPrice;
            order.status = 0;
            order.createDate= DateTime.Now;
            bool res = _OrderRepository.Insert(order);
            if (res)
            {
                order = _OrderRepository.GetData().Last();
                
                foreach (var cartItem in cartItemDetails)
                {
                    SpecificallyShoes SpecShoes = _specificallyShoes.GetById(cartItem.specificallyShoesId);
                    if (cartItem.Quantity > SpecShoes.quantity)
                    {
                        throw new Exception("Your shoes quantity is larger than quantity's shoes available.");
                    }
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.price = (double) cartItem.Price;
                    orderDetail.quantity = cartItem.Quantity;
                    orderDetail.orderId = order.orderId;
                    orderDetail.specificallyShoesId = cartItem.specificallyShoesId;
                    orderDetail.status = 1;
                    _OrderDetailRepository.Insert(orderDetail);
                }
                foreach (var cartItem in cartItemDetails)
                {
                    SpecificallyShoes SpecShoes = _specificallyShoes.GetById(cartItem.specificallyShoesId);
                    SpecShoes.quantity -= cartItem.Quantity;
                    _specificallyShoes.Update(SpecShoes);
                }

                foreach (var cartItem in cartItemDetails)
                {
                    _cartItemDetailsRepository.Delete(cartItem);
                }
                
                _cartItemRepository.Delete(_cartItemRepository.GetById(cartItemId));
            }
            else
            {
                throw new Exception("");
            }

        }

        public List<OrderDetail> ViewOrder(int? userId)
        {
            List<OrderDetail> orderDetails = _OrderDetailRepository.GetData(o => o.order.user.id == userId.Value && o.order.status == 1).ToList();
            //List<OrderDetail> ordersOut = null;
            
            if (orderDetails.Count > 0)
            {
                return orderDetails;
            }

            return null;
        }

        /*public Order CreateOrder(int userID, decimal price)
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
            _OrderRepository.Insert(CreateOrder(UserID, 0));
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
                        IEnumerable<Order> od = _OrderRepository.GetData(s => s.userId == UserID);
                        CreateOrderDetail(x.Quantity, (double)x.Price,x.specificallyShoesId,od.FirstOrDefault().orderId);
                        Sshoe.quantity = Sshoe.quantity - x.Quantity;
                        shoes.quantity = Sshoe.quantity;
						_specificallyShoes.Update(Sshoe);
                        _ShoesRepository.Update(shoes);
                        _cartItemDetailsRepository.Delete(x);

                    }
				}
			}
            if (itemDetails.Count == 0)
            {
                _cartItemRepository.Delete(item.FirstOrDefault());
            }
            return outItem;
        }*/

        /*        private void CreateOrderDetail(int quantity, decimal price, int specificallyShoesId, int orderId)
                {
                    throw new NotImplementedException();
                }*/




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
