using System;

public class GameMenu
{
    public static void AfficherMenu(Joueur joueur)
    {
        bool quitterMenu = false;
        while (!quitterMenu)
        {
            Console.Clear();
            Console.WriteLine("1. Reprendre");
            Console.WriteLine("2. Sauvegarder");
            Console.WriteLine("3. Équipe");
            Console.WriteLine("4. Quitter");
            Console.Write("Sélectionnez une option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    quitterMenu = true; // Reprendre le jeu
                    break;
                case "2":
                    Console.WriteLine("Sauvegarde en cours...");
                    // Implémenter la sauvegarde ici
                    Console.ReadKey();
                    break;
                case "3":
                    joueur.AfficherCollection();
                    Console.ReadKey();
                    break;
                case "4":
                    Environment.Exit(0); // Quitter le jeu
                    break;
                default:
                    Console.WriteLine("Option invalide, essayez à nouveau.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
