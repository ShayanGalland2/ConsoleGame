using System;

public class Combat
{
    public static void DemarrerCombat(int niveauPokemon, Joueur joueur, CarteManager carteManager, int x, int y)
    {
        // Créer un Pokémon sauvage basé sur le niveau
        Pokemon pokemonSauvage = PokemonFactory.CreerPokemon("pikachu", niveauPokemon);

        Console.WriteLine($"Un {pokemonSauvage.Nom} sauvage de niveau {pokemonSauvage.Niveau} apparaît !");
        AfficherStatsPokemon(pokemonSauvage);

        Console.WriteLine("Choisissez un Pokémon pour combattre :");
        var pokemonJoueur = joueur.ChoisirPokemonPourCombat();

        Console.WriteLine("1. Se battre");
        Console.WriteLine("2. Fuir");
        Console.Write("Choisissez une action: ");
        string choix = Console.ReadLine();

        if (choix == "1")
        {
            bool resultatCombat = LogiqueDeCombat.ExecuterCombat(joueur, pokemonJoueur, pokemonSauvage, niveauPokemon);
            if (resultatCombat)
            {
                joueur.CollectionPokemon.Add(pokemonSauvage);
                carteManager.RetirerPokemonDeLaCarte(x, y);
                Console.WriteLine($"{pokemonSauvage.Nom} capturé !");
                joueur.AjouterExperience(10 * pokemonSauvage.Niveau); // Ajoute de l'expérience
                Console.WriteLine($"Vous avez gagné {10 * pokemonSauvage.Niveau} points d'expérience.");
            }
        }
        else if (choix == "2")
        {
            Console.WriteLine("Vous avez fui le combat.");
        }
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
        // Ajoutez ici d'autres statistiques si nécessaire
        Console.WriteLine("Attaques:");
        foreach (var attaque in pokemon.Attaques)
        {
            Console.WriteLine($"- {attaque.Nom} (Puissance: {attaque.Puissance}, Coût Mana: {attaque.CoutMana})");
        }
    }
}
