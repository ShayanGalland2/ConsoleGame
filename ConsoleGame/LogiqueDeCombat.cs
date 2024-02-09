using System;
using System.Linq;

public static class LogiqueDeCombat
{
    public static bool ExecuterCombat(Joueur joueur, Pokemon pokemonJoueur, Pokemon pokemonSauvage, int niveauPokemon)
    {
        bool combatContinue = true;
        bool joueurGagne = false;

        Console.WriteLine($"Un combat commence entre {pokemonJoueur.Nom} et {pokemonSauvage.Nom}!");
        AfficherStats("Joueur", pokemonJoueur);
        AfficherStats("Sauvage", pokemonSauvage);

        while (combatContinue)
        {
            // Tour du joueur
            Console.WriteLine("C'est votre tour. Choisissez une action:");
            Console.WriteLine("1. Attaquer");
            Console.WriteLine("2. Utiliser une potion");
            Console.WriteLine("3. Fuir");
            string choixJoueur = Console.ReadLine();

            switch (choixJoueur)
            {
                case "1":
                    ChoisirEtExecuterAttaque(pokemonJoueur, pokemonSauvage);
                    break;
                case "2":
                    // Simplifié pour l'exemple
                    Console.WriteLine("Vous utilisez une potion!");
                    break;
                case "3":
                    Console.WriteLine("Vous fuyez le combat.");
                    combatContinue = false;
                    break;
            }

            if (pokemonSauvage.PointsDeVie <= 0)
            {
                Console.WriteLine($"Vous avez vaincu {pokemonSauvage.Nom}!");
                joueurGagne = true;
                break;
            }

            // Tour de l'IA
            if (combatContinue)
            {
                Console.WriteLine($"Tour de {pokemonSauvage.Nom}...");
                IAChoisitAction(pokemonSauvage, pokemonJoueur, niveauPokemon);
                if (pokemonJoueur.PointsDeVie <= 0)
                {
                    Console.WriteLine("Votre Pokémon est K.O. !");
                    combatContinue = false;
                }
            }
        }

        return joueurGagne;
    }

    private static void ChoisirEtExecuterAttaque(Pokemon attaquant, Pokemon defenseur)
    {
        Console.WriteLine("Choisissez une attaque :");
        for (int i = 0; i < attaquant.Attaques.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {attaquant.Attaques[i].Nom} (Mana: {attaquant.Attaques[i].CoutMana})");
        }
        int choix = int.Parse(Console.ReadLine());
        Attaque attaqueChoisie = attaquant.Attaques[choix - 1];

        if (attaquant.PointsDeMana >= attaqueChoisie.CoutMana)
        {
            attaquant.PointsDeMana -= attaqueChoisie.CoutMana;
            defenseur.PointsDeVie -= attaqueChoisie.Puissance;
            Console.WriteLine($"{attaquant.Nom} utilise {attaqueChoisie.Nom} et inflige {attaqueChoisie.Puissance} de dégâts.");
        }
        else
        {
            Console.WriteLine("Pas assez de mana pour cette attaque!");
        }
    }

    private static void IAChoisitAction(Pokemon pokemonSauvage, Pokemon pokemonJoueur, int niveauPokemon)
    {
        // Simplifié pour l'exemple
        Random rnd = new Random();
        var attaque = pokemonSauvage.Attaques[rnd.Next(pokemonSauvage.Attaques.Count)];
        pokemonJoueur.PointsDeVie -= attaque.Puissance;
        Console.WriteLine($"{pokemonSauvage.Nom} utilise {attaque.Nom} et inflige {attaque.Puissance} de dégâts.");
    }

    private static void AfficherStats(string proprietaire, Pokemon pokemon)
    {
        Console.WriteLine($"{proprietaire} Pokémon {pokemon.Nom}: PV {pokemon.PointsDeVie}, Mana {pokemon.PointsDeMana}, Attaque {pokemon.Attaque}, Défense {pokemon.Defense}, Vitesse {pokemon.Vitesse}");
    }
}
