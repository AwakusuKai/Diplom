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

        void CreateCopy(CopyDTO copyDTO);
        void UpdateCopy(CopyDTO copyDTO);
        void DeleteCopy(int id);
        IEnumerable<CopyDTO> GetCopies(int? book);
        CopyDTO GetCopy(int? id);

        void CreateOrder(OrderDTO orderDTO);
        void UpdateOrder(OrderDTO orderDTO);
        void DeleteOrder(int id);
        IEnumerable<OrderDTO> GetOrders(int? orderDTO);
        OrderDTO GetOrder(int? orderDTO);

        bool IsFreeCopiesExist(int bookId);

        int GetFreeCopyId(int bookId);
    }
}
