using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAutorService
    {
        void CreateAutor(AutorDTO autorDTO);
        void UpdateAutor(AutorDTO autorDTO);
        void DeleteAutor(int id);
        IEnumerable<AutorDTO> GetAutors();
        AutorDTO GetAutor(int? id);
    }
}
