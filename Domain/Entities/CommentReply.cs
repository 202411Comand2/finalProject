using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("CommentReplies")]
	public class CommentReply : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, MaxLength(400)]
		public string Text { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
