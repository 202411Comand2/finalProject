using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Comments")]
	public class Comment : IDbEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public decimal Estimation { get; set; }
		[Required, MaxLength(400)] public string Text { get; set; }
		public DateTime DateCreated { get; set; }
		public bool IsDeleted { get; set; }
		public int ReplyId { get; set; }
		public int ShopId { get; set; }

		[ForeignKey(nameof(ReplyId))]
		public CommentReply Replies { get; set; }
		[ForeignKey(nameof(ShopId))]
		public Shop Shop { get; set; }

		public int GetPrimaryKey()
		{
			return Id;
		}
	}
}
