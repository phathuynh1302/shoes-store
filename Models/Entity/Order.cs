﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace PRN211_ShoesStore.Models.Entity
{
	public enum OrderStatus
	{
		WAITING = 0,
		ACCEPTED = 1,
        DENIED = 2
	}
	[Table("Order")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }

        public int userId { get; set; }

        [ForeignKey("userId")]
        public User user { get; set; }

        [Column(TypeName = "Money")]
        public decimal price { get; set; }

        public int status { get; set; }

        public DateTime? createDate { get; set; }
    }
}
