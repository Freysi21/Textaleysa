using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Like
    {
        public int ID { get; set; }
        public int commentID { get; set; }
        public int userID { get; set; }
    }
}