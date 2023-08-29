using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using StimulusFrontEnd.Providers;
using StimulusFrontEnd.Services.Base;

namespace StimulusFrontEnd.Services.Authentification
{
    public class AuthenticationService : IAuthenticationService
    {
        //Le local storage n'est plus utilisé, mais il reste là au cas
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly ISessionStorageService sessionStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, ISessionStorageService sessionStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.sessionStorage = sessionStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(UtilisateurApplication appUser)
        {
            var response = await httpClient.LoginAsync(appUser);

            //Store le token
            await sessionStorage.SetItemAsync("accessToken", response.Token);

            //change l'État d'authorisation de l'application
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();


            return true;
        }

        public async Task Logout()
        {
            await((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
