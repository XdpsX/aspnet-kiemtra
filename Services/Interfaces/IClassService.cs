using ClassManagement2.Dtos.Class;

namespace ClassManagement2.Services.Interfaces
{
    public interface IClassService
    {
        List<ClassDto> GetAllClasses();
        ClassDto GetClassById(int id);
        ClassDto CreateClass(CreateClassDto createClassDto);
        ClassDto UpdateClass(int id, UpdateClassDto updateClassDto);
        void DeleteClass(int id);
    }
}
