using Application.Abstraction.IServices;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("error")]
        public void Error()
        {
            throw new NotImplementedException("This method is not implemented yet.");
        }

        [HttpPost]

        public async Task<IActionResult> AddStudentAsync([FromBody] StudentDTO studentDto)
        {
            try
            {
                var result =await _studentService.AddStudentAsync(studentDto).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding student: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync().ConfigureAwait(false);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving students: {ex.Message}");
            }
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetStudentByIdAsync(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id).ConfigureAwait(false);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving student: {ex.Message}");
            }
        }
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid id, [FromBody] StudentDTO studentDto)
        {
            var result = await _studentService.UpdateStudentAsync(id, studentDto).ConfigureAwait(false);
            Console.WriteLine("something happened");
            if (result.IsSuccess == true && result.IsError == false)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteStudentAsync(Guid id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id).ConfigureAwait(false);
                return Ok("Student deleted successfully.");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting student: {ex.Message}");
            }
        }
    }
}
