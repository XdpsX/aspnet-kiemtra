using ClassManagement2.Dtos.Common;
using ClassManagement2.Dtos.Student;

namespace ClassManagement2.Services.Interfaces
{
    public interface IStudentService
    {
        PageResultDto<StudentDto> GetAllStudents(StudentFilterDto studentFilterDto);
        StudentDto GetStudentById(int id);
        StudentDto CreateStudent(CreateStudentDto createStudentDto);
        StudentDto UpdateStudent(int id, UpdateStudentDto updateStudentDto);
        void DeleteStudent(int id);
    }
}
