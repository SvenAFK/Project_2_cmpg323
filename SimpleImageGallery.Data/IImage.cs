using Microsoft.WindowsAzure.Storage.Blob;
using SimpleImageGallery.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleImageGallery.Data
{
    public interface IImage
    {
        IEnumerable<GalleryImage> GetAll();
        IEnumerable<GalleryImage> GetWithTag(string tag);

        //Get with user id
        IEnumerable<GalleryImage> GetWithUserId(string strCurrentUserID);
        GalleryImage GetById(int id);
        CloudBlobContainer GetBlobContainer(string connectionString, string containerName);

        //Upload new images
        Task SetImage(string title, string tags, Uri uri,string user_Id);

        //Update old images
        Task UpdateImage(int imageID, string Title, string Tags);

        //Delete image
        Task DeleteImage(int imageID);

        List<ImageTag> ParseTags(string tags);
      
    }
}
