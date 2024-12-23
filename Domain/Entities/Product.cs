using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Products")]
	public class Product : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, MaxLength(30)] public string ModelNumber { get; set; }
		[Required, MaxLength(30)] public string Name { get; set; }
		[Required, MaxLength(255)] public string Description { get; set; }
		public int RatingId { get; set; }
		public int ClusterId { get; set; }
		public decimal Price { get; set; }
		public long Barcode { get; set; }

		[ForeignKey(nameof(RatingId))]
		public Rating Rating { get; set; }
		[ForeignKey(nameof(ClusterId))]
		public Cluster Cluster { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
