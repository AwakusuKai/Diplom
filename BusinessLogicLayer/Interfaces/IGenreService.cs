using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IGenreService
    {
        void CreateGenre(GenreDTO genreDTO);
        void UpdateGenre(GenreDTO genreDTO);
        void DeleteGenre(int id);
        IEnumerable<GenreDTO> GetGenres();
        GenreDTO GetGenre(int? id);
    }
}
