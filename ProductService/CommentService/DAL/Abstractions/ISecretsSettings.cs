namespace CommentService.DAL
{
    /// <summary>
    /// Интерфейс конфиденциальных настроек приложения
    /// </summary>
    public interface ISecretsSettings
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string ConnectionString { get; }
    }
}
