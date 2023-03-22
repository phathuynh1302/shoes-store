using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public byte[] image { get; set; }

        public string createBy { get; set; }

        public DateTime? createDate { get; set; }

        public string updateBy { get; set; }

        public bool status { get; set; }

        public DateTime? lastModifiedDate { get; set; }
    }
}
