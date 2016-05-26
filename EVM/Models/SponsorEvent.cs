using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class SponsorEvent
    {
        [Key]
        [Column(Order = 0)]
        public int SponsorId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int EventId { get; set; }
    }
}