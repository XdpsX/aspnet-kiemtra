namespace ClassManagement2.Entities
{
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public Student Student { get; set; }
        public Class Class { get; set; }

        public DateTime EnrollAt { get; set; }
    }
}
