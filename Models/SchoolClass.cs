namespace EasyCheck.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public required string ClassName { get; set; }
        public int TeacherId { get; set; }
    }
}