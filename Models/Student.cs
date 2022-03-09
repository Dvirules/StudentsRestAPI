using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsRestAPI.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double GradesAverage { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }


        public Student(string Id ,string firstName, string lastName, int age, double gradesAverage, string schoolName, string schoolAddress)
        {
                this.Id = Id;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Age = age;
                this.GradesAverage = gradesAverage;
                this.SchoolName = schoolName;
                this.SchoolAddress = schoolAddress;
        }
    }
}
