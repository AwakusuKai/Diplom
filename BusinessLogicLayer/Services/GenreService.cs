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
    public class GenreService : IGenreService
    {
        IRepository<Genre> GenreRepository { get; set; }
        public GenreService(IRepository<Genre> genreRepository)
        {
            GenreRepository = genreRepository;
        }

        public void CreateGenre(GenreDTO genreDTO)
        {
            Genre genre = Mapper.Convert<GenreDTO, Genre>(genreDTO);
            GenreRepository.Create(genre);
        }
        public IEnumerable<GenreDTO> GetGenres()
        {
            List<GenreDTO> genreDTOs = new List<GenreDTO>();
            foreach (Genre genre in GenreRepository.GetAll())
            {
                GenreDTO genreDTO = Mapper.Convert<Genre, GenreDTO>(genre);
                genreDTOs.Add(genreDTO);
            }

            return genreDTOs;
        }

        public void UpdateGenre(GenreDTO genreDTO)
        {
            Genre genre = Mapper.Convert<GenreDTO, Genre>(genreDTO);
            GenreRepository.Update(genre);
        }
        public GenreDTO GetGenre(int? id)
        {
            var genre = GenreRepository.GetById(id.Value);
            if (genre != null)
            {
                GenreDTO genreDTO = Mapper.Convert<Genre, GenreDTO>(genre);
                return genreDTO;
            }
            return null;
        }

        public void DeleteGenre(int id)
        {
            GenreRepository.Delete(id);
        }
    }
}