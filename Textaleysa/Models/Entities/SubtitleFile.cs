using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class SubtitleFile
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int mediaTitleID { get; set; }

        public DateTime date { get; set; }
        public string language { get; set; }
        public int downloadCount { get; set; }
        public string content { get; set; }
    }
}