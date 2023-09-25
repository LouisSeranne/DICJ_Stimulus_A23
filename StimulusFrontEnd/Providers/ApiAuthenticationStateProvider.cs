using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace StimulusFrontEnd.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        //Le local storage n'est plus utilisé, mais il reste là au cas
        private readonly ILocalStorageService localStorage;
        private readonly ISessionStorageService sessionStorage;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, ISessionStorageService sessionStorage)
        {
            this.localStorage = localStorage;
            this.sessionStorage = sessionStorage;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        /// Cette fonction vérifie si vous êtes authentifié ou pas
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //créé un Claim qui est nécéssaire pour les JWT. Voir la doc ci-dessous pour comprendre les JWT.
            //https://auth0.com/docs/secure/tokens/json-web-tokens/json-web-token-claims
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            
            //va chercher le token dans le session storage
            var savedToken = await sessionStorage.GetItemAsync<string>("accessToken");
            //Dans le cas où le token est null, l'application crash, donc on doit en généré un temporaire sans Claims 
            //cela va permettre à l'utilisateur d'utiliser ses credentials afin de généré un token apropiré
            if (savedToken==null)
            {
                //await sessionStorage.SetItemAsync("userClaim", user);
                return new AuthenticationState(user);
                //await sessionStorage.SetItemAsync("accessToken", jwtSecurityTokenHandler.CreateJwtSecurityToken().RawData);
                //savedToken = await sessionStorage.GetItemAsync<string>("accessToken");

            }
            
            //génération du token de sécurité à l'aide de ce qu'il y a dans le session storage
            var tokenContent = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(savedToken);

            //Vérification si le token est null et s'il n'est pas expiré
            if (tokenContent.ValidTo < DateTime.Now)
            {
                return new AuthenticationState(user);
            }
            //Va chercher les Claims
            var claims = await GetClaims();
            //Utilisation des claims pour assigné le role de l'utilisateur
            user = new ClaimsPrincipal(new ClaimsIdentity(claims,"role"));
           
            //await sessionStorage.SetItemAsync("userClaim", user.Identities);

            return new AuthenticationState(user);
        }

        public async Task LoggedIn()
        {
            
            var claims = await GetClaims();
            //Boubidibap est un nom bidon que le Claim a besoin pour être généré
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Boubidibap"));
            var authState = Task.FromResult(new AuthenticationState(user));
            //cette variable de session storage permet de donner le numéro de DA au Graph
            await sessionStorage.SetItemAsync("idEtudiant", user.Identity.Name);
            
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// Vide tout ce qui est lié à l'utilisateur
        /// </summary>
        public async Task LoggedOut()
        {
            //await sessionStorage.RemoveItemAsync("accessToken");
            await sessionStorage.ClearAsync();
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var savedToken = await sessionStorage.GetItemAsync<string>("accessToken");
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            var claims = tokenContent?.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, "jwt"));

            return claims;
        }
    }
}
