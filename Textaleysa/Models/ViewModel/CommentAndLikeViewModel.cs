using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Textaleysa.Models.ViewModel
{
	public class CommentAndLikeViewModel
	{
		public class CommentView
		{
			[Display(Name = "User")]
			public string userName { get; set; }

			[Display(Name = "Date of comment")]
			public DateTime date { get; set; }

			[Display(Name = "Content")]
			public string content { get; set; }

			[Display(Name = "Likes")]
			public int nrOfLikes { get; set; }
		}
	}
}