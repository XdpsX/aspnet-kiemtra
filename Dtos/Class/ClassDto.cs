using ClassManagement2.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClassManagement2.Dtos.Class
{
    public class ClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
