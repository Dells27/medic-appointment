using DoctorService.Application.DTOs;
using DoctorService.Application.UseCases.CreateDoctor;
using DoctorService.Application.UseCases.GetDoctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DoctorService.API.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly CreateDoctorHanlder _createDoctorHandler;
        private readonly GetDoctorHandler _getDoctorHandler;

        public DoctorController(CreateDoctorHanlder createDoctorHanlder, GetDoctorHandler getDoctorHandler)
        {
            _createDoctorHandler = createDoctorHanlder;
            _getDoctorHandler = getDoctorHandler;
        }


        // ============================================================
        // POST /api/doctors
        // Crea el perfil de un médico — solo médicos autenticados
        // ============================================================

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create([FromBody] CreateDoctorRequest request)
        {
            try
            {
                // Leer UserId del token JWT automáticamente
                var userIdClaim = User.FindFirst("sub")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim is null)
                    return Unauthorized(new { message = "Token inválido" });

                var userId = Guid.Parse(userIdClaim);
                request.userId = userId;
                var response = await _createDoctorHandler.Handle(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // ============================================================
        // GET /api/doctors
        // Lista todos los médicos disponibles — cualquier usuario
        // ============================================================
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _getDoctorHandler.HandleGetAll();
            return Ok(doctors);
        }


        // ============================================================
        // GET /api/doctors/{id}
        // Obtiene un médico por ID
        // ============================================================
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var doctor = await _getDoctorHandler.HandleGetById(id);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });

            }

        }

        // ============================================================
        // GET /api/doctors/specialty/{specialty}
        // Busca médicos por especialidad
        // ============================================================

        [HttpGet("specialty/{specialty}")]
        [Authorize]
        public async Task<IActionResult> GetBySpecialty(string specialty)
        {
            var doctors = await _getDoctorHandler.HandleGetBySpecialty(specialty);
            return Ok(doctors);
        }


    }
}

