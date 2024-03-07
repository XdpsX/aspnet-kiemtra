using AutoMapper;
using ClassManagement2.DbContexts;
using ClassManagement2.Dtos.Class;
using ClassManagement2.Dtos.Common;
using ClassManagement2.Dtos.Enrollments;
using ClassManagement2.Dtos.Student;
using ClassManagement2.Entities;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassManagement2.Services.Implements
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StudentDto CreateStudent(CreateStudentDto createStudentDto)
        {
            // Check uniqueness of Code and Email
            if (_context.Students.Any(s => s.Code == createStudentDto.Code))
            {
                throw new UniquePropertyException($"Student with code {createStudentDto.Code} already exists.");
            }

            if (_context.Students.Any(s => s.Email == createStudentDto.Email))
            {
                throw new UniquePropertyException($"Student with email {createStudentDto.Email} already exists.");
            }

            var studentEntity = _mapper.Map<Student>(createStudentDto);

            _context.Students.Add(studentEntity);
            _context.SaveChanges();

            return _mapper.Map<StudentDto>(studentEntity);
        }

        public void DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                throw new ResourceNotFoundException($"Student with id {id} not found.");
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        /* public PageResultDto<StudentDto> GetAllStudents()
         {
             var students = _context.Students.ToList();
             var studentDtos = _mapper.Map<List<StudentDto>>(students);
             var totalItems = students.Count;
             return new PageResultDto<StudentDto>
             {
                 Items = studentDtos,
                 TotalItem = totalItems
             }; ;
         }*/

        public PageResultDto<StudentDto> GetAllStudents(StudentFilterDto studentFilterDto)
        {
            // Query students from database
            var query = _context.Students.AsQueryable();

            // Filter by keyword
            if (!string.IsNullOrEmpty(studentFilterDto.Keyword))
            {
                query = query.Where(s => s.Name.Contains(studentFilterDto.Keyword));
            }

            // Filter by age range
            if (studentFilterDto.StartAge != null)
            {
                query = query.Where(s => s.Age >= studentFilterDto.StartAge);
            }
            if (studentFilterDto.EndAge != null)
            {
                query = query.Where(s => s.Age <= studentFilterDto.EndAge);
            }

            // Count total items
            var totalItems = query.Count();

            // Calculate total pages
            var totalPages = (int)Math.Ceiling((double)totalItems / studentFilterDto.PageSize);


            // Paginate
            query = query.Skip(studentFilterDto.Skip()).Take(studentFilterDto.PageSize);

            // Retrieve students
            var students = query.ToList();

            // Map to DTO
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return new PageResultDto<StudentDto>
            {
                Items = studentDtos,
                PageSize = studentFilterDto.PageSize,
                PageIndex = studentFilterDto.PageIndex,
                TotalPages = totalPages,
                TotalItem = totalItems
            };
        }

        public StudentDto GetStudentById(int id)
        {
            var student = _context.Students
                .Include(s => s.Enrollments)
                .FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                throw new ResourceNotFoundException($"Student with id {id} not found.");
            }

            //return _mapper.Map<StudentDto>(student);
            var studentDto = new StudentDto
            {
                Id = student.Id,
                Code = student.Code,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Enrollments = student.Enrollments.Select(e => new EnrollmentDto
                {
                    StudentId = e.StudentId,
                    ClassId = e.ClassId,
                    EnrollAt = e.EnrollAt
                }).ToList()
            };

            return studentDto;
        }

        public StudentDto UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            // Check uniqueness of Code and Email
            var existingStudent = _context.Students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                throw new ResourceNotFoundException($"Student with id {id} not found.");
            }

            if (_context.Students.Any(s => s.Id != id && s.Code == updateStudentDto.Code))
            {
                throw new UniquePropertyException($"Another student with code {updateStudentDto.Code} already exists.");
            }

            if (_context.Students.Any(s => s.Id != id && s.Email == updateStudentDto.Email))
            {
                throw new UniquePropertyException($"Another student with email {updateStudentDto.Email} already exists.");
            }

            _mapper.Map(updateStudentDto, existingStudent);

            _context.SaveChanges();

            return _mapper.Map<StudentDto>(existingStudent);
        }
    }
}
