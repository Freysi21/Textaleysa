using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.ViewModel
{
	public class SubtitleFileViewModels
	{
	}

	public class UploadMovieModelView
	{
		[Required]
		[Display(Name = "Title")]
		public string title { get; set; }

		[Required]
		[Display(Name = "Language")]
		public string language { get; set; }

		[Required]
		[Display(Name = "Year released")]
		public int yearReleased { get; set; }

		[Required]
		public HttpPostedFileBase file { get; set; }
	}

	public class UploadSerieModelView
	{
		[Required]
		[Display(Name = "Title")]
		public string title { get; set; }
		
		[Required]
		[Display(Name = "Language")]
		public string language { get; set; }

		[Required]
		[Display(Name = "Season")]
		public int season { get; set; }

		[Required]
		[Display(Name = "Episode")]
		public int episode { get; set; }

		[Required]
		[Display(Name = "Upload File")]
		public HttpPostedFileBase file { get; set; }
	}
}