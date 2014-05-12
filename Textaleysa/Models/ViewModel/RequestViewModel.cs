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

        [Required(ErrorMessage="Vantar titil")]
        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }

        [Required(ErrorMessage = "Vantar ártal")]
        [Display(Name = "Year Realeased")]
        public int yearReleased { get; set; }

        [Required(ErrorMessage = "Vantar tungumál")]
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