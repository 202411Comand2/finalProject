using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BLL.Dto
{
    public class AnswerWithBackendDto<T>
    {
        /// <summary>
        /// Объект, который приходит с сервера
        /// </summary>
        /// <param name="objectDto"></param>
        public T ObjectDto { get; set; }
        
        /// <summary>
        /// Полученны ли данные с бека
        /// </summary>
        public bool DataReceived { get; set; } = false;

        /// <summary>
        /// Лог ошибки
        /// </summary>
        public string ErrorLog {  get; set; } = string.Empty;

        /// <summary>
        /// Коллекция объектов
        /// </summary>
        public List<T> ObjectsDto { get; set; } = new List<T>();

        /// <summary>
        /// Добавить возращаемый объект
        /// </summary>
        /// <param name="objectDro">Сам объект ДТО</param>
        /// <param name="error">Ошибка если требуется вернуть</param>
        public void AddObject(T objectDro, string error = "") 
        {
            try
            {

                if (objectDro != null)
                {
                    DataReceived = true;
                    ErrorLog = error;
                    ObjectDto = objectDro;
                }
                else
                {
                    DataReceived = false;
                    ErrorLog = error;
                }
            }
            catch (Exception ex) 
            {

                DataReceived = false;
                ErrorLog = ex.Message;
            }

        }

        /// <summary>
        /// Добавить коллекцию объектов
        /// </summary>
        /// <param name="objectDro"></param>
        public void AddObjct(List<T> objectDro)
        {
            foreach (T obj in objectDro)
            {
                AnswerWithBackendDto<T> answerWithBackendDto = new();
                try
                {
                    if (obj != null)
                    {
                        answerWithBackendDto.DataReceived = true;
                        answerWithBackendDto.ErrorLog = "";
                        answerWithBackendDto.ObjectDto = obj;
                    }
                    else
                    {
                        answerWithBackendDto.DataReceived = false;
                        answerWithBackendDto.ErrorLog = "Object is null";
                    }
                }
                catch (Exception ex)
                {

                    answerWithBackendDto.DataReceived = false;
                    answerWithBackendDto.ErrorLog = ex.Message;
                }
            }
        }
        


        /// <summary>
        /// Добавить ошибку
        /// </summary>
        /// <param name="error"></param>
        public void AddErrorLog(string error) 
        {
            ErrorLog = error;
            DataReceived = false;
        }
    }
}
