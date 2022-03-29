using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Type = DataAccessLayer.Entities.Type;
namespace BusinessLogicLayer.Services
{
    public class TypeService: ITypeService
    {
        IRepository<Type> TypeRepository { get; set; }
        public TypeService(IRepository<Type> typeRepository)
        {
            TypeRepository = typeRepository;
        }

        public void CreateType(TypeDTO typeDTO)
        {
            Type type = Mapper.Convert<TypeDTO, Type>(typeDTO);
            TypeRepository.Create(type);
        }
        public IEnumerable<TypeDTO> GetTypes()
        {
            List<TypeDTO> typeDTOs = new List<TypeDTO>();
            foreach (Type type in TypeRepository.GetAll())
            {
                TypeDTO typeDTO = Mapper.Convert<Type, TypeDTO>(type);
                typeDTOs.Add(typeDTO);
            }

            return typeDTOs;
        }

        public void UpdateType(TypeDTO typeDTO)
        {
            Type type = Mapper.Convert<TypeDTO, Type>(typeDTO);
            TypeRepository.Update(type);
        }
        public TypeDTO GetType(int? id)
        {
            var type = TypeRepository.GetById(id.Value);
            if (type != null)
            {
                TypeDTO typeDTO = Mapper.Convert<Type, TypeDTO>(type);
                return typeDTO;
            }
            return null;
        }

        public void DeleteType(int id)
        {
            TypeRepository.Delete(id);
        }
    }
}
