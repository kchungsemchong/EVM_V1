using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVM.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public Nullable<DateTime> DtAdded { get; set; }
        [Url]
        [Display(Name = "Facebook Page")]
        public string FacebookUrl { get; set; }
        public string Status { get; set; }
    }
}