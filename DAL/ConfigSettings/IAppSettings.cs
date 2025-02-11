using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConfigSettings
{
    /// <summary>
    /// Интерфейс общих настроек приложения
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Базовый каталог для хранения фотографий продукта
        /// </summary>
        string ProductPhotoBaseDirectory { get; }
    }
}
