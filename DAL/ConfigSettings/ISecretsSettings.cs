using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConfigSettings
{
    /// <summary>
    /// Интерфейс конфиденциальных настроек приложения
    /// </summary>
    public interface ISecretsSettings
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string ConnectionString { get; }
    }
}
