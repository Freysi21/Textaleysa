using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Textaleysa.Models;

namespace Textaleysa.DAL
{
	public class CommentIntitializer : System.Data.Entity.DropCreateDatabaseAlways<HRContext>
	{
		protected override void Seed(HRContext context)
		{
			// Default gildi gangagruns...
			var comments = new List<Comment>
			{
				new Comment
				{
					ID = 1,
					userName = "TheHacker",
					fileID = 1,
					content = "Nú er gaman",
					date = DateTime.Now
				},
				new Comment
				{
					ID = 2,
					userName = "!C#",
					fileID = 1,
					content = "Ég er eyzisharp",
					date = DateTime.Now
}
			};
			comments.ForEach(c => context.comments.Add(c));
			context.SaveChanges();
		}

	}
}