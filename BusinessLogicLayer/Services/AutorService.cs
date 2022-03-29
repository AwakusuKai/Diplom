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
    public class AutorService: IAutorService
    {
        IRepository<Autor> AutorRepository { get; set; }
        public AutorService(IRepository<Autor> autorRepository)
        {
            AutorRepository = autorRepository;
        }

        public void CreateAutor(AutorDTO autorDTO)
        {
            Autor autor = Mapper.Convert<AutorDTO, Autor>(autorDTO);
            AutorRepository.Create(autor);
        }
        public IEnumerable<AutorDTO> GetAutors()
        {
            List<AutorDTO> autorDTOs = new List<AutorDTO>();
            foreach (Autor autor in AutorRepository.GetAll())
            {
                AutorDTO autorDTO = Mapper.Convert<Autor, AutorDTO>(autor);
                autorDTOs.Add(autorDTO);
            }

            return autorDTOs;
        }

        public void UpdateAutor(AutorDTO autorDTO)
        {
            Autor autor = Mapper.Convert<AutorDTO, Autor>(autorDTO);
            AutorRepository.Update(autor);
        }
        public AutorDTO GetAutor(int? id)
        {
            var autor = AutorRepository.GetById(id.Value);
            if (autor != null)
            {
                AutorDTO autorDTO = Mapper.Convert<Autor, AutorDTO>(autor);
                return autorDTO;
            }
            return null;
        }

        public void DeleteAutor(int id)
        {
            AutorRepository.Delete(id);
        }
    }
}
