using System;
using System.Collections.Generic;
using System.IO;

public class CarteManager
{
    private string[] carte;
    private Dictionary<(int x, int y), string> coordonneesPokemon = new Dictionary<(int x, int y), string>();
    private int joueurX, joueurY;

    public CarteManager(string cheminFichier)
    {
        carte = ChargerCarteDepuisFichier(cheminFichier);
        ChargerCoordonneesPokemon();

        TrouverPositionJoueur();
    }



    private void ChargerCoordonneesPokemon()
    {
        string cheminFichier = "coord.txt";
        if (File.Exists(cheminFichier))
        {
            string[] lignes = File.ReadAllLines(cheminFichier);
            foreach (string ligne in lignes)
            {
                var parties = ligne.Split(',');
                if (parties.Length == 3)
                {
                    int x = int.Parse(parties[0]);
                    int y = int.Parse(parties[1]);
                    string nomPokemon = parties[2];
                    coordonneesPokemon[(x, y)] = nomPokemon;
                }
            }
        }
    }

    public static void InitialiserPokemonSurCarte()
    {
        var carteManager = new CarteManager("map.txt");
    }

    public void GenererPokemonSurCarte()
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


    public string[] ChargerCarteDepuisFichier(string cheminFichier)
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
            if (caseCible == '#' || caseCible == 'P') return; // Bloquer le mouvement si c'est un mur ou la position initiale du joueur


            if (caseCible == 'M' || caseCible == 'F')
            {
                InteragirAvecPNJ(caseCible, joueur);
                return; // Pas de déplacement, mais interaction effectuée
            }


            // Gestion des rencontres avec des Pokémon sauvages
            if (char.IsDigit(caseCible))
            {
                if (coordonneesPokemon.TryGetValue((nouveauX, nouveauY), out string nomPokemon))
                {
                    int niveauPokemon = int.Parse(caseCible.ToString());
                    var pokemonSauvage = PokemonFactory.CreerPokemon(nomPokemon, niveauPokemon);
                    Console.WriteLine($"Vous rencontrez un {nomPokemon} sauvage !");
                    if (Combat.DemarrerCombat(niveauPokemon, joueur, this, pokemonSauvage, nouveauX, nouveauY))
                    {
                        coordonneesPokemon.Remove((nouveauX, nouveauY)); // Retirer le Pokémon de la carte après le combat
                        RetirerPokemonDeLaCarte(nouveauX, nouveauY);
                    }
                }
            }
            else
            {
                // Collecte d'objets
                switch (caseCible)
                {
                    case 'k':
                        joueur.AjouterObjetInventaire("clé");
                        Console.WriteLine("Vous avez trouvé une clé !");
                        break;
                    case 'e':
                        joueur.AjouterObjetInventaire("potion d'experience");
                        Console.WriteLine("Vous avez trouvé une potion d'expérience !");
                        break;
                    case 'v':
                        joueur.AjouterObjetInventaire("potion de vie");
                        Console.WriteLine("Vous avez trouvé une potion de vie !");
                        break;
                    case 'n':
                        joueur.AjouterObjetInventaire("potion de niveau");
                        Console.WriteLine("Vous avez trouvé une potion de niveau !");
                        break;
                }
                if (caseCible == '*')
                {
                    Random rnd = new Random();
                    if (rnd.Next(10) == 0) // 1 chance sur 10
                    {
                        // Sélection aléatoire du niveau du Pokémon entre 4 et 8
                        int niveauPokemon = rnd.Next(4, 9);
                        // Sélectionner un Pokémon aléatoire
                        Pokemon pokemonSauvage = PokemonFactory.AttribuerPokemonAleatoire(niveauPokemon);
                        Combat.DemarrerCombat(niveauPokemon, joueur, this, pokemonSauvage, nouveauX, nouveauY);

                        return; // Empêcher le déplacement après le combat
                    }
                }


                // Si ce n'est pas un buisson et pas un Pokémon, effacer l'objet de la carte
                if (caseCible != '*' && !char.IsDigit(caseCible))
                {
                    RetirerPokemonDeLaCarte(nouveauX, nouveauY);
                }
            }

            joueurX = nouveauX;
            joueurY = nouveauY;
        }
    }





    public void RetirerPokemonDeLaCarte(int x, int y)
    {
        if (carte[y][x] != '#')
        {
            carte[y] = carte[y].Substring(0, x) + ' ' + carte[y].Substring(x + 1);
        }
    }



    public void SauvegarderCarte()
    {
        using (StreamWriter sw = new StreamWriter("map.txt"))
        {
            for (int y = 0; y < carte.Length; y++)
            {
                string ligne = "";
                for (int x = 0; x < carte[y].Length; x++)
                {
                    if (x == joueurX && y == joueurY)
                    {
                        ligne += 'P'; // Marquer la position du joueur
                    }
                    else
                    {
                        ligne += carte[y][x]; // Ajouter le caractère de la carte
                    }
                }
                sw.WriteLine(ligne);
            }
        }
    }

    public void InteragirAvecPNJ(char pnj, Joueur joueur)
    {
        switch (pnj)
        {
            case 'M':
                Console.WriteLine("Maxime : Je te donne ce Charizard de niveau 5 pour t'aider dans ton aventure !");
                joueur.CollectionPokemon.Add(PokemonFactory.CreerPokemon("charizard", 5));
                Console.WriteLine("Vous avez reçu un Charizard de niveau 5 !");
                break;
            case 'F':
                Console.WriteLine("Frank : Veux-tu échanger une potion de soins, une potion de vie et une potion de niveau contre une potion revitalisante ? (o/n)");
                var reponse = Console.ReadLine();
                if (reponse.ToLower() == "o")
                {
                    if (joueur.PeutEchangerPotions())
                    {
                        joueur.EchangerPotionsPourRevitalisante();
                        Console.WriteLine("Échange réussi ! Vous avez reçu une potion revitalisante.");
                    }
                    else
                    {
                        Console.WriteLine("Vous n'avez pas assez de potions pour faire cet échange.");
                    }
                }
                else
                {
                    Console.WriteLine("Peut-être une autre fois alors.");
                }
                break;
            default:
                Console.WriteLine("Vous parlez à un inconnu.");
                break;
        }
        Console.WriteLine("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }


}
