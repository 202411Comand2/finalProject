namespace Models.Shop
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
    }
}
