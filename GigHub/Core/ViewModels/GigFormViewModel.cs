using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        public string Action
        {
            get
            {
                /* In Func Delegate c stands for GigsController
                 Update method returns Action Result
                 if (id!=0) update else create
                 
                 */
                Expression<Func<GigsController, ActionResult>> update =
                    (c => c.Update(this));
                Expression<Func<GigsController, ActionResult>> create =
                    (c => c.Create(this));
                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

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
        public string Heading { get; set; }

        public DateTime? GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}