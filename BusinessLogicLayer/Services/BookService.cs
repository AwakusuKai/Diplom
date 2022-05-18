using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class BookService : IBookService
    {
        IRepository<Book> BookRepository { get; set; }
        IRepository<Copy> CopyRepository { get; set; }
        IRepository<Order> OrderRepository { get; set; }
        public BookService(IRepository<Book> bookRepository, IRepository<Copy> copyRepository, IRepository<Order> orderRepository)
        {
            BookRepository = bookRepository;
            CopyRepository = copyRepository;
            OrderRepository = orderRepository;
        }

        public void CreateBook(BookDTO bookDTO)
        {
            Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
            BookRepository.Create(book);
        }
        public IEnumerable<BookDTO> GetBooks(int? autor, int? genre, int? ph, string name)
        {
            List<BookDTO> bookDTOs = new List<BookDTO>();
            foreach (Book book in BookRepository.GetAll())
            {
                if ((autor == null || autor == 0 || autor == book.AutorId) &&
                   (genre == null || genre == 0 || genre == book.GenreId) &&
                   (ph == null || ph == 0 || ph == book.PublishingHouseId) &&
                   (String.IsNullOrEmpty(name) || book.Name.Contains(name)))
                {
                    BookDTO bookDTO = Mapper.Convert<Book, BookDTO>(book);
                    bookDTO.Autor = Mapper.Convert<Autor, AutorDTO>(book.Autor);
                    bookDTO.Genre = Mapper.Convert<Genre, GenreDTO>(book.Genre);
                    bookDTO.PublishingHouse = Mapper.Convert<PublishingHouse, PublishingHouseDTO>(book.PublishingHouse);
                    bookDTO.Type = Mapper.Convert<DataAccessLayer.Entities.Type, TypeDTO>(book.Type);
                    bookDTOs.Add(bookDTO);
                }

            }

            return bookDTOs;
        }

        public void UpdateBook(BookDTO bookDTO)
        {
            Book book = Mapper.Convert<BookDTO, Book>(bookDTO);
            BookRepository.Update(book);
        }
        public BookDTO GetBook(int? id)
        {
            var book = BookRepository.GetById(id.Value);
            if (book != null)
            {
                BookDTO bookDTO = Mapper.Convert<Book, BookDTO>(book);
                bookDTO.Autor = Mapper.Convert<Autor, AutorDTO>(book.Autor);
                bookDTO.Genre = Mapper.Convert<Genre, GenreDTO>(book.Genre);
                bookDTO.PublishingHouse = Mapper.Convert<PublishingHouse, PublishingHouseDTO>(book.PublishingHouse);
                bookDTO.Type = Mapper.Convert<DataAccessLayer.Entities.Type, TypeDTO>(book.Type);
                return bookDTO;
            }
            return null;
        }

        public void DeleteBook(int id)
        {
            BookRepository.Delete(id);
        }

        public void CreateCopy(CopyDTO copyDTO)
        {
            Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
            CopyRepository.Create(copy);
        }

        public IEnumerable<CopyDTO> GetCopies(int? book)
        {
            List<CopyDTO> copyDTOs = new List<CopyDTO>();
            foreach (Copy copy in CopyRepository.GetAll())
            {
                if (book == null || book == 0 || book == copy.BookId)
                {
                    CopyDTO copyDTO = Mapper.Convert<Copy, CopyDTO>(copy);
                    copyDTO.Book = GetBook(copy.BookId);
                    copyDTOs.Add(copyDTO);
                }

            }
            return copyDTOs;
        }

        public void UpdateCopy(CopyDTO copyDTO)
        {
            Copy copy = Mapper.Convert<CopyDTO, Copy>(copyDTO);
            CopyRepository.Update(copy);
        }

        public CopyDTO GetCopy(int? id)
        {
            var copy = CopyRepository.GetById(id.Value);
            if (copy != null)
            {
                CopyDTO copyDTO = Mapper.Convert<Copy, CopyDTO>(copy);
                copyDTO.Book = GetBook(copy.BookId);
                return copyDTO;
            }
            return null;
        }

        public void DeleteCopy(int id)
        {
            CopyRepository.Delete(id);
        }

        public bool IsFreeCopiesExist(int bookId)
        {
            IEnumerable<CopyDTO> copyDTOs = GetCopies(bookId);
            foreach(CopyDTO copyDTO in copyDTOs)
            {
                if(copyDTO.Status == 0)
                {
                    return true;
                }
            }
            return false;

        }

        public void CreateOrder(OrderDTO orderDTO)
        {
            Order order = Mapper.Convert<OrderDTO, Order>(orderDTO);
            OrderRepository.Create(order);
            Copy copy = CopyRepository.GetById(orderDTO.CopyId);
            copy.Status = 1;
            CopyRepository.Update(copy);
        }

        public IEnumerable<OrderDTO> GetOrders(int? user)
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order order in OrderRepository.GetAll())
            {
                if (user == null || user == 0 || user == order.UserId)
                {
                    OrderDTO orderDTO = Mapper.Convert<Order, OrderDTO>(order);
                    orderDTO.Copy = GetCopy(order.CopyId);
                    orderDTO.User = Mapper.Convert<User, UserDTO>(order.User);
                    orderDTO.User.Role = Mapper.Convert<Role, RoleDTO>(order.User.Role);
                    orderDTOs.Add(orderDTO);
                }

            }
            return orderDTOs;
        }

        public void UpdateOrder(OrderDTO orderDTO)
        {
            Order order = Mapper.Convert<OrderDTO, Order>(orderDTO);
            OrderRepository.Update(order);
        }

        public OrderDTO GetOrder(int? id)
        {
            var order = OrderRepository.GetById(id.Value);
            if (order != null)
            {
                OrderDTO orderDTO = Mapper.Convert<Order, OrderDTO>(order);
                orderDTO.Copy = GetCopy(order.CopyId);
                orderDTO.User = Mapper.Convert<User, UserDTO>(order.User);
                orderDTO.User.Role = Mapper.Convert<Role, RoleDTO>(order.User.Role);
                return orderDTO;
            }
            return null;
        }

        public void DeleteOrder(int id)
        {
            OrderRepository.Delete(id);
        }

        public int GetFreeCopyId(int bookId)
        {
            IEnumerable<CopyDTO> copies = GetCopies(bookId);
            foreach(CopyDTO copyDTO in copies)
            {
                if(copyDTO.Status == 0)
                {
                    return copyDTO.Id;
                }
            }
            return 0;
        }
    }
}
