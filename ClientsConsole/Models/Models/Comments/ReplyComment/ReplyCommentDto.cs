namespace Client.Models
{
    public class ReplyCommentDto
    {
        /// <summary>
        /// Id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текстовое описание
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Удалён ли комментарий
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
