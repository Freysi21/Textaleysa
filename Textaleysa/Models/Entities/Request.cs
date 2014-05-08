using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public DateTime date { get; set; }
        public string mediaType { get; set; }
        public string mediaTitle { get; set; }
        public string language { get; set; }
    }
}