using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Shop
{
    public class ShopDto
    {
        /// <summary>
        /// ClusterId магазина
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название магазина
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Магазин удалён
        /// </summary>
        public bool IsDelete { get; set; } = false;
    }
}
