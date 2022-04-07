using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        IRepository<User> UserRepository { get; set; }
        public UserService(IRepository<User> userRepository)
        {
            UserRepository = userRepository;
        }

        public void CreateUser(UserDTO userDTO)
        {
            User user = Mapper.Convert<UserDTO, User>(userDTO);
            UserRepository.Create(user);
        }
        public IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (User user in UserRepository.GetAll())
            {
                {
                    UserDTO userDTO = Mapper.Convert<User, UserDTO>(user);
                    userDTO.Role = Mapper.Convert<Role, RoleDTO>(user.Role);
                    userDTOs.Add(userDTO);
                }

            }

            return userDTOs;
        }

        public UserDTO GetUser(int? id)
        {
            var user = UserRepository.GetById(id.Value);
            if (user != null)
            {
                //ljdhsfb
                UserDTO userDTO = Mapper.Convert<User, UserDTO>(user);
                userDTO.Role = Mapper.Convert<Role, RoleDTO>(user.Role);
                return userDTO;
            }
            return null;
        }

        public void DeleteUser(int id)
        {
            UserRepository.Delete(id);
        }

        public bool IsUserIdUnique(int userId)
        {
            IEnumerable<UserDTO> userDTOs = GetUsers();
            foreach(UserDTO userDTO in userDTOs)
            {
                if(userDTO.Id == userId)
                {
                    return false;
                }
            }
            return true;

        }
    }
}
