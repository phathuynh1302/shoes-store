using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IOrderService
    {
        public void AddOrder(Order Order);
        public List<Order> GetAllOrder();
        public Order GetOrderByOrderId(int id);
        public Order GetOrderByUserId(int userId);
        public void UpdateOrder(Order Order);
        public void RemoveOrder(Order Order);
    }
}
