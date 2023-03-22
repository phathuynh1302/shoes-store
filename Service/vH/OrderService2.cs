using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;
using System.Linq;

namespace PRN211_ShoesStore.Service.vH
{
    public class OrderService2 : vH.Interface.IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        public OrderService2(IOrderRepository OrderRepository)
        {
            this._OrderRepository = OrderRepository;
        }
        public void AddOrder(Order Order) => this._OrderRepository.Add(Order);
        public Order GetOrderByOrderId(int id) => this._OrderRepository.GetFirstOrDefault(item => item.orderId == id);
        public void UpdateOrder(Order Order) => this._OrderRepository.Update(Order);
        public void RemoveOrder(Order Order) => this._OrderRepository.Remove(Order);

        public Order GetOrderByUserId(int userId) => this._OrderRepository.GetFirstOrDefault(item => item.userId== userId);

        public List<Order> GetAllOrder()
        {
            return (List<Order>)this._OrderRepository.GetAll(includeProperties: "user", 
                options: item => item.OrderByDescending(o => o.createDate).ToList());
        }
    }
}
