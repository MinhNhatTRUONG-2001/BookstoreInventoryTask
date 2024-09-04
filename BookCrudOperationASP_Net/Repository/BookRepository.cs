using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCrudOperationASP_Net.Data
{
    public class BookRepository : IBooksRepository
    {
        private readonly BookDbContext _entities;

        public BookRepository(BookDbContext bookDbContext)
        {
            _entities = bookDbContext;
        }

        public List<Book> GetAllBooks()
        {
            return _entities.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _entities.Books.DefaultIfEmpty(null).FirstOrDefault(b => b.Id == id);
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            else
            {
                Book sameIsbnBook = _entities.Books.FirstOrDefault(b => b.isbn == book.isbn);
                if (sameIsbnBook == null)
                {
                    _entities.Books.Add(book);
                    _entities.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            else
            {
                Book existingIsbnBook = _entities.Books.FirstOrDefault(b => b.Id != book.Id && b.isbn == book.isbn);
                if (existingIsbnBook != null)
                {
                    return false;
                }
                else
                {
                    _entities.Books.Update(book);
                    _entities.SaveChanges();
                    return true;
                }
            }
        }

        public bool DeleteBook(int id)
        {
            Book book = _entities.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return false;
            }
            else
            {
                _entities.Books.Remove(book);
                _entities.SaveChanges(true);
                return true;
            }
        }
    }
}