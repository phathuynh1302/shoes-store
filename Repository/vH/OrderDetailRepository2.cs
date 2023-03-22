using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PRN211_ShoesStore.Repository.vH
{
    public class OrderDetailRepository2 : vH.Repository<OrderDetail>, vH.Interface.IOrderDetailRepository
    {
        private readonly AppDbContext AppDbContext;
        public OrderDetailRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public List<OrderDetail> GetOrderDetailFromOrderIdIncludeShoes(int id)
        {
            return AppDbContext.ordersDetail
                .Where(od => od.orderId == id)
                .Include(od => od.shoes)
                    .ThenInclude(s => s.shoes)
                .Include(od => od.shoes)
                    .ThenInclude(s => s.SpecificallyShoesSize)
                        .ThenInclude(ss => ss.size)
                .Include(od => od.shoes)
                    .ThenInclude(s => s.ColorSpecificallyShoes)
                        .ThenInclude(sc => sc.color)
                .ToList();
        }
    }
}
