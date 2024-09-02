using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DPPAPP.Models.Custom;
using DPPAPP.Services;
using System.IdentityModel.Tokens.Jwt;

namespace DPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAutorizationService _autorizationService;
        public UserController(IAutorizationService autorizationService)
        {
            _autorizationService = autorizationService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AutorizationRequest autorization)
        {
            var Authorization_result = await _autorizationService.ReturnToken(autorization);
            if (Authorization_result == null)
                return Unauthorized();

            return Ok(Authorization_result);
        }

        [HttpPost]
        [Route("getRefreshToken")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var TokenSuposelyExpirated = tokenHandler.ReadJwtToken(request.ExpiredToken);

            if (TokenSuposelyExpirated.ValidTo > DateTime.UtcNow)
                return BadRequest(new AutorizationResponse { Resultado = false, Msg = " Token expired" });

            string idUsuario = TokenSuposelyExpirated.Claims.First(x =>
            x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();

            var autorizationResponse = await _autorizationService.ReturnRefreshToken(request, int.Parse(idUsuario));

            if (autorizationResponse == null)
                return BadRequest(autorizationResponse);
            else
                return Ok(autorizationResponse);

        }
    }
}