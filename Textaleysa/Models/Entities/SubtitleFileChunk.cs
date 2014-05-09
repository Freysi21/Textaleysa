﻿using System;
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
        public string subtitleLineOne { get; set; }
		public string subtitleLineTwo { get; set; }
    }
}