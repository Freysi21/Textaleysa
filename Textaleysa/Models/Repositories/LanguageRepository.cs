using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Textaleysa.DAL;

namespace Textaleysa.Models.Repositories
{
	public class LanguageRepository
	{
		LanguageContext db = new LanguageContext();

		public Language GetLanguageById(int id)
		{
			var result = (from m in db.languages
						  where m.ID == id
						  select m).SingleOrDefault();
			return result;
		}

		public IEnumerable<Language> GetLanguages()
		{
			var result = from m in db.languages
						 orderby m.ID
						 select m;
			return result;
		}

		public void AddLanguage(Language l)
		{
			db.languages.Add(l);
			db.SaveChanges();
		}

		public void Modify(Language mt)
		{
			db.Entry(mt).State = EntityState.Modified;
			db.SaveChanges();
		}
	}
}