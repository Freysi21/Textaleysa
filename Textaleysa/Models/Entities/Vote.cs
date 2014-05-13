using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Vote
    {
        public int ID { get; set; }
        public int requestID { get; set; }
        public string userName { get; set; }
    }
}