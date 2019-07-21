using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDomain;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ConcurrentDictionary<int, Student> _students;
        private const string NameOfRoute = "Students";


        public StudentsController([FromServices] ConcurrentDictionary<int, Student> students)
        {
            _students = students;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return Ok(_students.Values);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = NameOfRoute)]
        public ActionResult<Student> Get(int id)
        {
            if (_students.TryGetValue(id, out var student))
            {
                var studentDTO = new DTO.Student()
                {
                    Id = student.Key,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    ZipCode = student.ZipCode
                };

                return Ok(studentDTO);
            }

            return BadRequest($"Could not find student id: {id}");
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] DTO.Student dtoStudent)
        {
            var student = dtoStudent.ToStudent();
            if (student.IsFailure)
                return BadRequest(student.Errors);

            if (_students.TryAdd(dtoStudent.Id, student))
            {
                return CreatedAtRoute(NameOfRoute, new { id = student.Value.Key }, student.Value);
            }

            return Conflict(student.Value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}