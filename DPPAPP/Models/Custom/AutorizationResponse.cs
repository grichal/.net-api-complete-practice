﻿namespace DPPAPP.Models.Custom
{
    public class AutorizationResponse
    {

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Resultado { get; set; }
        public string Msg { get; set; }
        public string NombreUsuario { get; set; }
    }
}