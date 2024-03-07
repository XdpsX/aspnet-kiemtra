using AutoMapper;
using ClassManagement2.DbContexts;
using ClassManagement2.Dtos.Class;
using ClassManagement2.Dtos.Student;
using ClassManagement2.Entities;
using ClassManagement2.Exceptions;
using ClassManagement2.Services.Interfaces;

namespace ClassManagement2.Services.Implements
{
    public class ClassService : IClassService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClassService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ClassDto> GetAllClasses()
        {
            var classes = _dbContext.Classes.ToList();
            return _mapper.Map<List<ClassDto>>(classes);
        }

        public ClassDto GetClassById(int id)
        {
            var classEntity = _dbContext.Classes.FirstOrDefault(c => c.Id == id);
            if (classEntity == null)
                throw new ResourceNotFoundException($"Class with ID {id} not found.");

            return _mapper.Map<ClassDto>(classEntity);
        }

        public ClassDto CreateClass(CreateClassDto createClassDto)
        {
            var classEntity = _mapper.Map<Class>(createClassDto);
            _dbContext.Classes.Add(classEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<ClassDto>(classEntity);
        }

        public ClassDto UpdateClass(int id, UpdateClassDto updateClassDto)
        {
            var classEntity = _dbContext.Classes.FirstOrDefault(c => c.Id == id);
            if (classEntity == null)
                throw new ResourceNotFoundException($"Class with ID {id} not found.");

            // Update properties
            _mapper.Map(updateClassDto, classEntity);

            _dbContext.SaveChanges();

            return _mapper.Map<ClassDto>(classEntity);
        }

        public void DeleteClass(int id)
        {
            var classEntity = _dbContext.Classes.FirstOrDefault(c => c.Id == id);
            if (classEntity == null)
                throw new ResourceNotFoundException($"Class with ID {id} not found.");

            _dbContext.Classes.Remove(classEntity);
            _dbContext.SaveChanges();
        }
    }
}
