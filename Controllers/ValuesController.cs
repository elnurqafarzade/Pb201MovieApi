using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PB201Initial.Entities;

namespace PB201Initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static List<Student> _students = new List<Student>
        {
            new Student()
            {
             Fullname = "Inal Guliyev",
             Grade = 60,
             Id =  1,
            },
            new Student()
            {
                Fullname = "Elcan Shalanov",
                Grade = 90,
                Id = 2,
            },
            new Student()
            {
                Fullname = "Nijat Azizov",
                Grade = 95,
                Id = 3
            },
        };

        [HttpGet("")]
        public IActionResult GetAll() 
        {
            return Ok(_students);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id) 
        {
            var std = _students.FirstOrDefault(x => x.Id == id);
            return Ok(std);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Student student) 
        {
            _students.Add(student);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Student student) 
        {
            var std = _students.Find(x => x.Id == id);
            std.Fullname = student.Fullname;
            std.Grade = student.Grade;
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public IActionResult Update([FromRoute] int id) 
        {
            var std = _students.Find(x => x.Id == id);
            _students.Remove(std);
            return Ok(std);
        }

    }
}
