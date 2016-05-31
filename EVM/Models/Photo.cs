using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int EventId { get; set; }
    }
}