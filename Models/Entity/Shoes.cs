using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        public decimal price { get; set; }

        [StringLength(200)]
        public string shoesDetails { get; set; }

        public DateTime? launchDate { get; set; }

        public bool status { get; set; }

        public long quantity { get; set; }
    }
}
