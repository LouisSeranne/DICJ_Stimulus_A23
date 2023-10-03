using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreteurPython;

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

/*
 * Model pour l'interpreteur python. 
 * Nom : corerspond au nom du fichier. Doit contenir l'extension .py
 * Contenu : code que conteint le fichier
 * */