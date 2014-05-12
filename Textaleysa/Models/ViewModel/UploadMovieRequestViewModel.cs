using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Textaleysa.Models.ViewModel;

namespace Textaleysa.Models.ViewModel
{
    public class UploadMovieRequestViewModel
    {
        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }
        [Display(Name = "Year Realeased")]
        public int year { get; set; }

        [Display(Name = "language")]
        public string language { get; set; }
        [Display(Name = "userName")]
        public string userName { get; set; }


    }
}