using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool isCanceled { get; set; }

        public string ArtistId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Venue { get; set; }

        public byte GenreId { get; set; }
    }
}