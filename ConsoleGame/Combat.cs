using System;

public class Combat
{
    public static bool DemarrerCombat(int niveauPokemon, Joueur joueur, CarteManager carteManager, Pokemon pokemonSauvage, int x, int y)
    {
        Console.WriteLine($"Un {pokemonSauvage.Nom} sauvage de niveau {pokemonSauvage.Niveau} apparaît !");
        AfficherStatsPokemon(pokemonSauvage);

        var pokemonJoueur = joueur.ChoisirPokemonPourCombat();

        Console.WriteLine("\n1. Se battre");
        Console.WriteLine("2. Fuir");
        Console.Write("Choisissez une action: ");
        string choix = Console.ReadLine();

        if (choix == "1")
        {
            bool resultatCombat = LogiqueDeCombat.ExecuterCombat(joueur, pokemonJoueur, pokemonSauvage, niveauPokemon, carteManager, x, y);
            if (resultatCombat)
            {
                joueur.CollectionPokemon.Add(pokemonSauvage);
                carteManager.RetirerPokemonDeLaCarte(x, y);
                Console.WriteLine($"{pokemonSauvage.Nom} capturé !");
                joueur.AjouterExperience(10 * pokemonSauvage.Niveau);
                Console.WriteLine($"Vous avez gagné {10 * pokemonSauvage.Niveau} points d'expérience.");
                return true;
            }
        }
        else if (choix == "2")
        {
            pokemonJoueur.EstKO = true;
            pokemonJoueur.KoRestantMatchs = 3;

            Console.WriteLine("Vous avez fui le combat. Votre Pokémon est maintenant KO pour 3 matchs.");

            return false;
        }
        return false;
    }


    private static void AfficherStatsPokemon(Pokemon pokemon)
    {
        Console.WriteLine($"Nom: {pokemon.Nom}");
        Console.WriteLine($"Niveau: {pokemon.Niveau}");
        Console.WriteLine($"Attaque: {pokemon.Attaque}");
        Console.WriteLine($"Défense: {pokemon.Defense}");
        Console.WriteLine($"Vitesse: {pokemon.Vitesse}");
        Console.WriteLine($"Points de Vie: {pokemon.PointsDeVie}");
        Console.WriteLine($"Points de Mana: {pokemon.PointsDeMana}");
        Console.WriteLine($"Précision: {pokemon.Precision}");
        Console.WriteLine($"Type: {pokemon.Type}");
        Console.WriteLine("Attaques:");
        foreach (var attaque in pokemon.Attaques)
        {
            Console.WriteLine($"- {attaque.Nom} (Puissance: {attaque.Puissance}, Coût Mana: {attaque.CoutMana})");
        }
    }
}
