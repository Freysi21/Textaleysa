﻿using System;
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

	public class UploadSerieModelView
	{
		[Required]
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Required]
		[Display(Name = "Tungumál")]
		public int languageID { get; set; }

		[Required]
		[Display(Name = "Sería")]
		public int season { get; set; }

		[Required]
		[Display(Name = "Þáttur nr.")]
		public int episode { get; set; }

		public IEnumerable<Language> languageOptions { get; set; }
	}

	public class UploadMovieModelView
	{
		[Required]
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Required]
		[Display(Name = "Tungumál")]
		public int languageID { get; set; }

		[Required]
		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		public IEnumerable<Language> languageOptions { get; set; }
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

        [Required(ErrorMessage = "Einkunn verður að vera 0-10")]
		[Display(Name = "Einkunn")]
		public double grade { get; set; }

		[Display(Name = "Hlaðið upp")]
		public DateTime date { get; set; }

		[Display(Name = "Fjöldi niðurhala")]
		public int downloadCount { get; set; }

		public IEnumerable<Language> CategoryTypeOptions { get; set; }
	}

	public class DisplaySerieView
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Sería")]
		public int season { get; set; }

		[Display(Name = "Þáttur")]
		public int episode { get; set; }

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

	public class DisplayMovieFileView
	{
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		public List<DisplayContentFileView> content { get; set; }
	}

	public class DisplaySerieFileView
	{
		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Sería")]
		public int season { get; set; }

		[Display(Name = "Þáttur")]
		public int episode { get; set; }

		public List<DisplayContentFileView> content { get; set; }
	}

	public class DisplayContentFileView
	{
		public int ID { get; set; }

		public int lineID { get; set; }

		[Required]
		public string startTime { get; set; }

		[Required]
		public string stopTime { get; set; }

		[Required]
		public string line1 { get; set; }

		public string line2 { get; set; }

		public string line3 { get; set; }
	}

	public class EditSerieView
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Sería")]
		public int season { get; set; }

		[Display(Name = "Þáttur")]
		public int episode { get; set; }

		[Required]
		[Display(Name = "Innihald skráar")]
		public string content { get; set; }
	}

	public class EditMovieView
	{
		public int ID { get; set; }

		[Display(Name = "Titill")]
		public string title { get; set; }

		[Display(Name = "Tungumál")]
		public string language { get; set; }

		[Display(Name = "Útgáfuár")]
		public int yearReleased { get; set; }

		[Required]
		[Display(Name = "Innihald skráar")]
		public string content { get; set; }
	}

	public class EditFileView
	{
		public int ID { get; set; }

		[Required]
		[Display(Name = "Innihald skráar")]
		public string content { get; set; }
	}

	public class LanguageView
	{
		[Required]
		[Display(Name = "Tungumál")]
		public string language { get; set; }
	}
}