using Microsoft.Extensions.Configuration;

namespace ProductService.DAL
{
    public class AppSettings : IAppSettings
    {
        /// <inheritdoc />
        public string ProductPhotoBaseDirectory { get; private set; }

        public AppSettings(IConfiguration config) 
        {
            var section = config.GetRequiredSection("AppSettings");
            ProductPhotoBaseDirectory = section.GetValue<string>("ProductPhotoBaseDirectory");
        }
    }
}
