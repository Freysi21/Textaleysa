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


		public class SearchResultView
		{
			public string searchString { get; set; }

			public List<SearchResultListView> searchResultList { get; set; }
		}
		
		public class SearchResultListView
		{
			public int ID { get; set; }

			[Display(Name = "Titill")]
			public string title { get; set; }

			[Display(Name = "Tungumál")]
			public string language { get; set; }

			[Display(Name = "Fjöldi Niðurhala")]
			public int downloadCount { get; set; }

			[Display(Name = "Notandi")]
			public string userName { get; set; }

			[Display(Name = "Hlaðið upp")]
			public DateTime date { get; set; }

			public bool isMovie { get; set; }

			
		}
}