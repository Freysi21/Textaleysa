using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Textaleysa.Models.ViewModel;

namespace Textaleysa.Models.ViewModel
{
    public class RequestViewModel
    {
    }
    public class UploadMovieRequestViewModel
    {
        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }

        [Display(Name = "Year Realeased")]
        public int yearReleased { get; set; }

        [Display(Name = "language")]
        public string language { get; set; }
    }
    public class ListRequestViewModel
    {
        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }

        [Display(Name = "language")]
        public string language { get; set; }

        [Display(Name = "User")]
        public string userName { get; set; }
    }
}