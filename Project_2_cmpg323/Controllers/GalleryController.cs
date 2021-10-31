using Microsoft.AspNetCore.Mvc;
using Project_2_cmpg323.Models;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_2_cmpg323.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            var hikingImageTags = new List<ImageTag>();
            var cityImageTags = new List<ImageTag>();

            var tag1 = new ImageTag()
            {
                Description = "Adventure",
                Id = 0
            };

            var tag2 = new ImageTag()
            {
                Description = "Urban",
                Id = 1
            };

            var tag3 = new ImageTag()
            {
                Description = "New York",
                Id = 2
            };

            hikingImageTags.Add(tag1);
            cityImageTags.AddRange(new List<ImageTag>{ tag2, tag3});

            var imageList = new List<GalleryImage>()
            {
                new GalleryImage()
                {
                    Title = "Hiking Trip",
                    Url = "https://images.pexels.com/photos/4550411/pexels-photo-4550411.jpeg?cs=srgb&dl=pexels-cottonbro-4550411.jpg&fm=jpg",
                    Created = DateTime.Now,
                    Tags = hikingImageTags
                },

                new GalleryImage()
                {
                    Title = "On the trail",
                    Url = "https://images.pexels.com/photos/2792292/pexels-photo-2792292.jpeg?cs=srgb&dl=pexels-alexander-zvir-2792292.jpg&fm=jpg",
                    Created = DateTime.Now,
                    Tags = hikingImageTags
                },

                new GalleryImage()
                {
                    Title = "Downtown",
                    Url = "https://images.pexels.com/photos/9828188/pexels-photo-9828188.jpeg?cs=srgb&dl=pexels-sonya-livshits-9828188.jpg&fm=jpg",
                    Created = DateTime.Now,
                    Tags = cityImageTags
                }
            };

            var model = new GalleryIndexModel()
            {
                Images = imageList,
                SearchQuery = ""
            };

            return View(model);
        }
    }
}
