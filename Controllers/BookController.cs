using Microsoft.AspNetCore.Mvc;
using DotnetCoreMssql.Models;
using DotnetCoreMssql.Requests;
using DotnetCoreMssql.Repositories;

namespace DotnetCoreMssql.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [Route("/v1/books")]
        public IActionResult GetAll()
        {
            var books = _bookRepository.GetAll();
            return Ok(books);
        }

        [HttpGet]
        [Route("/v1/books/{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Route("/v1/books")]
        public IActionResult Create(CreateRequest bookRequest)
        {
            var book = _bookRepository.Add(bookRequest);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut]
        [Route("/v1/books/{id}")]
        public IActionResult Update(int id, UpdateRequest bookRequest)
        {
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            
            existingBook.Name = bookRequest.Name;
            existingBook.Author = bookRequest.Author;

            _bookRepository.Update(existingBook);
            return NoContent();
        }

        [HttpDelete]
        [Route("/v1/books/{id}")]
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            
            _bookRepository.Delete(book);
            return NoContent();
        }
    }
}
