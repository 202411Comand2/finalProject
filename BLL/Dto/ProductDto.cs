using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ProductDto
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Id магазина
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// Id кластера
        /// </summary>
        public int ClusterId { get; set; }
        /// <summary>
        /// Название продукта
        /// </summary>
        public string NameProduct { get; set; }
        /// <summary>
        /// Описание продукта
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Штрихкод
        /// </summary>
        public long Barcode { get; set; }
        /// <summary>
        /// Номер модели
        /// </summary>
        public string ModelNumber { get; set; }

    }
}
