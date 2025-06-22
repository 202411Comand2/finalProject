using AuthService.BLL.Dto;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthService.BLL
{
    public class UsersAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        /// <param name="shop">Магазин Entitie</param>
        /// <returns>CommentDto</returns>
        public static UserDto ConvertFromEntitieToDTO(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Password = user.Password,
                NumberPhone = user.NumberPhone,
                Email = user.Email,
                TelegramID = user.TelegramID
            };
        }
        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        /// <param name="shop">Магазин Entitie</param>
        /// <returns>CommentDto</returns>
        public static UserEasyDto ConvertFromEntitieToDTOEasy(User user)
        {
            return new UserEasyDto()
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
            };
        }
        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="shopDto">Магазин Entitie</param>
        /// <returns>Comment</returns>
        public static User ConvertFromDTOToEntity(UserDto userDto)
        {
            return new User()
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Patronymic = userDto.Patronymic,
                Password = userDto.Password,
                NumberPhone = userDto.NumberPhone,
                Email = userDto.Email,
                TelegramID = userDto.TelegramID
            };
        }


        public static User ConvertFromDTOToEntity(UpdateUserDto userDto)
        {
            return new User()
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Patronymic = userDto.Patronymic,
                Password = userDto.Password,
                NumberPhone = userDto.NumberPhone,
                Email = userDto.Email,
                TelegramID = userDto.TelegramID
            };
        }


        public static User ConvertFromDTOToEntity(AddUserDto userDto)
        {
            return new User()
            {
                Login = userDto.Login,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Patronymic = userDto.Patronymic,
                Password = userDto.Password,
                NumberPhone = userDto.NumberPhone,
                Email = userDto.Email,
                TelegramID = userDto.TelegramID
            };
        }


    }
}

