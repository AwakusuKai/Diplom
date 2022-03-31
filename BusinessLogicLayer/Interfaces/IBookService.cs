using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IBookService
    {
        void CreateBook(BookDTO bookDTO);
        void UpdateBook(BookDTO bookDTO);
        void DeleteBook(int id);
        IEnumerable<BookDTO> GetBooks(int? autor, int? genre, int? ph, string name);
        BookDTO GetBook(int? id);
    }
}
