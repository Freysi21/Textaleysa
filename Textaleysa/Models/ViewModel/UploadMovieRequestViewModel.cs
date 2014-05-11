using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Textaleysa.Models.ViewModel
{
    public class UploadMovieRequestViewModel
    {
        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }
        [Display(Name = "Year Realeased")]
        public int Year { get; set; }

        [Display(Name = "language")]
        public string language { get; set; }
        [Display(Name = "userName")]
        public string userName { get; set; }


    }
}