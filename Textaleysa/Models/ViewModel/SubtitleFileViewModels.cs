using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Textaleysa.Models.ViewModel;

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

	public class DisplayMovieView
	{
		public int ID { get; set; }

		[Display(Name = "Title")]
		public string title { get; set; }

		[Display(Name = "Language")]
		public string language { get; set; }

		[Display(Name = "Year released")]
		public int yearReleased { get; set; }

		[Display(Name = "User")]
		public string userName { get; set; }

		[Display(Name = "Grade")]
		public double grade { get; set; }

		[Display(Name = "Date")]
		public DateTime date { get; set; }

		[Display(Name = "Comments")]
		public List<CommentAndLikeViewModel.CommentView> commentlist { get; set; }

		[Display(Name = "Download count")]
		public int downloadCount { get; set; }
	}

	public class FileFrontPageList
	{
		public int ID { get; set; }

		[Display(Name = "Title")]
		public string title { get; set; }
	}

	public class LanguageView
	{
		[Required]
		[Display(Name = "Language")]
		public string language { get; set; }
	}
}