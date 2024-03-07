using ClassManagement2.Dtos.Enrollments;
using ClassManagement2.Entities;

namespace ClassManagement2.Dtos.Student
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public ICollection<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
    }
}
