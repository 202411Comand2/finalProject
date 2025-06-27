using SupperBackEnd.Dto;
using ManagersShopsService.BLL.Dto;
using ManagersShopsService.DAL;
namespace ManagersShopsService.BLL
{
    /// <summary>
    /// Сервис по работе с авторизацией
    /// </summary>
    public class ManagersShopsMainService : IManagersShopsMainService
    {
        private readonly ManagersShopsRepositories _managersShopsRepositories;
        public ManagersShopsMainService(IContextManager contextManager)
        {
            _managersShopsRepositories = new ManagersShopsRepositories(contextManager);
        }

        #region
        //public async Task<AnswerWithBackendDto<DeleteManagersShopsDto>> RegisterUser(AddUserDto UserDto)
        //{
        //    AnswerWithBackendDto<DeleteManagersShopsDto> answerWithBackendDto = new();
        //    // проверка на существующий ник нейм

        //    if (await _managersShopsRepositories.SearchUserNickName(UserDto.Login) is not null)
        //    {
        //        answerWithBackendDto.AddErrorLog($"Пользователь c таким nickName найден в системе!");
        //        return answerWithBackendDto;
        //    }
        //    else 
        //    {

        //        var result = await _managersShopsRepositories.Add(ManagersShopsAdapter.ConvertFromDTOToEntity(UserDto));
        //        if (result is null)
        //        {
        //            answerWithBackendDto.AddErrorLog($"Не получилось создать пользователя");
        //        }
        //        else 
        //        {
        //            answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO (result));
        //        }
        //        return answerWithBackendDto;
        //    }
        //}

        //public async Task<AnswerWithBackendDto<DeleteManagersShopsDto>> UpdateInfoUser(UpdateManagersShopsDto UserDto)
        //{
        //    AnswerWithBackendDto<DeleteManagersShopsDto> answerWithBackendDto = new();
        //    var oldNice = await _managersShopsRepositories.SearchUserNickName(UserDto.Login);
        //    if (oldNice is not null && oldNice.Id!= UserDto.Id)
        //    {
        //        answerWithBackendDto.AddErrorLog($"Пользователь c таким nickName найден в системе!");
        //        return answerWithBackendDto;
        //    }
        //    else
        //    {

        //        var result = await _managersShopsRepositories.Update(ManagersShopsAdapter.ConvertFromDTOToEntity(UserDto));
        //        if (result is null)
        //        {
        //            answerWithBackendDto.AddErrorLog($"Не получилось обновить пользователя");
        //        }
        //        else
        //        {
        //            answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(result));
        //        }
        //        return answerWithBackendDto;
        //    }
        //}

        //public async Task<AnswerWithBackendDto<DeleteManagersShopsDto>> AuthUser(GetManagersShopsDto UserDto)
        //{
        //    //var s = await _managersShopsRepositories.Get(1);

        //    AnswerWithBackendDto<DeleteManagersShopsDto> answerWithBackendDto = new();
        //    var modelEntites = await _managersShopsRepositories.GetUserLoginPassword(UserDto.Login,UserDto.Password);
        //    if (modelEntites is null)
        //    {
        //        answerWithBackendDto.AddErrorLog($"Auth User: {UserDto.Login}");
        //        return answerWithBackendDto;
        //    }
        //    else
        //    {
        //        answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO( modelEntites));
        //        return answerWithBackendDto;
        //    }
        //}

        #endregion



        public async Task<AnswerWithBackendDto<AddManagersShopsDto>> AddManagersShopsDto(AddManagersShopsDto model)
        {
            AnswerWithBackendDto<AddManagersShopsDto> answerWithBackendDto = new();
            var modelEntites = await _managersShopsRepositories.Add(ManagersShopsAdapter.ConvertFromDTOToEntity(model));
            if (modelEntites is null)
            {
                answerWithBackendDto.AddErrorLog($"not create: {model.UserId}");
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(modelEntites));
                return answerWithBackendDto;
            }
            throw new NotImplementedException();

        }

        public async Task<AnswerWithBackendDto<DeleteManagersShopsDto>> DeleteManagersShopsDto(DeleteManagersShopsDto model)
        {
            AnswerWithBackendDto<DeleteManagersShopsDto> answerWithBackendDto = new();
            var modelEntites = await _managersShopsRepositories.Delete(ManagersShopsAdapter.ConvertFromDTOToEntity(model));
            if (!modelEntites)
            {
                answerWithBackendDto.AddErrorLog($"not delete: {model.UserId}");
                return answerWithBackendDto;
            }
            else
            {
                // answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(modelEntites));
                answerWithBackendDto.DataReceived = true;
                return answerWithBackendDto;
            }
        }




        public async Task<AnswerWithBackendDto<GetManagersShopsDto>> GetManagersShopsDto(GetManagersShopsDto model)
        {
            AnswerWithBackendDto<GetManagersShopsDto> answerWithBackendDto = new();
            var modelEntites = await _managersShopsRepositories.Add(ManagersShopsAdapter.ConvertFromDTOToEntity(model));
            if (modelEntites is null)
            {
                answerWithBackendDto.AddErrorLog($"not create: {model.UserId}");
                return answerWithBackendDto;
            }
            else
            {
               // answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(modelEntites));
                return answerWithBackendDto;
            }
        }

        public async Task<AnswerWithBackendDto<GetManagersShopsDto>> GetManagersShopsDto(int idShop)
        {
            AnswerWithBackendDto<GetManagersShopsDto> answerWithBackendDto = new();
            var items = await _managersShopsRepositories.GetManager(idShop);

            if (items == null)
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(items));
            return answerWithBackendDto;

        }

        public async Task<AnswerWithBackendDto<GetManagersShopsDto>> GetShop(int UserId)
        {
            AnswerWithBackendDto<GetManagersShopsDto> answerWithBackendDto = new();

            var items = await _managersShopsRepositories.GetShop(UserId);

            if (items == null)
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка при обращении к бд.");
            }
            if (items?.Count == 0)
            {
                answerWithBackendDto.AddErrorLog($"Данные отсутствуют.");
            }
            answerWithBackendDto.AddObject(ManagersShopsAdapter.ConvertFromEntitieToDTO(items));
            return answerWithBackendDto;

            
        }
    }
}
