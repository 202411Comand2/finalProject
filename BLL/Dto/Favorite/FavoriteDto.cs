namespace BLL.Dto.Favorite
{
    public class FavoriteDto
    {
        /// <summary>
        /// Id объекта
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }
    }
}
