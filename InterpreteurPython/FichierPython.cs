using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteurPython;

/// <summary>
/// Modèle pour l'interpréteur Python
/// 
/// Nom : Correspond au nom du fichier. Il doit avoir l'extension.py
/// Contenu : Contenu du fichier
/// </summary>
public class FichierPython
{
    public string Nom { get; set; }
    public string Contenu { get; set; }

    public FichierPython(string nom, string contenu)
    {
        Nom = nom;
        Contenu = contenu;
    }
}