using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.API.Models
{
    public class Album
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}
