using LibaryWebApi.Data.Models;
using LibaryWebApi.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibaryWebApi.Controllers
{
    [EnableCors]
    [Route("/api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IRepositoryBase<Student> _repo;
        public StudentController(IRepositoryBase<Student> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _repo.GetAllRecordsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _repo.GetRecordByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Student student)
        {
            await _repo.CreateRecordAsync(student);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] Student student)
        {
            Student? existingStudent = await _repo.GetRecordByIdAsync(id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Grade = student.Grade;

            await _repo.UpdateRecordAsync(existingStudent);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Student studentToDelete = await _repo.GetRecordByIdAsync(id);

            if (studentToDelete == null)
            {
                NotFound();
            }

            await _repo.RemoveRecordAsync(studentToDelete);

            return Ok();
        }
    }
}
