using AutoMapper;
using ClassManagement2.Dtos.Class;
using ClassManagement2.Dtos.Enrollments;
using ClassManagement2.Dtos.Student;
using ClassManagement2.Entities;

namespace ClassManagement2.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();

            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<CreateClassDto, Class>();
            CreateMap<UpdateClassDto, Class>();

            CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        }
    }
}
