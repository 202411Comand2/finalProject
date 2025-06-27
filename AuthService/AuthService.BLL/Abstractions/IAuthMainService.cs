using AuthService.BLL.Dto;
using SupperBackEnd.Dto;

namespace AuthService.BLL
{
    /// <summary>
    /// Интерфейс магазина
    /// </summary>
    public interface IAuthMainService
    {

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="UserDto">Модель AddUserDto </param>
        /// <returns>Возвращает зарегистрированного пользователя</returns>
        public Task<AnswerWithBackendDto<UserDto>> RegisterUser(AddUserDto UserDto);

        /// <summary>
        /// Обновление данных о пользователе
        /// </summary>
        /// <param name="UserDto">Нужно передать UserDto</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public Task<AnswerWithBackendDto<UserDto>> UpdateInfoUser(UpdateUserDto UserDto);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="UserDto"> пароль и логин пользователя</param>
        /// <returns>Удалось ли удалить магазин</returns>
        public Task<AnswerWithBackendDto<UserDto>> AuthUser(AuthUserDto UserDto);

        /// <summary>
        /// Поиск пользователя
        /// <param name="NickNameUser">Имя пользователя</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<UserDto>> SearchUser(string NickNameUser);

        /// <summary>
        /// Получить информацию о пользователе после авторизации
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<UserDto>> GetInfoUser(int id);

    }
}
