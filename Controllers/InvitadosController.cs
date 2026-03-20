using madi_fest_api.Data;
using madi_fest_api.DTOs;
using madi_fest_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace madi_fest_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvitadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearInvitado([FromBody] InvitadoRequest request)
        {
            var invitado = new Invitado
            {
                Nombre = request.Nombre,
                Mensaje = request.Mensaje,
                FestId = request.FestId,
                Confirmado = true,
                Acompanantes = request.Acompanantes?
                    .Select(x => new Acompanante { Nombre = x })
                    .ToList() ?? new List<Acompanante>()
            };

            _context.Invitados.Add(invitado);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                invitado.Id,
                invitado.Nombre,
                invitado.Mensaje,
                Acompanantes = invitado.Acompanantes.Select(a => a.Nombre)
            });
        }

        [HttpGet("por-fest/{festId}")]
        public IActionResult ObtenerPorFest(int festId)
        {
            var data = _context.Invitados
                .Where(i => i.FestId == festId)
                .Select(i => new
                {
                    i.Nombre,
                    i.Mensaje,
                    TotalPersonas = 1 + i.Acompanantes.Count
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("reporte/{festId}")]
        public IActionResult Reporte(int festId)
        {
            var data = _context.Invitados
                .Where(i => i.FestId == festId)
                .Select(i => new
                {
                    Fiesta = i.Fest.Nombre,
                    Invitado = i.Nombre,
                    Mensaje = i.Mensaje,
                    Acompanantes = i.Acompanantes.Select(a => a.Nombre),
                    Total = 1 + i.Acompanantes.Count
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("resumen/{festId}")]
        public IActionResult Resumen(int festId)
        {
            var total = _context.Invitados
                .Where(i => i.FestId == festId)
                .Select(i => 1 + i.Acompanantes.Count)
                .Sum();

            return Ok(new
            {
                FestId = festId,
                TotalPersonas = total
            });
        }
    }
}
