using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class Tariff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TariffId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public int SeatsAvailable { get; set; }
        public Nullable<DateTime> Deadline { get; set; }
    }
}