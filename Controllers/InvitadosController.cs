using madi_fest_api.Data;
using madi_fest_api.DTOs;
using madi_fest_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                FestId = request.FestId,
                Name = request.Nombre,
                LastName = request.Apellido,
                Message = request.Mensaje,
                ConfirmedCompanions = request.ConfirmaAcomp, // El bit de la base de datos
                Confirmed = true,
                DateRegister = DateTime.Now,

                Acompanantes = request.Acompanantes?
                    .Select(x => new Acompanante { Nombre = x })
                    .ToList() ?? new List<Acompanante>()
            };

            _context.Invitados.Add(invitado);
            await _context.SaveChangesAsync();

            // Devolvemos la data procesada
            return Ok(new
            {
                invitado.Id,
                NombreCompleto = $"{invitado.Name} {invitado.LastName}",
                invitado.ConfirmedCompanions,
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
                    // Combinamos Nombre y Apellido para la vista rápida
                    NombreCompleto = $"{i.Name} {i.LastName}",
                    i.Message,
                    TotalPersonas = 1 + i.Acompanantes.Count
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("reporte/{festId}")]
        public IActionResult Reporte(int festId)
        {
            var data = _context.Invitados
                .Include(i => i.Fest) // Ahora i.Fest ya existe en el modelo
                .Where(i => i.FestId == festId)
                .Select(i => new
                {
                    Fiesta = i.Fest.Name, // Nombre de la fiesta desde la clase Fest
                    Invitado = $"{i.Name} {i.LastName}",
                    Mensaje = i.Message,
                    ConfirmaAcomp = i.ConfirmedCompanions,
                    Acompanantes = i.Acompanantes.Select(a => a.Nombre),
                    Total = 1 + i.Acompanantes.Count
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("resumen/{festId}")]
        public IActionResult Resumen(int festId)
        {
            // Este método se mantiene casi igual, solo aseguramos que use la nueva estructura
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
