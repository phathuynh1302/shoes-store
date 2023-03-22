using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string roleName { get; set; }

        public string roleDescription { get; set; }

        public DateTime? creatDate { get; set; }

        public DateTime? updateDate { get; set; }

        public bool status { get; set; }
    }
}
