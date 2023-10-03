using System.Diagnostics;

namespace InterpreteurPython;


//Classe static qui s'occupe de lire le python. Pour faire fonctionner, seulement appeler PythonReader.Run() en fournissant les paramètres appropriés
public static class PythonReader
{
    internal static TempHandler tempHandler = new TempHandler();


    /*Fonction qui s'occupe de lire les fichiers pythons créées par le tempHandler.
    params : 
    main : c'est le fichier que le lecteur va lire. Doit être le main.py
    pythonPath : Correspond au path de l'executable python sur le serveur. Ne devrais pas être hardcodé et ne pas modifier la valeur car 
                 elle est propre au serveur IIS sur lequel le projet tourne
    */
    static private string Read(string main)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        string pythonPath = Environment.GetEnvironmentVariable("StimulusPythonExecutable");

        //Donne le nom du processus
        start.FileName = pythonPath;

        //Les parametres du processus
        start.Arguments = main;

        //Sans console
        start.UseShellExecute = false;

        //Redirection de la sortie
        start.RedirectStandardOutput = true;

        //Ajouter le retour d'erreur
#warning oublie moi pas connard;
        start.RedirectStandardError = true;

        //start.Verb = "runas";
        using (Process process = Process.Start(start))
        {
            string result;
            string error;
            using (StreamReader reader = process.StandardOutput)
            {
                //Lis et retourne le résultat
                result = reader.ReadToEnd();
            }
            using (StreamReader reader2 = process.StandardError)
            {
                error = reader2.ReadToEnd();
            }

            string stringRemove = main.Replace(main.Split("\\").Last(), "");
            string errorMessage = error.Replace(stringRemove, "");

            return $"{result} {errorMessage}";
        }
    }

    /*Commande pour lancer la lecture du code python
     *Params:
     *  code : Il s'agit d'une list d'objet de type FichierPython que l'utilisateur veux faire lire par l'itérateur.
     *  userId : Id de l'utilisateur qui veux faire lire son code. Correspond au DA de l'utilisateur.
     * */
    static public string Run(List<FichierPython> code, string userId)
    {       
        //Appel la fonction qui setup les fichiers temporaire qui vont contenir le code de l'utilisateur
        string pathToUserTempFiles = tempHandler.TempSetup(code, userId);

        //Appel la fonction qui lit le code de l'utilisateur
        string result = Read(Path.Combine(pathToUserTempFiles, "main.py"));

        //Appel la fonction qui va supprimer les fichiers temporaires de l'utilisateur
        tempHandler.TempCleanUp(pathToUserTempFiles);

        //Retourne le résultat
        return result;
    }   
}