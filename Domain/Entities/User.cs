using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Users")]
	public class User : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, MaxLength(30)] public string Name { get; set; }
		[Required, MaxLength(50)] public string Password { get; set; }
		[Required, MaxLength(12)] public string Phone { get; set; }
		[Required, MaxLength(40)] public string Email { get; set; }
		public int? TelegramId { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
