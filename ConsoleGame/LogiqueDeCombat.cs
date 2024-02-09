using System;

public static class LogiqueDeCombat
{
    public static bool ExecuterCombat(Joueur joueur, Pokemon pokemonSauvage, int niveauPokemon)
    {
        // Logique du combat
        Console.WriteLine("Combat en cours...");

        // Simuler un combat simplifié pour l'exemple
        Random rnd = new Random();
        int resultat = rnd.Next(2); // 0 pour défaite, 1 pour victoire

        if (resultat == 1)
        {
            Console.WriteLine("Vous avez gagné le combat !");
            return true;
        }
        else
        {
            Console.WriteLine("Vous avez perdu le combat...");
            return false;
        }
    }
}
