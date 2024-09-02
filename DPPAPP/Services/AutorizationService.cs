using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DPPAPP.Models;
using DPPAPP.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace DPPAPP.Services
{
        //here we add the interfaze we integrate before this step, IAutorizationService
        //to add the interface, we place the mouse over the name of the interface after the two dots (:)
        //and then we press ctl+. and then click on add interface.
    public class AutorizationService : IAutorizationService
    {
        //now let's first declare the variables since we're going to be using our database context.
        //and we are also going to be reading that key that we have in the appsettings.json file.
        private readonly ParpolContext _context;
        private readonly IConfiguration _configuration;

        //recive the values of those two variables into a constructor.
        public AutorizationService(ParpolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerateToken(string idUsuario) 
        {
            var key = _configuration.GetValue<string>("JwtSettings:key"); //get the value of the key
            var keyBytes = Encoding.ASCII.GetBytes(key);//convert the key into an array

            var claims = new ClaimsIdentity();//create the info of the user for the token
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var CredentialsToken = new SigningCredentials(//create a credential for the token
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature//algorithm security
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = CredentialsToken
            };//create details of the token

            var tokenHandler = new JwtSecurityTokenHandler(); //creates the controllers of the jwt
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);//here creates configuration token

            string tokenCreated = tokenHandler.WriteToken(tokenConfig);

            return tokenCreated;
        }

        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var mg = RandomNumberGenerator.Create())
            {
                mg.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            };
            return refreshToken;
        }

        private async Task<AutorizationResponse> SaveHistoryRefreshToken(int idUsuario,
        string token,
        string refreshtoken)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x=> x.IdUsiario == idUsuario);

            var HistoryRefreshToken = new HistorialRefreshToken
            {
                IdUsuario = idUsuario,
                Token = token,
                RefreshToken = refreshtoken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddHours(24),
            };

            await _context.HistorialRefreshTokens.AddAsync(HistoryRefreshToken);
            await _context.SaveChangesAsync();

            return new AutorizationResponse { Token = token, RefreshToken = refreshtoken, Resultado = true, Msg = "ok", NombreUsuario = usuario.NombreUsuario };
        }
        
        //and here we have already placed the method of the interface we created before.
        public async Task<AutorizationResponse> ReturnToken(AutorizationRequest autorization)
        {

            var user_finded = _context.Usuarios.FirstOrDefault(x =>
            x.NombreUsuario == autorization.UserName &&
            x.Clave == autorization.Password
            );

            if (user_finded == null)
            {
                return await Task.FromResult<AutorizationResponse>(null);
            };

            string Password = autorization.Password;

            if (!user_finded.Clave.Equals(Password)) {
            return await Task.FromResult<AutorizationResponse>( null );
            };

            string TokenCreated = GenerateToken(user_finded.IdUsiario.ToString());

            string refreshTokenCreated = GenerateRefreshToken();

            //return new AutorizationResponse() { Token = TokenCreated, Resultado = true, Msg = "OK"};

            return await SaveHistoryRefreshToken(user_finded.IdUsiario, TokenCreated, refreshTokenCreated);
        }

        public async Task<AutorizationResponse> ReturnRefreshToken(RefreshTokenRequest RefreshTokenRequest, int IdUsiario)
        {
            var refreshTokenFinded = _context.HistorialRefreshTokens.FirstOrDefault(x=>
            x.Token == RefreshTokenRequest.ExpiredToken &&
            x.RefreshToken == RefreshTokenRequest.RefreshToken &&
            x.IdUsuario == IdUsiario);

            if (refreshTokenFinded == null)
                return new AutorizationResponse { Resultado = false, Msg ="Token doesnt exist"};

            var refreshtokenCreated = GenerateRefreshToken();
            var tokenCreated = GenerateToken(IdUsiario.ToString());

            return await SaveHistoryRefreshToken(IdUsiario, tokenCreated, refreshtokenCreated);
              
        }
    }
}