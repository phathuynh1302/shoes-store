using PRN211_ShoesStore.Models.Entity;
using System;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.DTO
{
    public class OrderDetailsViewDto
    {
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
