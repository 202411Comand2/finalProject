using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupperBackEndDto
{
    /// <summary>
    /// Глобальные настройки для приложения
    /// </summary>
    public static class GlobalSetting
    {
        /// <summary>
        /// Путь, подключения к серверу 
        /// </summary>
        static public string PathServer { get; set; } = "localhost";

        // static public string PathServer { get; set; } = "host.docker.internal";   //для контейнеров  docker   
    }
}
