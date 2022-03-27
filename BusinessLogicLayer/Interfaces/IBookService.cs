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
        IEnumerable<BookDTO> GetBooks();
        BookDTO GetBook(int? id);
    }
}
