﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
	public class Request 
	{
		public int ID { get; set; }
		public string userName { get; set; }
		public DateTime date { get; set; }
        //public int yearReleased { get; set; }
        public string mediaTitle { get; set; }
		public string mediaType { get; set; }
		public string language { get; set; }
	}
}