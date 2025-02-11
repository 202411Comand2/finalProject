using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConfigSettings
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
