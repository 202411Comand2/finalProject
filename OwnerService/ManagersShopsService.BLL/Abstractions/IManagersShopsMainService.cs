
using ManagersShopsService.BLL.Dto;
using SupperBackEnd.Dto;
namespace ManagersShopsService 
{
    /// <summary>
    /// Интерфейс магазина
    /// </summary>
    public interface IManagersShopsMainService
    {

        /// <summary>
        /// Добавить партнёра по магазину
        /// </summary>
        /// <param name="UserDto"> </param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<AddManagersShopsDto>> AddManagersShopsDto(AddManagersShopsDto model);


        /// <summary>
        /// Удаление партнёра по магазину
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns>Удалось ли обновить магазин</returns>
        public Task<AnswerWithBackendDto<DeleteManagersShopsDto>> DeleteManagersShopsDto(DeleteManagersShopsDto model);

        /// <summary>
        /// Получить информацию о менеджарах магазина
        /// </summary>
        /// <param name="id">id магазина</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<GetManagersShopsDto>> GetManagersShopsDto(int idShop);

        /// <summary>
        /// Получить магазины, к который есть доступ у пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<GetManagersShopsDto>> GetShop(int id);

    }
}
