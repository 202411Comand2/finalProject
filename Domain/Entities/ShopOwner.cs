using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("ShopOwners")]
	public class ShopOwner : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int ShopId { get; set; }
		public bool IsHost { get; set; }
		public bool IsDeleted { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
		[ForeignKey(nameof(ShopId))]
		public Shop Shop { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
