using DPPAPP.Models.Custom;
namespace DPPAPP.Services

    //here we have the interface that had been created where we'll be adding the method that will return the response autorization
{
    public interface IAutorizationService
    {
        Task<AutorizationResponse> ReturnToken(AutorizationRequest autorization);
        Task<AutorizationResponse> ReturnRefreshToken(RefreshTokenRequest RefreshTokenRequest, int idUsuario);
    }
}