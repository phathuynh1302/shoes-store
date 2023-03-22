using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("Color")]
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string createBy { get; set; }

        public DateTime? createDate { get; set; }

        public string updateBy { get; set; }

        public bool status { get; set; }

        public DateTime? updateDate { get; set; }
    }
}
