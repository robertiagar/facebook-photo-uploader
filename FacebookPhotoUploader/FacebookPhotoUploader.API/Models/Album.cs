using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Description { get; set; }
        public string CoverPhotoLink { get; set; }
        public virtual IList<Photo> Photos { get; set; }

        public Album()
        {
        }

        public Album(dynamic album)
        {
            this.Id = album.id;
            this.Name = album.name;
            this.Created = DateTime.Parse(album.created_time);
            this.Updated = DateTime.Parse(album.updated_time);
            this.Description = album.description;
            this.CoverPhotoLink = string.Empty;
        }

        public void AddPhoto(Photo photo)
        {
            if (Photos == null)
            {
                Photos = new List<Photo>();
            }

            var exists = Photos.Where(p => p.Id == photo.Id).SingleOrDefault();

            if (exists == null)
            {
                this.Photos.Add(photo);
            }
        }
    }
}
