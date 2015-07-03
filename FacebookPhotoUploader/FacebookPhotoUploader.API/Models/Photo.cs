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
        public string Id { get; set; }
        public string ImageLink { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Place Place { get; set; }

        public Photo()
        {
        }

        public Photo(dynamic image)
        {
            this.Id = image.id;
            this.Caption = image.name;
            this.ImageLink = image.source;
        }
    }
}
