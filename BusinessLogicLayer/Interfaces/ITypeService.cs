using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITypeService
    {
        void CreateType(TypeDTO typeDTO);
        void UpdateType(TypeDTO typeDTO);
        void DeleteType(int id);
        IEnumerable<TypeDTO> GetTypes();
        TypeDTO GetType(int? id);
    }
}
