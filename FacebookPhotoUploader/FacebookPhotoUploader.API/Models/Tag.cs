using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookPhotoUploader.API.Models
{
    public class Tag
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public string Source { get; set; }
    }
}
