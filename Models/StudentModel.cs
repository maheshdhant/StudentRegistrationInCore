namespace StudentRegistrationInCore.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public int ClassId { get; set; }

        public string? Phone { get; set; }
    }
}
