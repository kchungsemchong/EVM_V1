using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class Outlet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OutletId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Display(Name = "City or Village")]
        public string City { get; set; }
    }
}