using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RunGroupApp.Helpers;
using RunGroupApp.Interface;

namespace RunGroupApp.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecrete);
            _cloudinary=new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadresult= new ImageUploadResult();
            if(file.Length > 0)
            {
                using var stream=file.OpenReadStream();
                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation=new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadresult = await _cloudinary.UploadAsync(uploadParam);
            }
           return uploadresult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParam = new DeletionParams(publicId);
            var result=await   _cloudinary.DestroyAsync(deleteParam);
            return result;
        }
    }
}
