using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Clusters")]
	public class Cluster : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, MaxLength(30)] public string Name { get; set; }
		public int ParentId { get; set; }

		[ForeignKey(nameof(ParentId))]
		public Cluster Parent { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
