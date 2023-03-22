using PRN211_ShoesStore.Models.Entity;
using PRN211_ShoesStore.Models;
using PRN211_ShoesStore.Service.vH;
using PRN211_ShoesStore.Repository.vH.Interface;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PRN211_ShoesStore.Repository.vH
{
    public class OrderRepository2 : vH.Repository<Order>, vH.Interface.IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        public OrderRepository2(AppDbContext appDbContext) : base(appDbContext)
        {
            this._appDbContext = appDbContext;
        }
    }
}
