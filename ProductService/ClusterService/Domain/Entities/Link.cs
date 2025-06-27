using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.DAL;

namespace ClusterService.Domain
{
    [Table("Link")]
    public class Link : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

     
        public int ClusterId { get; set; }  
        public int SearchClusterId { get; set; }  

        // Навигационные свойства
        [ForeignKey(nameof(ClusterId))]
        public Cluster Cluster { get; set; }

        [ForeignKey(nameof(SearchClusterId))]
        public SearchCluster SearchCluster { get; set; }

        public int GetPrimaryKey() => Id;



    }
}


