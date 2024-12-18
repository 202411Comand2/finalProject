using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Rating : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public decimal AverageRating { get; set; }
		public int AmountOfComments { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
