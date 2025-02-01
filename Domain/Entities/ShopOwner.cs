using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    /// <summary>
    /// Владелец магазина
    /// </summary>
    [Table("shop_owners")]
    public class ShopOwner:IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Владелец магазина
        /// </summary>
        [Required, Column("is_host")]
        public bool IsHost { get; set; } = false;
      
    

        /// <summary>
        /// Пользователь удалён
        /// </summary>
        [Required, Column("is_delete")]
        public bool IsDelete { get; set; } = false;


        #region связи

        ////ToDO я думаю, что тут не 1к1,  а много ко многим
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required, Column("user_id")]
        public int UserId { get; set; }
       
        [ForeignKey("UserId")]
        public User? User { get; set; }

        ///// <summary>
        ///// Коллекция магазинов
        ///// </summary>
        //public ICollection<Shop> Shops { get; set; } = new List<Shop>();


        public int ShopId { get; set; }

        //[ForeignKey(nameof(ShopId))]
        //public Shop Shop { get; set; }


        #endregion


        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }
    }
}
