using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

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
    }
}
