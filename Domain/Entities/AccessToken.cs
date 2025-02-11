using Domain.Abstractions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    [Table("AccessTokens")]
    public class AccessToken : IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Ключ токена
        /// </summary>
        [Required]                  public byte[] Key { get; set; }
        /// <summary>
        /// Роли пользователя
        /// </summary>
        [Required]                  public UserRole[] Roles { get; set; } = new[] {UserRole.Guest};
        /// <summary>
        /// Название устройства, с которого выполнен вход
        /// </summary>
        [Required, MaxLength(100)]  public string? DeviceName { get; set; }
        /// <summary>
        /// Последний ip адрес устройства
        /// </summary>
        [Required, MaxLength(45)]   public string? DeviceIp { get; set; }
        /// <summary>
        /// Дата создания токена авторизации 
        /// </summary>
        [Required]                  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Дата истечения срока действия токена 
        /// </summary>
        [Required]                  public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddMinutes(30);
        /// <summary>
        /// Id связанного пользователя
        /// </summary>
                                    public int? UserId { get; set; }
        /// <summary>
        /// Связанный пользователь
        /// </summary>
        [ForeignKey(nameof(UserId))]public User? User { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
