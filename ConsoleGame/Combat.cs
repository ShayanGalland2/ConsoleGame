using System;

public class Combat
{
    public static void DemarrerCombat(int niveauPokemon, Joueur joueur)
    {
        // choisir le pokemon
        Console.WriteLine("Choisissez un Pokémon pour combattre :");
        var pokemonJoueur = joueur.ChoisirPokemonPourCombat();

        // creer un Pokémon sauvage basé sur le niveau
        Pokemon pokemonSauvage = PokemonFactory.CreerPokemon("Pikachu", niveauPokemon);

        Console.WriteLine($"Un {pokemonSauvage.Nom} sauvage de niveau {pokemonSauvage.Niveau} apparaît !");
        AfficherStatsPokemon(pokemonSauvage);

        Console.WriteLine("1. Se battre");
        Console.WriteLine("2. Fuir");
        Console.Write("Choisissez une action: ");
        string choix = Console.ReadLine();

        if (choix == "1")
        {
            Console.WriteLine("Vous avez choisi de vous battre !");
            // Ici, nous pouvons développer la logique de combat
        }
        else if (choix == "2")
        {
            Console.WriteLine("Vous avez réussi à fuir.");
            // Retour à la carte sans changement
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
        // Ajoutez ici d'autres statistiques si nécessaire
        Console.WriteLine("Attaques:");
        foreach (var attaque in pokemon.Attaques)
        {
            Console.WriteLine($"- {attaque.Nom} (Puissance: {attaque.Puissance}, Coût Mana: {attaque.CoutMana})");
        }
    }
}
