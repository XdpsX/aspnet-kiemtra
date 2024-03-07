using ClassManagement2.Dtos.Enrollments;

namespace ClassManagement2.Services.Interfaces
{
    public interface IEnrollmentService
    {
        EnrollmentDto CreateEnroll(CreateEnrollmentDto createEnrollmentDto);
        EnrollmentDto GetEnrollmentById(int studentId, int classId);
    }
}
