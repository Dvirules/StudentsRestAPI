
namespace StudentsRestAPI.Models
{
    public class CreateStudentRequestBody
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double GradesAverage { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        
    }
}
