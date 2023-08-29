using StimulusFrontEnd.Services.Base;

namespace StimulusFrontEnd.Services.Authentification
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(UtilisateurApplication appUser);

        public Task Logout();
    }
}
