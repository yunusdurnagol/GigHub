﻿using System;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }

        public bool isCanceled { get; set; }
        public UserDto Artist { get; set; }

        public string ArtistId { get; set; }
        public DateTime? DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
        public byte GenreId { get; set; }
    }
}