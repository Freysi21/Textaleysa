using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class Serie : MediaTitle
    {
        public int season { get; set; }
        public int episode { get; set; }
    }
}   