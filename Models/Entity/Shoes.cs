using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("Shoes")]
    public class Shoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }

        public string image { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#} VNĐ", ApplyFormatInEditMode = false)]
        public decimal price { get; set; }

        [StringLength(200)]
        public string shoesDetails { get; set; }

        public DateTime? launchDate { get; set; }

        public bool status { get; set; }

        public long quantity { get; set; }


        //
        public virtual ICollection<CategoryShoes> CategoryShoes { get; set; }
        public virtual ICollection<ShoesImage> ShoesImages { get; set; }
        public virtual ICollection<ShoesColor> ShoesColors { get; set; }
        public virtual ICollection<SpecificallyShoes> SpecificallyShoes { get; set; }
        //

    }
}
