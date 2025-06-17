using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopService.BLL
{
    public class AddShopDto
    {
        public string? Name { get; set; }

        /// <summary>
        /// Описание магазина
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// Контактная инфомрация о магазине
        /// </summary>
        public string ContactInfo { get; set; } = string.Empty;

        /// <summary>
        /// Адресс магазина
        /// </summary>
        public string Adress { get; set; } = string.Empty;

    }
}
