using PRN211_ShoesStore.Models.Entity;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Repository.vH.Interface
{
    public interface IOrderDetailRepository : vH.Interface.IRepository<OrderDetail>
    {
        public List<OrderDetail> GetOrderDetailFromOrderIdIncludeShoes(int id);
    }
}
