using System;
using System.Collections.Generic;
using System.IO;

public class CarteManager
{
    private string[] carte;
    private int joueurX, joueurY;

    public CarteManager(string cheminFichier)
    {
        carte = ChargerCarteDepuisFichier(cheminFichier);
        TrouverPositionJoueur();
    }

    public static void InitialiserPokemonSurCarte()
    {
        var carteManager = new CarteManager("map.txt");
        carteManager.GenererPokemonSurCarte();
    }

    private void GenererPokemonSurCarte()
    {
        var coordonneesPokemon = new Dictionary<(int x, int y), string>();
        for (int y = 0; y < carte.Length; y++)
        {
            for (int x = 0; x < carte[y].Length; x++)
            {
                if (char.IsDigit(carte[y][x]))
                {
                    var pokemon = PokemonFactory.AttribuerPokemonAleatoire(int.Parse(carte[y][x].ToString()));
                    coordonneesPokemon.Add((x, y), pokemon.Nom);
                }
            }
        }
        SauvegarderCoordonneesPokemon(coordonneesPokemon);
    }

    private void SauvegarderCoordonneesPokemon(Dictionary<(int x, int y), string> coordonneesPokemon)
    {
        using (StreamWriter sw = new StreamWriter("coord.txt"))
        {
            foreach (var coord in coordonneesPokemon)
            {
                sw.WriteLine($"{coord.Key.x},{coord.Key.y},{coord.Value}");
            }
        }
    }


    private string[] ChargerCarteDepuisFichier(string cheminFichier)
    {
        List<string> lignesCarte = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    lignesCarte.Add(ligne);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Le fichier n'a pas pu être lu:");
            Console.WriteLine(e.Message);
        }

        return lignesCarte.ToArray();
    }

    private void TrouverPositionJoueur()
    {
        for (int y = 0; y < carte.Length; y++)
        {
            for (int x = 0; x < carte[y].Length; x++)
            {
                if (carte[y][x] == 'P')
                {
                    joueurX = x;
                    joueurY = y;
                    // suppr le P de base sur la carte
                    carte[y] = carte[y].Substring(0, x) + ' ' + carte[y].Substring(x + 1); // Remplacer 'P' par un espace
                    return;
                }
            }
        }
    }



    public void AfficherCarte()
    {
        Console.Clear();
        for (int y = 0; y < carte.Length; y++)
        {
            for (int x = 0; x < carte[y].Length; x++)
            {
                if (x == joueurX && y == joueurY)
                {
                    Console.Write('P');
                }
                else
                {
                    Console.Write(carte[y][x]);
                }
            }
            Console.WriteLine();
        }
    }


    public void DeplacerJoueur(ConsoleKeyInfo touche, Joueur joueur)
    {
        int nouveauX = joueurX;
        int nouveauY = joueurY;

        switch (touche.Key)
        {
            case ConsoleKey.UpArrow: nouveauY--; break;
            case ConsoleKey.DownArrow: nouveauY++; break;
            case ConsoleKey.LeftArrow: nouveauX--; break;
            case ConsoleKey.RightArrow: nouveauX++; break;
        }

        if (nouveauX >= 0 && nouveauX < carte[0].Length && nouveauY >= 0 && nouveauY < carte.Length)
        {
            char caseCible = carte[nouveauY][nouveauX];
            if (caseCible == '#') return; // Bloquer le mouvement si c'est un mur

            if (char.IsDigit(caseCible)) // Déclencher un combat si c'est un chiffre
            {
                int niveauPokemon = int.Parse(caseCible.ToString());
                Combat.DemarrerCombat(niveauPokemon, joueur); // Modifier pour passer le niveau du Pokémon
                return; // Empêcher le déplacement sur la case du combat
            }

            joueurX = nouveauX;
            joueurY = nouveauY;
        }
    }
}
