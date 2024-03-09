using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Business.Abstract
{
    public interface IPhotoService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadImageFromPathAsync(string filePath);
        Task<bool> DeleteImageAsync(string publicId);
        Task<string> UpdateImageAsync(IFormFile newFile, string oldPublicId);
        string GetPublicIdFromUrl(string url);
    }
}
