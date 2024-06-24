namespace EasyCheck.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string  StudentCode { get; set; }
        public string ParentContact { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
    }
}
