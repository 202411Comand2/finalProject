using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperBackEnd.ServerResponseEND
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Возвращать объект с данными ответа и статусом
        /// </summary>
        /// <param name="isSuccess">Успешно true\false</param>
        /// <param name="data">Объект</param>
        /// <param name="errorMessage">Сообщенька</param>
        public ApiResponse(bool isSuccess, T data, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
        
    }
}
