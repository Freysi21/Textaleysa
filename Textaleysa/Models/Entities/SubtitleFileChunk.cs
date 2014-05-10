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

        public TimeSpan startTime { get; set; }
		public TimeSpan stopTime { get; set; }
        public string subtitleLine1 { get; set; }
		public string subtitleLine2 { get; set; }
		public string subtitleLine3 { get; set; }
    }
}