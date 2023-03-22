using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System;
using System.Collections.Generic;

namespace PRN211_ShoesStore.Models.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int roleId { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public DateTime? createDate { get; set; }

        public DateTime? updateDate { get; set; }

        public string address { get; set; }

        [Required]
        [StringLength(50)]
        public string phone { get; set; }

        [Required]
        public string email { get; set; }

        [ForeignKey("roleId")]
        public Role role { get; set; } 

        public bool status { get; set; }
        
        
        //
        public virtual ICollection<Order> Orders { get; set; }

    }
}
