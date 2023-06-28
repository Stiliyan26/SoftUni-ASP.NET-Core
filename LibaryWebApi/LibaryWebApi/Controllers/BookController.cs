using LibaryWebApi.Data.Models;
using LibaryWebApi.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibaryWebApi.Controllers
{
    [EnableCors]
    [Route("/api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepositoryBase<Book> _repo;
        public BookController(IRepositoryBase<Book> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<List<Book>> GetBooksAsync()
        {
            return await _repo.GetAllRecordsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _repo.GetRecordByIdAsync(id);  
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]Book book)
        {
            await _repo.CreateRecordAsync(book);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody]Book book)
        {
            Book? existingBook = await _repo.GetRecordByIdAsync(id);

            if (existingBook == null) 
            {
                return NotFound();
            }

            existingBook.BookAuthor = book.BookAuthor;
            existingBook.BookTitle = book.BookTitle;
            existingBook.BookPublisher = book.BookPublisher;

            await _repo.UpdateRecordAsync(existingBook);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Book bookToDelete = await _repo.GetRecordByIdAsync(id);

            if (bookToDelete == null)
            {
                NotFound();
            }

            await _repo.RemoveRecordAsync(bookToDelete);

            return Ok();
        }
    }
}
