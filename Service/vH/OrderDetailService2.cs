using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Repository.vH.Interface;
using PRN211_ShoesStore.Service.vH.Interface;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Service.vH
{
    public class OrderDetailService2 : vH.Interface.IOrderDetailService
    {
        private readonly IOrderDetailRepository _OrderDetailRepository;
        public OrderDetailService2(IOrderDetailRepository OrderDetailRepository)
        {
            this._OrderDetailRepository = OrderDetailRepository;
        }
        public void AddOrderDetail(OrderDetail OrderDetail) => this._OrderDetailRepository.Add(OrderDetail);
        public OrderDetail GetOrderDetailByOrderDetailId(int id) => this._OrderDetailRepository.GetFirstOrDefault(item => item.id == id);
        public void UpdateOrderDetail(OrderDetail OrderDetail) => this._OrderDetailRepository.Update(OrderDetail);
        public void RemoveOrderDetail(OrderDetail OrderDetail) => this._OrderDetailRepository.Remove(OrderDetail);

        public List<OrderDetail> GetAllOrderDetail() => (List<OrderDetail>)this._OrderDetailRepository.GetAll();

        public List<OrderDetail> GetOrderDetailFromOrderIdIncludeShoes(int id)
        {
            return this._OrderDetailRepository.GetOrderDetailFromOrderIdIncludeShoes(id);
        }
    }
}
