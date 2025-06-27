using System.ComponentModel.DataAnnotations;

namespace IdentityService.DAL
{
	public class User : BaseEntity
	{
		[Required, MaxLength(30)] public string Name { get; set; }
		[Required, MaxLength(50)] public string Password { get; set; }
		[Required, MaxLength(12)] public string Phone { get; set; }
		[Required, MaxLength(40)] public string Email { get; set; }
		public int? TelegramId { get; set; }
		public DateTime DateCreated = DateTime.UtcNow;
	}
}
