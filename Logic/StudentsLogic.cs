using StudentsRestAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentsRestAPI.Logic
{
    public class StudentsLogic : IStudentsLogic
    {
        private Dictionary<string, Student> allStudents = new Dictionary<string, Student>();

        public string GetAllStudents()
        {
            return JsonSerializer.Serialize(allStudents);
        }
        public string GetStudent(string id)
        {
            if (allStudents.ContainsKey(id))
                return JsonSerializer.Serialize(allStudents[id]);
            else
                throw new KeyNotFoundException();
                
        }

        public string AddStudent(CreateStudentRequestBody stuInput)
        {
                string id = Guid.NewGuid().ToString();
                allStudents.Add(id, new Student(id, stuInput.FirstName, stuInput.LastName, stuInput.Age, stuInput.GradesAverage,
                    stuInput.SchoolName, stuInput.SchoolAddress));
                return $"New student {id} has been added!";
        }

        public string DeleteStudent(string id)
        {
            if (allStudents.ContainsKey(id))
            {
                allStudents.Remove(id);
                return $"Student {id} has been deleted!";
            }
            else throw new KeyNotFoundException();
        }

        public string ModifyStudent(string id, CreateStudentRequestBody studentInput)
        {
            if (allStudents.ContainsKey(id))
            {

                if (allStudents[id].FirstName != studentInput.FirstName)
                    allStudents[id].FirstName = studentInput.FirstName;

                if (allStudents[id].LastName != studentInput.LastName)
                    allStudents[id].LastName = studentInput.LastName;

                if (allStudents[id].Age != studentInput.Age)
                    allStudents[id].Age = studentInput.Age;

                if (allStudents[id].GradesAverage != studentInput.GradesAverage)
                    allStudents[id].GradesAverage = studentInput.GradesAverage;

                if (allStudents[id].SchoolName != studentInput.SchoolName)
                    allStudents[id].SchoolName = studentInput.SchoolName;

                if (allStudents[id].SchoolAddress != studentInput.SchoolAddress)
                    allStudents[id].SchoolAddress = studentInput.SchoolAddress;

                return "Student succussfuly modified!";
            }
            else throw new KeyNotFoundException();
        }

    }
}
