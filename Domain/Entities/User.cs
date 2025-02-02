using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Таблица пользователей
    /// </summary>
    [Table("Users")]
    public class User : IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key,Column("id"),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required, Column("name"), MaxLength(40)]
        public string? Name { get; set; }
        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required, Column("password"), MaxLength(144)]
        public byte[] Password { get; set; }
        /// <summary>
        /// Номер мобильного телефона
        /// </summary>
        [Column("phone"),MaxLength(12)]
        public string? Phone { get; set; }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [Column("email"), MaxLength(255)]
        public string? Email { get; set; }
        /// <summary>
        /// Связанный id телеграмм акаунта пользователя
        /// </summary>
        [Column("telegram_id")]
        public long? TelegramId { get; set; }
        /// <summary>
        /// true - пользователь удалён, false - пользователь не удалён
        /// </summary>
        [Required, Column("is_delete")]
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// Коллекция владельцев магазинов
        /// </summary>
        public ICollection<ShopOwner> ShopOwners { get; set; } = new List<ShopOwner>();
        /// <summary>
        /// Коллекция комментариев пользователя
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        /// <summary>
        /// Коллекция комментариев пользователя
        /// </summary>
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();      
        /// <summary>
        /// Коллекция заказов пользователя
        /// </summary>
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public int GetPrimaryKey()
        {
           return Id;
        }
    }
}
