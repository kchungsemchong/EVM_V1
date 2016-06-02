using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EVM.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> EventDate { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> DtAdded { get; set; }
        public string Status { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public IEnumerable<Tariff> Tariffs { get; set; }
    }

    public class EventDetailsViewModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> EventDate { get; set; }
        public string LocationName { get; set; }
        public byte[] WallpaperContent { get; set; }
        public Nullable<DateTime> DtAdded { get; set; }
        public string Status { get; set; }
    }

    public class EventViewModel
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> EventDate { get; set; }
        public string LocationName { get; set; }
        public byte[] WallpaperContent { get; set; }
    }

    public class EventArtistViewModel
    {
        public string ArtistName { get; set; }
        public string FacebookUrl { get; set; }
    }

    public class EventSponsorViewModel
    {
        public string SponsorName { get; set; }
        public byte[] SponsorImage { get; set; }
    }
}