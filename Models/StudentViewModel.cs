using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationInCore.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int ClassId { get; set; }
        public int? DocId { get; set; }
        public string? Phone { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public int? GenderId { get; set; }
        public int? ImageId { get; set; }
        public string? Hobbies { get; set; }
        public List<Dropdown> ClassList { get; set; }
        public List<Dropdown> GenderList { get; set; }
        public List<HobbyModel> hobbyModel { get; set; }

        public SingleFileModel singleFileModel { get; set; }
        public PhotoUpload photoUpload{ get; set; }
         
        internal string? HobbyNotSelectedError()
        {
            throw new NotImplementedException("Please select atleast one hobby!");
        }
    }

    public  class HobbyModel
    { 
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
        public bool IsActive { get; set; }
    }

    public class Dropdown
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class ClassModel
    {
        public int ClassId { get; set; }

        public int Grade { get; set; }

        public string ClassTeacher { get; set; } = null!;

        public int? Total { get; set; }
    }

    public class ResponseModel
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsResponse { get; set; }
    }

    public class SingleFileModel : ResponseModel
    {

        [Required(ErrorMessage = "Please select file")]
        public IFormFile File { get; set; }
    }

    public class PhotoUpload
    {
        [Required(ErrorMessage = "Please select file")]
        public IFormFile Photo { get; set; }
    }
}
