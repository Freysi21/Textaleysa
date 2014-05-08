using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Movie : MediaTitle
    {
        public int ID { get; set; }
        public int yearReleased { get; set; }
    }
}   