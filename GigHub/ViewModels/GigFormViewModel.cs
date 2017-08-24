using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        [DisplayName("Genres")]
        public byte Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public DateTime? GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}