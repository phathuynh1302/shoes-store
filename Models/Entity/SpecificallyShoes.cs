using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("ShoesSpecifically")]
    public class SpecificallyShoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Column(TypeName = "Money")]
        public decimal price { get; set; }

        public long quantity { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        public int shoesId { get; set; }

        public bool status { get; set; }

        [ForeignKey("shoesId")]
        public Shoes shoes { get; set; }

        //
        public virtual ICollection<ColorSpecificallyShoes> ColorSpecificallyShoes { get; set; }
        public virtual ICollection<SpecificallyShoesSize> SpecificallyShoesSize { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
