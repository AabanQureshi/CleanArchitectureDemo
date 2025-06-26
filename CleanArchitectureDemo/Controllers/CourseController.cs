using Application.Abstraction.IRepositories;
using Application.Abstraction.IServices;
using Application.DTOs;
using Domain.Entites;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourseAsync([FromBody] CourseDTO courseDto)
        {
            try
            {
                var course = new Course
                {
                    Title = courseDto.Title,
                    Description = courseDto.Description,
                    StartDate = courseDto.StartDate,
                    EndDate = courseDto.EndDate
                };
                var result = await _courseService.AddCourseAsync(courseDto).ConfigureAwait(false);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding course: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesAsync()
        {
            try
            {
                var courses = await _courseService.GetCoursesAsync().ConfigureAwait(false);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving courses: {ex.Message}");
            }
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetCourseByIdAsync(Guid id)
        {
            try
            {
                var course = await _courseService.GetCourseByIdAsync(id).ConfigureAwait(false);
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found.");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving course: {ex.Message}");
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateCourseAsync(Guid id, [FromBody] CourseDTO courseDto)
        {
            try
            {
                await _courseService.UpdateCourseAsync(id, courseDto).ConfigureAwait(false);
                return Ok("Course updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating course: {ex.Message}");
            }
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteCourseAsync(Guid id)
        {
            try
            {
               
                await _courseService.DeleteCourseAsync(id).ConfigureAwait(false);
                return Ok("Course deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting course: {ex.Message}");
            }
        }
    }

}
