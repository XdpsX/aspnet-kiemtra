using ClassManagement2.Dtos.Class;
using ClassManagement2.Dtos.Student;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public IActionResult GetAllClasses()
        {
            var classes = _classService.GetAllClasses();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassById(int id)
        {
            try
            {
                var classEntity = _classService.GetClassById(id);
                return Ok(classEntity);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateClass([FromBody] CreateClassDto createClassDto)
        {
            var classEntity = _classService.CreateClass(createClassDto);
            return CreatedAtAction(nameof(GetClassById), new { id = classEntity.Id }, classEntity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClass(int id, [FromBody] UpdateClassDto updateClassDto)
        {
            if (id != updateClassDto.Id)
            {
                return BadRequest("Class ID mismatch");
            }
            try
            {
                var classEntity = _classService.UpdateClass(id, updateClassDto);
                return Ok(classEntity);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClass(int id)
        {
            try
            {
                _classService.DeleteClass(id);
                return NoContent();
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
