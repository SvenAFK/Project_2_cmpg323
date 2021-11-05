using Microsoft.Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SimpleImageGallery.Data;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleImageGallery.Services
{
    public class ImageService : IImage
    {
        private readonly SimpleImageGalleryDbContext _ctx;
        public ImageService(SimpleImageGalleryDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<GalleryImage> GetAll()
        {
            return _ctx.GalleryImages
                .Include(img => img.Tags);
        }

        //function to get images that specfic users uploaded 
        public IEnumerable<GalleryImage> GetWithUserId(string strCurrentUserID)
        {
            return _ctx.GalleryImages.Where(SimpleImageGallery => SimpleImageGallery.User_Id == strCurrentUserID);
        }

        public GalleryImage GetById(int id)
        {
            return GetAll().Where(img => img.Id == id)
                .First();
        }

        public IEnumerable<GalleryImage> GetWithTag(string tag)
        {
            return GetAll().Where(img
             => img.Tags
            .Any(t => t.Description == tag));
        }

        public CloudBlobContainer GetBlobContainer(string AzureStorageConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=project2imagegallery;AccountKey=6jANYl9ENp8ywx/PiewIAebhFBv0Kdg8DxMxVWVXB9a+KHq+F0uibcIAP7U3JPoE0N3t5mZlb/wh1udiTfCFsw==;EndpointSuffix=core.windows.net");
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public async Task SetImage(string title, string tags, Uri uri, string user_Id)
        {
            var image = new GalleryImage
            {
                Title = title,
                Tags = ParseTags(tags),
                Url = uri.AbsoluteUri,
                Created = DateTime.Now,
                User_Id = user_Id
            };

            _ctx.Add(image);
            await _ctx.SaveChangesAsync();
        }

        public List<ImageTag> ParseTags(string tags)
        {
            return  tags.Split(",").Select(tag => new ImageTag {
                Description = tag
            }).ToList();

        }
    }
}
