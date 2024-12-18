using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Shops")]
	public class Shop : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, MaxLength(30)] public string Name { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsVisible { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
