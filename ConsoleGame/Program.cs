using System;

class Program
{
    static Joueur joueur = new Joueur();

    static void Main(string[] args)
    {
        CarteManager.InitialiserPokemonSurCarte();

        // Menu 
        Menu();
    }



    static void Menu()
    {
        bool quitter = false;
        while (!quitter)
        {
            Console.Clear();
            Console.WriteLine("1. Jouer");
            Console.WriteLine("2. Équipe");
            Console.WriteLine("3. Quitter");
            Console.Write("Sélectionnez une option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Jouer();
                    break;
                case "2":
                    VoirEquipe();
                    break;
                case "3":
                    quitter = true;
                    break;
                default:
                    Console.WriteLine("Option invalide, essayez à nouveau.");
                    Console.ReadKey();
                    break;
            }
        }
    }



    static void Jouer()
    {
        CarteManager carteManager = new CarteManager("map.txt");
        carteManager.AfficherCarte();

        bool jeuEnCours = true;

        // GameLoop
        while (jeuEnCours)
        {
            // Controles
            if (Console.KeyAvailable)
            {
                var touche = Console.ReadKey(true);
                switch (touche.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        carteManager.DeplacerJoueur(touche, joueur);
                        carteManager.AfficherCarte();
                        break;
                    case ConsoleKey.M:
                        GameMenu.AfficherMenu(joueur); // Afficher le menu en jeu
                        carteManager.AfficherCarte(); // Réafficher la carte après la fermeture du menu
                        break;
                    case ConsoleKey.Escape:
                        jeuEnCours = false;
                        break;
                }
            }
        }
    }


    static void VoirEquipe()
    {
        joueur.AfficherCollection();
        Console.ReadKey();
    }
}
