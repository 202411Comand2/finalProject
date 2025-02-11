using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Пользователь с доступом к управлению магазином
    /// </summary>
    [Table("shop_owners")]
    public class ShopOwner:IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// true - пользователь является владельцем магазина, false - ограниченные права доступа
        /// </summary>
        [Required, Column("is_host")]   public bool IsHost { get; set; } = false;
        /// <summary>
        /// true - пользователь удалён, false - пользователь не удалён
        /// </summary>
        [Required, Column("is_delete")] public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required]                      public int UserId { get; set; }
        /// <summary>
        /// Связанный пользователь
        /// </summary>
        [ForeignKey(nameof(UserId))]    public User? User { get; set; }
        /// <summary>
        /// Id связанного магазина
        /// </summary>
        [Required]                      public int ShopId { get; set; }
        /// <summary>
        /// Связанный магазин
        /// </summary>
        [ForeignKey(nameof(ShopId))]    public Shop Shop { get; set; }

        public int GetPrimaryKey()
        {
            return Id;
        }
    }
}
