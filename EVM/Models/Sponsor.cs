﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class Sponsor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SponsorId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public Nullable<DateTime> DtAdded { get; set; }
        public string Status { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}