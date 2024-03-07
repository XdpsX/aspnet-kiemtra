using ClassManagement2.Dtos.Enrollments;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{studentId}/{classId}")]
        public IActionResult GetEnrollmentById(int studentId, int classId)
        {
            try
            {
                var enrollment = _enrollmentService.GetEnrollmentById(studentId, classId);
                return Ok(enrollment);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateEnrollment(CreateEnrollmentDto createEnrollmentDto)
        {
            try
            {
                var enrollment = _enrollmentService.CreateEnroll(createEnrollmentDto);
                return CreatedAtAction(nameof(GetEnrollmentById), new { studentId = enrollment.StudentId, classId = enrollment.ClassId }, enrollment);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
