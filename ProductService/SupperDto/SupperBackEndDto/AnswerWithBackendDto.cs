using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public string ErrorLog { get; set; } = string.Empty;

        /// <summary>
        /// Коллекция объектов
        /// </summary>
        public List<AnswerWithBackendDto<T>> ObjectsDto { get; set; } = new();

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
        /// Сериализировать объект в json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectDro"></param>
        /// <returns></returns>
        static private string Serialization(List<T> objectDro) => JsonSerializer.Serialize(objectDro);


        private AnswerWithBackendDto<T> _adapter(AnswerWithBackendDto<T> obj)
        {
            return obj;
        }

        /// <summary>
        /// Получить коллекцию объектов без проблемных, т.е. в выборку попадут объекты без ошибок
        /// </summary>
        /// <returns></returns>
        public string GetCollectionNotProblem()
        {
            List<T> list = new();
            foreach (var item in ObjectsDto)
            {
                if (item.DataReceived)
                {
                    list.Add(item.ObjectDto);
                }

            }
            return Serialization(list);
        }

        /// <summary>
        /// Получить коллекцию объектов включая и проблемные проблемных
        /// </summary>
        /// <returns></returns>
        public string GetCollectionWithProblem()
        {
            List<T> list = new();
            foreach (var item in ObjectsDto)
            {

                list.Add(item.ObjectDto);

            }
            return Serialization(list);
        }

        ///// <summary>
        ///// Получить коллекцию объектов c проблемами, т.е. нет ошибок в нём
        ///// </summary>
        ///// <returns></returns>
        //public string GetCollectionWithProblem() => Serialization(ObjectsDto);



        /// <summary>
        /// Добавить коллекцию объектов
        /// </summary>
        /// <param name="objectDto"></param>
        public void AddObject(List<T> objectDto)
        {
            if (objectDto.Count != 0)
            {
                DataReceived = true;
            }
            foreach (T obj in objectDto)
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

                ObjectsDto.Add(answerWithBackendDto);
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
