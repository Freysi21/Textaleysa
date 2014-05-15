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
			public int fileID { get; set; }

			[Display(Name = "Content")]
			public string comment { get; set; }
		}
	}
}