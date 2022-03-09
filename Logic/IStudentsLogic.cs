using StudentsRestAPI.Models;

namespace StudentsRestAPI.Logic
{
    public interface IStudentsLogic
    {
        public string GetAllStudents();
        public string GetStudent(string id);
        public string AddStudent(CreateStudentRequestBody studentInput);
        public string DeleteStudent(string id);
        public string ModifyStudent(string id, CreateStudentRequestBody studentInput);
    }
}
