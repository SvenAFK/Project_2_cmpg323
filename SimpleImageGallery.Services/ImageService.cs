using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;

namespace SimpleImageGallery.Services
{
    public class ImageService : IImage
    {
        public IEnumerable<GalleryImage> GetAll()
        {
            throw new NotImplementedException();
        }

        public GalleryImage GetById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            throw new NotImplementedException();
        }
    }
}
