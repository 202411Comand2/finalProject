using Domain.Abstractions;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Аутификация
    /// </summary>
    public class AccessToken : IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        /// <summary>
        /// Ключ токена
        /// </summary>
        [Required] public byte[] Key { get; set; }
        [Required] public UserRole[] Roles { get; set; }
        /// <summary>
        /// Название устройства 
        /// </summary>
        [Required, MaxLength(250)]  public string DeviceName { get; set; }
        [Required, MaxLength(100)] public string DeviceIp { get; set; }
        /// <summary>
        /// Дата создания токена авторизации 
        /// </summary>
        [Required]  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Дата истечения срока действия токена 
        /// </summary>
        [Required]
        public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddMinutes(30);

        /// <summary>
        /// Связь много(токенов) к одну (пользователю)
        /// </summary>
        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

		/// <summary>
		/// Реализация интерфейса IDbEntity
		/// </summary>
		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
