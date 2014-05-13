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
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Tungumál")]
		public int languageID { get; set; }

		[Required]
		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		public IEnumerable<Language> languageOptions { get; set; }
	}

	public class UploadSerieModelView
	{
		[Required]
		[Display(Name = "Titill")]
		public string title { get; set; }
		
		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Required]
		[Display(Name = "Sería")]
		public int season { get; set; }

		[Required]
		[Display(Name = "Þáttur nr.")]
		public int episode { get; set; }

		[Required]
		[Display(Name = "Hlaða upp")]
		public HttpPostedFileBase file { get; set; }
	}

	public class DisplayMovieView
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		[Display(Name = "Notandi")]
		public string userName { get; set; }

		[Display(Name = "Einkunn")]
		public double grade { get; set; }

		[Display(Name = "Hlaðið upp")]
		public DateTime date { get; set; }

		[Display(Name = "Athugasemdir")]
		public List<CommentAndLikeViewModel.CommentView> commentlist { get; set; }

		[Display(Name = "Fjöldi niðurhala")]
		public int downloadCount { get; set; }

		public IEnumerable<Language> CategoryTypeOptions { get; set; }
	}

	public class DisplayFileView
	{
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		public List<DisplayContentFileView> content { get; set; }
	}

	public class DisplayContentFileView
	{
		public int lineID { get; set; }

		public string startTime { get; set; }

		public string stopTime { get; set; }

		public string line1 { get; set; }

		public string line2 { get; set; }

		public string line3 { get; set; }
	}

	public class EditFileView
	{
		public int ID { get; set; }

		[Required]
		[Display(Name = "Innihald skráar")]
		public string content { get; set; }
	}

	public class FileFrontPageList
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		public string title { get; set; }
	}

	public class LanguageView
	{
		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }
	}
}