using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB201Initial.DAL;
using PB201Initial.Entities;

namespace PB201Initial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public GroupsController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Groups.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var data = await _context.Groups.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Group group)
        {
            if (string.IsNullOrEmpty(group.GroupNo) || string.IsNullOrEmpty(group.Name))
            {
                return BadRequest();
            }
            group.CreatedDate = DateTime.Now;
            group.ModifiedDate = DateTime.Now;
            group.isDeleted = false;

            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return Created(new Uri($"/api/groups/{group.Id}"), UriKind.Relative,group);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] Group group) 
        {
            if (string.IsNullOrEmpty(group.GroupNo) || string.IsNullOrEmpty(group.Name))
            {
                return BadRequest();
            }
            var data = await _context.Groups.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            data.GroupNo = group.GroupNo;
            data.Name = group.Name;
            data.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id) 
        {
            var data = await _context.Groups.FindAsync(id);
            if (data is null )
            {
                return NotFound();
            }
            _context.Groups.Remove(data);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
