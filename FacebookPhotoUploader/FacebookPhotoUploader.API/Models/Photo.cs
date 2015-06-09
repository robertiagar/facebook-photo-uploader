using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Models
{
    public class Photo
    {
        public string Caption { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Place Place { get; set; }
    }
}
