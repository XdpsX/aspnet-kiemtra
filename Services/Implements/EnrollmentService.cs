using AutoMapper;
using ClassManagement2.DbContexts;
using ClassManagement2.Dtos.Enrollments;
using ClassManagement2.Entities;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassManagement2.Services.Implements
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public EnrollmentService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public EnrollmentDto CreateEnroll(CreateEnrollmentDto createEnrollmentDto)
        {
            // Check if the student and class exist
            var student = _dbContext.Students.Find(createEnrollmentDto.StudentId);
            if (student == null)
                throw new ResourceNotFoundException($"Student with ID {createEnrollmentDto.StudentId} not found.");

            var classEntity = _dbContext.Classes.Find(createEnrollmentDto.ClassId);
            if (classEntity == null)
                throw new ResourceNotFoundException($"Class with ID {createEnrollmentDto.ClassId} not found.");

            // Create new enrollment
            var enrollment = new Enrollment
            {
                StudentId = createEnrollmentDto.StudentId,
                ClassId = createEnrollmentDto.ClassId,
                Student = student,
                Class = classEntity,
                EnrollAt = DateTime.Now // Set current date/time for EnrollAt
            };

            _dbContext.Enrollments.Add(enrollment);
            _dbContext.SaveChanges();

            return _mapper.Map<EnrollmentDto>(enrollment);
        }

        public EnrollmentDto GetEnrollmentById(int studentId, int classId)
        {
            var enrollment = _dbContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Class)
                .FirstOrDefault(e => e.StudentId == studentId && e.ClassId == classId);

            if (enrollment == null)
                throw new ResourceNotFoundException($"Enrollment not found for student ID {studentId} and class ID {classId}");

            return _mapper.Map<EnrollmentDto>(enrollment);
        }
    }
}
