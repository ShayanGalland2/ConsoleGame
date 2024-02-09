using System;

public class GameMenu
{
    public static void AfficherMenu(Joueur joueur, CarteManager carteManager)
    {
        bool quitterMenu = false;
        while (!quitterMenu)
        {
            Console.Clear();
            Console.WriteLine("1. Reprendre");
            Console.WriteLine("2. Sauvegarder");
            Console.WriteLine("3. Équipe");
            Console.WriteLine("4. Inventaire");
            Console.WriteLine("5. Quitter");
            Console.Write("Sélectionnez une option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    quitterMenu = true; // Reprendre le jeu
                    break;
                case "2": // Option de sauvegarde
                    joueur.SauvegarderCollection();
                    carteManager.SauvegarderCarte();
                    joueur.SauvegarderExperience(); // Ajout pour sauvegarder l'expérience
                    joueur.SauvegarderInventaire();
                    Console.WriteLine("Partie sauvegardée.");
                    Console.ReadKey();
                    break;

                case "3":
                    joueur.AfficherCollection();
                    Console.ReadKey();
                    break;
                case "4":
                    Console.WriteLine($"Niveau : {joueur.Niveau}, Expérience : {joueur.Experience}/{joueur.ExperienceNecessairePourNiveau(joueur.Niveau)}");

                    joueur.VerifierEtAttribuerPotionsRevitalisantes();
                    joueur.AfficherInventaire();
                    string choixObjet = Console.ReadLine(); // Lisez le choix de l'utilisateur
                    joueur.UtiliserObjet(choixObjet); // Utilise l'objet sélectionné

                    Console.ReadKey();
                    break;
                case "5":
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
