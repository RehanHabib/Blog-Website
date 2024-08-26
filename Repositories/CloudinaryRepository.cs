using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Blog_Website.Repositories
{
    public class CloudinaryRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName
                };

                var uploadResult = await client.UploadAsync(uploadParams);

                if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (uploadResult.SecureUrl != null)
                    {
                        return uploadResult.SecureUrl.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using a logging framework or to a file)
                Console.WriteLine($"An error occurred during image upload: {ex.Message}");
            }

            return null;
        }


    }
}
