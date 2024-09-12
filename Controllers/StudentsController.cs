using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB201Initial.DAL;
using PB201Initial.DTOs.StudentDtos;
using PB201Initial.Entities;

namespace PB201Initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public StudentsController(AppDBContext context) 
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll() 
        {
            var datas = await _context.Students.Include(x=>x.Group).ToListAsync();
            List<StudentGetDto> studentDtos = datas.Select(std=>new StudentGetDto(std.Id,std.Fullname,std.Grade, std.GroupId,std.Group.Name)).ToList();
            return Ok(studentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id) 
        {
            if (id<1)
            {
                return BadRequest();
            }
            var data = await _context.Students.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);

            if (data is null)
            {
                return NotFound();
            }
            StudentGetDto dto = new StudentGetDto(data.Id, data.Fullname, data.Grade, data.GroupId, data.Group.Name);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]StudentCreateDto dto) 
        {
            if (dto is null)
            {
                return BadRequest();
            }

            Student student = new Student()
            {
                Fullname = dto.fullName,
                GroupId = dto.groupId,
                Grade = dto.grade,
                isDeleted = dto.isDeleted,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Created(new Uri($"/api/students/{student.Id}",UriKind.Relative),student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto dto) 
        {
            if (id < 1)
            {
                return BadRequest();
            }

            if (dto is null)
            {
                return BadRequest();
            }
            var data = await _context.Students.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);

            if (data is null)
            {
                return NotFound();
            }

            data.Fullname = dto.fullName;
            data.GroupId = dto.groupId;
            data.Grade = dto.grade;
            data.isDeleted = dto.isDeleted;
            data.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            if (id<1)
            {
                return BadRequest();

            }
            var data = await _context.Students.FindAsync(id);
            await _context.SaveChangesAsync();
            return Ok();    
        }
    }
}
