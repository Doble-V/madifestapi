using madi_fest_api.Data;
using madi_fest_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace madi_fest_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FestsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FestsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearFest(Fest fest)
        {
            _context.Fests.Add(fest);
            await _context.SaveChangesAsync();
            return Ok(fest);
        }

        [HttpGet]
        public IActionResult ObtenerFests()
        {
            return Ok(_context.Fests.ToList());
        }
    }
}
