using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public int fileID { get; set; }
        public double mediaGrade { get; set; }
    }
}