using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class CartProduct : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		
		public int CartId { get; set; }
		public int ProductId { get; set; }
		public int Count { get; set; }
		public DateTime DateAdded { get; set; }

		[ForeignKey(nameof(CartId))]
		public Cart Cart { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
