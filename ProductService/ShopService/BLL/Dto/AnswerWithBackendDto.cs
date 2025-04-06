using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class AnswerWithBackendDto<T>
    {
        /// <summary>
        /// Объект, который приходит с сервера
        /// </summary>
        /// <param name="objectDto"></param>
        public T ObjectDro { get; set; }
        

    }
}
