namespace Client.Models
{
    public class AddSearchClusterDto
    {
        /// <summary>
        /// Ключевое слово
        /// </summary>
        public List<string?> KeyWords { get; set; }
        /// <summary>
        /// Id класстера
        /// </summary>
        public int IdCluster { get; set; }
    }
  
}
