using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Jewellery.Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jewellery.Business.Concrete
{
    public class PhotoService : IPhotoService
    {
        private IConfiguration _configuration;
        private Cloudinary _cloudinary;

        public PhotoService(IConfiguration configuration)
        {
            _configuration = configuration;
            //_cloudinarySettings = _configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            //Account account = new Account(_cloudinarySettings.CloudName,
            //    _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);

            Account account = new Account(
                _configuration["CloudinarySettings:CloudName"],
                _configuration["CloudinarySettings:ApiKey"],
                _configuration["CloudinarySettings:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }



        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadedResult = new ImageUploadResult();
            if (file?.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadedResult = await _cloudinary.UploadAsync(uploadParams);
                    if (uploadedResult != null)
                    {
                        return uploadedResult.Url.ToString();
                    }
                }
            }
            return "";
        }

        public async Task<string> UploadImageFromPathAsync(string filePath)
        {
            var uploadedResult = new ImageUploadResult();
            if (filePath?.Length > 0)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(filePath)
                };
                uploadedResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadedResult != null)
                {
                    return uploadedResult.Url.ToString();
                }
            }
            return "";
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

            return deletionResult.Result == "ok";
        }

        public async Task<string> UpdateImageAsync(IFormFile newFile, string oldPublicId)
        {
            // Yeni fotoğrafı yükle
            var newImageUrl = await UploadImageAsync(newFile);

            if (!string.IsNullOrEmpty(newImageUrl))
            {
                // Eski fotoğrafı sil
                await DeleteImageAsync(oldPublicId);
            }

            return newImageUrl;
        }

        public string GetPublicIdFromUrl(string url)
        {
            string pattern = @"upload/(.*?)\.";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(url);

            if (match.Success)
            {
                return match.Groups[1].Value.Split('/')[1];
            }
            else
            {
                return null;
            }
        }
    }

}
