using Microsoft.Extensions.Configuration;
using OrderService.DAL.Abstractions;

namespace OrderService.DAL.ConfigSettings
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
