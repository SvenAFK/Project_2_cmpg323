using SimpleImageGallery.Data.Models;
using System.Collections.Generic;

namespace Project_2_cmpg323.Models
{
    public class GalleryIndexModel
    {
        public IEnumerable<GalleryImage> Images { get; set; }
        public string SearchQuery { get; set; }
    }
}
