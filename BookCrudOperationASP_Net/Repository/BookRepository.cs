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
            this._entities = bookDbContext;
        }

        public List<Book> GetAllBooks()
        {
            return this._entities.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return this._entities.Books.DefaultIfEmpty(null).FirstOrDefault(b => b.Id == id);
        }

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            else
            {
                Book sameIsbnBook = this._entities.Books.FirstOrDefault(b => b.isbn == book.isbn);
                if (sameIsbnBook == null)
                {
                    this._entities.Books.Add(book);
                    this._entities.SaveChanges();
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
                Book sameIdAndIsbnBook = this._entities.Books.FirstOrDefault(b => b.Id == book.Id && b.isbn == book.isbn);
                if (sameIdAndIsbnBook == null)
                {
                    return false;
                }
                else
                {
                    this._entities.Books.Update(book);
                    this._entities.SaveChanges();
                    return true;
                }
            }
        }

        public bool DeleteBook(int id)
        {
            Book book = this._entities.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return false;
            }
            else
            {
                this._entities.Books.Remove(book);
                this._entities.SaveChanges(true);
                return true;
            }
        }
    }
}