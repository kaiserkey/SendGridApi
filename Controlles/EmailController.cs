using Microsoft.AspNetCore.Mvc;
using SendGridApi.Interfaces;
using SendGridApi.Models;

namespace SendGridApi.Controlles
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Nombre))
            {
                return BadRequest("El email y el nombre son obligatorios.");
            }

            var resultado = await _emailService.EnviarEmailAsync(request.Email, request.Nombre);

            if (resultado)
                return Ok(new { mensaje = "Email enviado con éxito" });
            else
                return StatusCode(500, "Error al enviar el email.");
        }
    }
}
