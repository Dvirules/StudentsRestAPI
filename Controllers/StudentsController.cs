#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsRestAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using StudentsRestAPI.Logic;
using StackExchange.Redis;

namespace StudentsRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Context _context;
        private readonly IStudentsLogic _studentLogic;
        private readonly IDatabase _database;

        public StudentsController(Context context, IStudentsLogic studentLogic, IDatabase DataBase)
        {
            _context = context;
            _studentLogic = studentLogic;
            _database = DataBase;
        }

        // GET: api/launch
        [HttpGet("/api/launch")]
        public string Welcome()
        {
            return "Welcome! \nRouting is possible to the following URLs: \nGET: api/Students to watch all students \n" +
                "GET: api/Students/id to watch a specific student with the matching id \n" +
                "PUT: api/Students/id to modify a specific student with the matching id \n" +
                "POST: api/Students to add a student \n" +
                "DELETE: api/Students/id to delete a specific student with the matching id";
        }

        // GET: api/Students
        [HttpGet]
        public string GetAllStudents()
        {
            if (_database.KeyExists("GetAllStudents"))
                return $"The same request was sent less than 10 seconds ago, resubmitting previous response:" +
                    $"\n \n {_database.StringGet("GetAllStudents")}";
            else
            {
                _database.StringSet("GetAllStudents", _studentLogic.GetAllStudents(), new TimeSpan(0, 0, 10));
                return _studentLogic.GetAllStudents();
            }
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public string GetStudent(string id)
        {
            if (_database.KeyExists(id))
                return $"The same request was sent less than 10 seconds ago, resubmitting previous response:" +
                    $"\n \n {_database.StringGet(id)}";
            else
            {
                try
                {
                    _database.StringSet(id, this._studentLogic.GetStudent(id), new TimeSpan(0, 0, 10));
                    return this._studentLogic.GetStudent(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    this.HttpContext.Response.StatusCode = 404;
                    return "No matching ID was found";
                }
            }
        }

        //PUT: api/Students/5
        [HttpPut("{id}")]
        public string ModifyStudent(string id, [FromBody] CreateStudentRequestBody studentInput)
        {
            if (studentInput.Age <= 18 && studentInput != null)
            {
                try
                {
                    return this._studentLogic.ModifyStudent(id, studentInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    this.HttpContext.Response.StatusCode = 404;
                    return "No matching ID was found";
                }
            }
            else
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return "Bad request - a student's age cannot be bigger than 18";
            }
        }

        // POST: api/Students
        [HttpPost]
        public string AddStudent([FromBody] CreateStudentRequestBody studentInput)
        {
           if(studentInput.Age <= 18 && studentInput != null)
              return this._studentLogic.AddStudent(studentInput);
            else
            {
                this.HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                return "Bad request - a student's age cannot be bigger than 18";
            }
        }

        //DELETE: api/Students/5
        [HttpDelete("{id}")]
        public string DeleteStudent(string id)
        {
            try
            {
                return this._studentLogic.DeleteStudent(id);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
                this.HttpContext.Response.StatusCode = 404;
                return "No student with a matching ID was found";
            }
        }
    }
}
