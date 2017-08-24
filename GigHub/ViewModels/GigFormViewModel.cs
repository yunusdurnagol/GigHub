using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Date needs to be filled in properly...")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Time needs to be filled in properly...")]
        [ValidTime(ErrorMessage = "Please enter: e.g 10:00")]
        public string Time { get; set; }

        [Required]
        [DisplayName("Genres")]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime? GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}