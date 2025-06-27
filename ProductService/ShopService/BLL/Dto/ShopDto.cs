namespace ShopService.BLL
{
    public class ShopDto
    {
        /// <summary>
        /// ClusterId магазина
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название магазина
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Магазин удалён
        /// </summary>
        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// Описание магазина
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// Контактная инфомрация о магазине
        /// </summary>
        public string ContactInfo { get; set; } = string.Empty;

        /// <summary>
        /// Адресс магазина
        /// </summary>
        public string Adress { get; set; } = string.Empty;

    }
}
