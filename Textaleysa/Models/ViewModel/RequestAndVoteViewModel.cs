using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Textaleysa.Models.ViewModel
{
    public class RequestAndVoteViewModel
    {
        [Display(Name = "User")]
        public string userName { get; set; }

        [Display(Name = "Date of comment")]
        public DateTime date { get; set; }

        [Display(Name = "mediaTitle")]
        public string mediaTitle { get; set; }
        [Display(Name = "mediaType")]
        public string mediaType { get; set; }
        [Display(Name = "language")]
        public string language { get; set; }

        [Display(Name = "Votes")]
        public int nrOfLikes { get; set; }
    }
}