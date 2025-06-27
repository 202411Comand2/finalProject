namespace Client.Models
{
    /// <summary>
    /// Добавить новый кластер
    /// </summary>
    public class AddClusterDto
    {
        /// <summary>
        /// Имя кластера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родительский кластер
        /// </summary>
        public string NameParentCluseter { get; set; }

        public AddClusterDto() { }

        public AddClusterDto(string name, string nameParentCluseter)
        {
            Name = name;
            NameParentCluseter = nameParentCluseter;
        }
    }
}
