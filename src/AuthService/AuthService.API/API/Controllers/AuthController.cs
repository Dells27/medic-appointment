using Microsoft.AspNetCore.Mvc;
using AuthService.Application.DTOs;
using AuthService.Application.UseCases.Login;
using AuthService.Application.UseCases.Register;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace AuthService.API.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly RegisterHandler _registerHandler;
        private readonly LoginHandler _loginHandler;


        public AuthController(RegisterHandler registerHanlder, LoginHandler loginHandler)
        {

            _registerHandler = registerHanlder;
            _loginHandler = loginHandler;

        }

        // ============================================================
        // POST /api/auth/register
        // Registra un nuevo usuario
        // ============================================================

        // POST /api/auth/register/patient
        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _registerHandler.Handle(request, "Patient");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/auth/register/doctor
        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] RegisterRequest request)
        {
            try
            {
                var response = await _registerHandler.Handle(request, "Doctor");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _loginHandler.Login(request);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }


        }


        // ============================================================
        // GET /api/auth/me
        // Devuelve los datos del usuario autenticado
        // Lee los claims del token JWT sin consultar la base de datos
        // ============================================================

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()

        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var fullName = User.FindFirst("fullName")?.Value;

            return Ok(new { userId, email, role, fullName });

        }
    }

}