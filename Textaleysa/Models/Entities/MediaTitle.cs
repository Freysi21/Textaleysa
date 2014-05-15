using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class MediaTitle
    {
        public int ID { get; set; }
        public string title { get; set; }
		public int yearReleased { get; set; }
		public int season { get; set; }
		public int episode { get; set; }
		public bool isMovie { get; set; }
    }
}