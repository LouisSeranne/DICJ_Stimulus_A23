namespace StimulusAPI.Authorization
{
    public class ModelEnregistrement
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Le nom d'utilisateur est essentiel.")]
        public string UserName { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Le mot de passe est requis.")]
        public string Password { get; set; }
    }
}
