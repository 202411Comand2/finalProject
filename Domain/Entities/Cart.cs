using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Carts")]
	public class Cart : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
