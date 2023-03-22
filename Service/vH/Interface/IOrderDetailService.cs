using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH.Interface
{
    public interface IOrderDetailService
    {
        public void AddOrderDetail(OrderDetail OrderDetail);
        public List<OrderDetail> GetAllOrderDetail();
        public OrderDetail GetOrderDetailByOrderDetailId(int id);
        public void UpdateOrderDetail(OrderDetail OrderDetail);
        public void RemoveOrderDetail(OrderDetail OrderDetail);
        public List<OrderDetail> GetOrderDetailFromOrderIdIncludeShoes(int id);
    }
}
