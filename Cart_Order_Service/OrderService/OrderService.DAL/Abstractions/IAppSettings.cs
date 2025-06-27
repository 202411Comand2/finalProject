namespace OrderService.DAL.Abstractions
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
