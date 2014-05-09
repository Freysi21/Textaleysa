using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public int fileID { get; set; }
        public string content { get; set; }
        public DateTime date { get; set; }
		public List<String> likes { get; set; }
    }
}