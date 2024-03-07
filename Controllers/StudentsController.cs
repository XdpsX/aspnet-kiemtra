using ClassManagement2.Dtos.Student;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAllStudents([FromQuery] StudentFilterDto studentFilterDto)
        {
            var students = _studentService.GetAllStudents(studentFilterDto);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            try
            {
                var createdStudent = _studentService.CreateStudent(createStudentDto);
                return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
            }
            catch (UniquePropertyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            if (id != updateStudentDto.Id)
            {
                return BadRequest("Student ID mismatch");
            }
            try
            {
                updateStudentDto.Id = id;
                var updatedStudent = _studentService.UpdateStudent(id,updateStudentDto);
                return Ok(updatedStudent);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniquePropertyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                return NoContent();
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
