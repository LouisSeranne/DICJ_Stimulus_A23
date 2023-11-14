using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using StimulusAPI.Authorization;
using StimulusAPI.Models;
using StimulusAPI.Context;
using StimulusAPI.LoginContext;
using Microsoft.Data.SqlClient;
using StimulusAPI.Config;
using Serilog;

namespace StimulusAPI.Controllers
{
    //LoginController a son propre système de logs, 
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<UtilisateurApplication> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly TestStimulusProjet_Evolution _context;
        private readonly _2022_Projet_StimulusLoginContext _loginContext;

        public LoginController(UserManager<UtilisateurApplication> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, TestStimulusProjet_Evolution context, _2022_Projet_StimulusLoginContext loginContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _context = context;
            _loginContext = loginContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Reponse>> Login([FromBody] UtilisateurApplication model)
        {
            
            var user = await userManager.FindByNameAsync(model.UserName);
            var log = Log.ForContext<StimulusAPI.Controllers.LoginController>();
            if (user != null && await userManager.CheckPasswordAsync(user, model.PasswordHash))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                
             
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            log.Information($"FAILED LOGIN ATTEMPT -> Login([FromBody] UtilisateurApplication model = {model}): UNAUTHORIZED LOGIN");
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] ModelEnregistrement model)
        {

            var userExists = await userManager.FindByNameAsync(model.Code);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "L'utilisateur existe!" });

            UtilisateurApplication user = new UtilisateurApplication()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Code
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Création d'utilisateur a échoué! Vérifié les détails de l'utilisateur et réessayer." });
            if (!await roleManager.RoleExistsAsync(RolesUtilisateurs.Etudiant))
                await roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Etudiant));
            if (await roleManager.RoleExistsAsync(RolesUtilisateurs.Etudiant))
            {
                await userManager.AddToRoleAsync(user, RolesUtilisateurs.Etudiant);
            }
            await AjoutBD(model, RolesUtilisateurs.Etudiant);
            return Ok(new Reponse { Status = "Success", Message = "Utilisateur créé avec succès!" });
        }

        [HttpPost]
        [Route("register-prof")]
        public async Task<IActionResult> RegisterProf([FromBody] ModelEnregistrement model)
        {            
            var userExists = await userManager.FindByNameAsync(model.Code);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "L'utilisateur existe!" });

            UtilisateurApplication user = new UtilisateurApplication()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Code
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Création d'utilisateur a échoué! Vérifié les détails de l'utilisateur et réessayer." });
            if (!await roleManager.RoleExistsAsync(RolesUtilisateurs.Professeur))
                await roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Professeur));
            if (await roleManager.RoleExistsAsync(RolesUtilisateurs.Professeur))
            {
                await userManager.AddToRoleAsync(user, RolesUtilisateurs.Professeur);
            }
            await AjoutBD(model, RolesUtilisateurs.Professeur);
            return Ok(new Reponse { Status = "Success", Message = "Utilisateur créé avec succès!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] ModelEnregistrement model)
        {
            var userExists = await userManager.FindByNameAsync(model.Code);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "L'utilisateur existe!" });

            UtilisateurApplication user = new UtilisateurApplication()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Code
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Création d'utilisateur a échoué! Vérifié les détails de l'utilisateur et réessayer." });

            if (!await roleManager.RoleExistsAsync(RolesUtilisateurs.Administrateur))
                await roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Administrateur));
            if (!await roleManager.RoleExistsAsync(RolesUtilisateurs.Etudiant))
                await roleManager.CreateAsync(new IdentityRole(RolesUtilisateurs.Etudiant));

            if (await roleManager.RoleExistsAsync(RolesUtilisateurs.Administrateur))
            {
                await userManager.AddToRoleAsync(user, RolesUtilisateurs.Administrateur);
            }
            await AjoutBD(model, RolesUtilisateurs.Administrateur);
            return Ok(new Reponse { Status = "Success", Message = "Utilisateur créé avec succès!" });
        }

        private async Task<string> GenerateToken(UtilisateurApplication appUser)
        {

            //possible d'utiliser les roles pour faire des claims differents en fonction du role de l'utilisateur
            var roles = await userManager.GetRolesAsync(appUser);
            string errmsg = "user role = " + roles.ToString();

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
            var userClaims = await userManager.GetClaimsAsync(appUser); 

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //il est possible de cree d'autre claim
                //ex:  new Claim(JwtRegisteredClaimNames.Email, appUser.Email), new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires:DateTime.Now.AddHours(3),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Ajoute l'utilisateur dans les tables de données usuelles (autre que ASP.NET)
        /// </summary>
        /// <param name="user"> Informations de l'utilisateur à inscrire </param>
        /// <param name="role"> Rôle attribué à l'utilisateur </param>
        /// <returns> Code de status HTTP en fonction de la réussite de l'action </returns>
        private async Task<IActionResult> AjoutBD(ModelEnregistrement user, string role)
        {
            DbConfig dbConfig = new DbConfig();
            string connectionString = dbConfig.SqlConnStringBuilder.ToString();
            switch (role)
            {
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Erreur inattendue liée à la base de données, contactez un administrateur" });
                case "Etudiant":
                    await using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string insertQuery = "INSERT INTO dbo.etudiant (code_da, nom, prenom, mot_de_passe) VALUES (@Valeur1, @Valeur2, @Valeur3, @Valeur4)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Valeur1", user.Code);
                                command.Parameters.AddWithValue("@Valeur2", user.Nom);
                                command.Parameters.AddWithValue("@Valeur3", user.Prenom);
                                command.Parameters.AddWithValue("@Valeur4", user.Password);
                                command.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Erreur inattendue liée à la base de données, contactez un administrateur" });
                        }
                    }
                    break;
                case "Professeur":
                    await using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string insertQuery = "INSERT INTO dbo.professeur (nom, prenom, mot_de_passe) VALUES (@Valeur1, @Valeur2, @Valeur3)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Valeur1", user.Nom);
                                command.Parameters.AddWithValue("@Valeur2", user.Prenom);
                                command.Parameters.AddWithValue("@Valeur3", user.Password);
                                command.ExecuteNonQuery();
                            }                            
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new Reponse { Status = "Error", Message = "Erreur inattendue liée à la base de données, contactez un administrateur" });
                        }
                    }
                    break;
                case "Administrateur":
                    break;
            }
            return Ok(new Reponse { Status = "200 Ok", Message = "200 Ok" });
        }
    }
}
