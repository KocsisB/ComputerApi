using ComputerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsController : ControllerBase
    {
        private readonly ComputerContext computerContext;

        public OsController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<OS>> Post(CreatedOsDto createOsDto)
        {
            var os = new OS
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name,
                CreatedTime = DateTime.Now
            };

            if (os != null)
            {
                await computerContext.Os.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<OS>> Get()
        {
            return Ok(await computerContext.Os.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OS>> GetById(Guid id)
        {
            var os = await computerContext.Os.FirstOrDefaultAsync(o => o.Id == id);
            if (os != null)
            {
                return Ok(os);
            }
            return BadRequest();
        }
    }
}
