using AuthService.BLL.Dto;
using SupperBackEnd.Dto;
using AuthService.Domain.Entities;
using AuthService.BLL;
using AuthService.DAL;

namespace AuthService.BLL
{
    /// <summary>
    /// Сервис по работе с авторизацией
    /// </summary>
    public class AuthMainService : IAuthMainService
    {
        private readonly AuthRepositories _authRepository;
        public AuthMainService(IContextManager contextManager)
        {
            _authRepository = new AuthRepositories(contextManager);
        }


        public async Task<AnswerWithBackendDto<UserDto>> RegisterUser(AddUserDto UserDto)
        {
            AnswerWithBackendDto<UserDto> answerWithBackendDto = new();
            // проверка на существующий ник нейм

            if (await _authRepository.SearchUserNickName(UserDto.Login) is not null)
            {
                answerWithBackendDto.AddErrorLog($"Пользователь c таким nickName найден в системе!");
                return answerWithBackendDto;
            }
            else 
            {

                var result = await _authRepository.Add(UsersAdapter.ConvertFromDTOToEntity(UserDto));
                if (result is null)
                {
                    answerWithBackendDto.AddErrorLog($"Не получилось создать пользователя");
                }
                else 
                {
                    answerWithBackendDto.AddObject(UsersAdapter.ConvertFromEntitieToDTO (result));
                }
                return answerWithBackendDto;

            }
        }

        public async Task<AnswerWithBackendDto<UserDto>> UpdateInfoUser(UserDto UserDto)
        {
            AnswerWithBackendDto<UserDto> answerWithBackendDto = new();

            throw new NotImplementedException();
        }

        public async Task<AnswerWithBackendDto<UserDto>> AuthUser(AuthUserDto UserDto)
        {
            //var s = await _authRepository.Get(1);

            AnswerWithBackendDto<UserDto> answerWithBackendDto = new();
            var auth = await _authRepository.GetUserLoginPassword(UserDto.Login,UserDto.Password);
            if (auth is null)
            {
                answerWithBackendDto.AddErrorLog($"Auth User: {UserDto.Login}");
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddObject(UsersAdapter.ConvertFromEntitieToDTO( auth));
                return answerWithBackendDto;
            }
        }

        public async Task<AnswerWithBackendDto<UserDto>> SearchUser(string NickNameUser)
        {
            AnswerWithBackendDto<UserDto> answerWithBackendDto = new();

            throw new NotImplementedException();
        }

        public async Task<AnswerWithBackendDto<UserDto>> GetInfoUser(int id) 
        {
            AnswerWithBackendDto<UserDto> answerWithBackendDto = new();
            var auth = await _authRepository.Get(id);
            if (auth is null)
            {
                answerWithBackendDto.AddErrorLog($"Auth User: {id}");
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddObject(UsersAdapter.ConvertFromEntitieToDTO(auth));
                return answerWithBackendDto;
            }
        }


    }
}
