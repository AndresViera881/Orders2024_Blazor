using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly DataDbContext _context;
        public PaisesController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync() 
        {
            var paises = await _context.Paises.ToListAsync();
            return Ok(paises);
        }

        [HttpGet("${id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var pais = await _context.Paises.FirstOrDefaultAsync(x => x.Id == id);
            if(pais == null)
                return NotFound();
            return Ok(pais);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Pais pais) 
        {
            if (pais == null) 
            {
                return BadRequest();
            }
            await _context.AddAsync(pais);
            await _context.SaveChangesAsync();
            return Ok(pais);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Pais pais) 
        {
            _context.Update(pais);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("${id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var pais = await _context.Paises.FirstOrDefaultAsync(x => x.Id == id);
            if (pais == null)
                return NotFound();
            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
