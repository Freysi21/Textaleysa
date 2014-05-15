using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Textaleysa.Models.ViewModel;

namespace Textaleysa.Models.ViewModel
{
		public class SearchViewModel
		{
			public string searchString { get; set; }

			public bool isMoive {get; set;}

			[Display(Name = "Tungumál")]
			public int languageID { get; set; }

			public IEnumerable<Language> languageOptions { get; set; }
			
		}

		public class FrontPageViewModel : SearchViewModel
		{
			public List<ListOfFilesView> latestFiles { get; set; }

			public List<ListOfFilesView> mostPopularFiles { get; set; }
		}

		public class ListOfFilesView
		{
			public int ID { get; set; }

			[Display(Name = "Titill")]
			public string title { get; set; }

			public bool isMovie { get; set; }
		}
}