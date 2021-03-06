﻿using System;
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
        [Display(Name = "Titill")]
        public string mediaTitle { get; set; }

        [Required(ErrorMessage = "Vantar ártal")]
        [Display(Name = "Útgáfuár")]
        public int yearReleased { get; set; }

        [Required(ErrorMessage = "Tungumál vantar")]
        [Display(Name = "Tungumál")]
        public string language { get; set; }
    }
    public class UploadEpisodeRequestViewModel
    {

        [Required(ErrorMessage = "Vantar titil")]
        [Display(Name = "Seríunafn")]
        public string mediaTitle { get; set; }

        [Required(ErrorMessage = "Vinsamlegast tilgreindu seríu")]
        [Display(Name = "Sería")]
        public int season { get; set; }

        [Required(ErrorMessage = "Vinsamlegast tilgreindu þátt")]
        [Display(Name = "Þáttur")]
        public int episode { get; set; }

        [Required(ErrorMessage = "Tungumál vantar")]
        [Display(Name = "Tungumál")]
        public string language { get; set; }
    }
    public class ListRequestViewModel
    {
        [Display(Name = "Nafn myndefnis")]
        public string mediaTitle { get; set; }

        [Display(Name = "Tungumál")]
        public string language { get; set; }

        [Display(Name = "Hlaðið upp af")]
        public string userName { get; set; }

        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Fjöldi atkvæða")]
        public int votes { get; set; }
    }
}