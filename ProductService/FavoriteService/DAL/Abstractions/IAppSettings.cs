namespace FavoriteService.DAL
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
