using DotnetCoreMssql.Models;
using DotnetCoreMssql.Requests;
using System.Collections.Generic;
using System.Linq;
using DotnetCoreMssql.Data;

namespace DotnetCoreMssql.Repositories
{
    public class BookRepository
    {
        private readonly AppDbContext _dbContext;

        public BookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetAll()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _dbContext.Books.FirstOrDefault(book => book.Id == id);
        }

        public Book Add(CreateRequest bookRequest)
        {
            Book book = new Book(bookRequest);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return book;
        }

        public void Update(Book book)
        {
            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
        }

        public void Delete(Book book)
        {
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
