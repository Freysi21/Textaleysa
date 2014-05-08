using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Textaleysa.Models
{
    public class SubtitleFileChunk
    {
        public int ID { get; set; }
        public int subtitleFileID { get; set; }
        public int lineID { get; set; }
        public string startTime { get; set; }
        public string stopTime { get; set; }
        public string subtitleContent { get; set; }
    }
}