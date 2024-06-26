namespace EasyCheck.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string  TeacherCode { get; set; }
        public required SchoolClass SchoolClass { get; set; }
    }
}