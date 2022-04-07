using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDTO);
        //void UpdateUser(UserDTO userDTO);
        void DeleteUser(int id);
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(int? id);
        bool IsUserIdUnique(int userId);
    }
}
