using NUnit.Framework.Constraints;
using System;
using System.Linq;

public static class LogiqueDeCombat
{
    public static bool ExecuterCombat(Joueur joueur, Pokemon pokemonJoueur, Pokemon pokemonSauvage, int niveauPokemon, CarteManager carteManager, int x, int y)
    {
        int pvInitiauxJoueur = pokemonJoueur.PointsDeVie;
        int pmInitiauxJoueur = pokemonJoueur.PointsDeMana;
        int pvInitiauxSauvage = pokemonSauvage.PointsDeVie;
        int pmInitiauxSauvage = pokemonSauvage.PointsDeMana;
        bool combatContinue = true;

        Console.WriteLine($"\nUn combat commence entre {pokemonJoueur.Nom} et {pokemonSauvage.Nom}!");

        while (combatContinue && joueur.CollectionPokemon.Any(p => p.PointsDeVie > 0))
        {
            AfficherStats("Votre Pokémon", pokemonJoueur);
            AfficherStats("Pokémon sauvage", pokemonSauvage);

            Console.WriteLine("\nC'est votre tour. Choisissez une action:");
            Console.WriteLine("1. Attaquer");
            Console.WriteLine("2. Sauter le tour");
            Console.WriteLine("3. Utiliser une potion");
            Console.WriteLine("4. Fuir");
            string choixJoueur = Console.ReadLine();

            switch (choixJoueur)
            {
                case "1":
                    ChoisirEtExecuterAttaque(pokemonJoueur, pokemonSauvage);
                    break;
                case "2":
                    Console.WriteLine("Vous décidez de sauter votre tour.");
                    break;
                case "3":
                    // Simplifié pour l'exemple
                    joueur.UtiliserPotion();
                    break;
                case "4":
                    Console.WriteLine("Vous fuyez le combat.");
                    combatContinue = false;
                    break;
            }

            if (pokemonSauvage.PointsDeVie <= 0)
            {
                //victoire
                pokemonJoueur.PointsDeVie = pvInitiauxJoueur;
                pokemonJoueur.PointsDeMana = pmInitiauxJoueur;
                pokemonSauvage.PointsDeVie = pvInitiauxSauvage;
                pokemonSauvage.PointsDeMana = pmInitiauxSauvage;
                ApresCombat(joueur);
                return true;
            }

            // Tour de l'IA
            Console.WriteLine($"\nTour de {pokemonSauvage.Nom}...");
            IAChoisitAction(pokemonSauvage, pokemonJoueur, niveauPokemon);

            

            if (pokemonJoueur.PointsDeVie <= 0)
            {
                pokemonJoueur.EstKO = true;
                pokemonJoueur.KoRestantMatchs = 5; // Le Pokémon est KO pendant 5 matchs
                Console.WriteLine("Votre Pokémon est K.O. !");
                if (!EncorePokemon(joueur))
                {
                    Console.WriteLine("\n\tTous vos Pokémon sont K.O. - Game Over");
                    Environment.Exit(0);
                    combatContinue = false;
                    break;
                }
                pokemonJoueur = joueur.ChoisirPokemonPourCombat(); // Choisir un nouveau Pokémon pour le combat
            }
        }
        return false;
    }


    private static bool EncorePokemon(Joueur joueur)
    {

            int i = 0;

            foreach (var pokemon in joueur.CollectionPokemon)
            {
                if (pokemon.EstKO)
                    i++;
            }
            if (i == joueur.CollectionPokemon.Count)
                return false;

            return true;
    }

    public static void ApresCombat(Joueur joueur)
    {
        foreach (var pokemon in joueur.CollectionPokemon)
        {
            if (pokemon.KoRestantMatchs > 0)
            {
                pokemon.KoRestantMatchs--;
                if (pokemon.KoRestantMatchs == 0)
                {
                    // Le Pokémon n'est plus considéré comme KO.
                    pokemon.EstKO = false;
                }
            }
        }
    }



    private static void ChoisirEtExecuterAttaque(Pokemon attaquant, Pokemon defenseur)
    {
        Console.WriteLine("Choisissez une action :");
        // Afficher les attaques disponibles
        for (int i = 0; i < attaquant.Attaques.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Attaque: {attaquant.Attaques[i].Nom} (Mana: {attaquant.Attaques[i].CoutMana}, Dégât: {attaquant.Attaques[i].Puissance})");
        }
        // Afficher les canalisations disponibles
        int canalisationStartIndex = attaquant.Attaques.Count + 1; // Début de l'indexation pour les canalisations
        for (int j = 0; j < attaquant.Canalisations.Count; j++)
        {
            Console.WriteLine($"{canalisationStartIndex + j}. Canalisation: {attaquant.Canalisations[j].Nom} (Gain Mana: {attaquant.Canalisations[j].GainMana}, Gain PV: {attaquant.Canalisations[j].GainPV})");
        }

        int choix = int.Parse(Console.ReadLine());
        // Exécution de l'attaque
        if (choix <= attaquant.Attaques.Count)
        {
            Attaque attaqueChoisie = attaquant.Attaques[choix - 1];
            if (attaquant.PointsDeMana >= attaqueChoisie.CoutMana)
            {
                attaquant.PointsDeMana -= attaqueChoisie.CoutMana;
                Random rnd = new Random();
                if (rnd.Next(100) < attaquant.Precision - defenseur.Vitesse) // Gestion de la précision et de l'esquive
                {
                    double degatsInfliges = CalculerDegats(attaquant, defenseur, attaqueChoisie);
                    int degatsArrondis = (int)Math.Round(degatsInfliges);
                    defenseur.PointsDeVie -= degatsArrondis;
                    Console.WriteLine($"{attaquant.Nom} utilise {attaqueChoisie.Nom} et inflige {degatsArrondis} de dégâts.");
                }
                else
                {
                    Console.WriteLine($"{attaquant.Nom} a manqué son attaque !");
                }
            }
            else
            {
                Console.WriteLine("Pas assez de mana pour cette attaque!");
            }
        }
        // Exécution de la canalisation
        else if (choix >= canalisationStartIndex && choix < canalisationStartIndex + attaquant.Canalisations.Count)
        {
            var canalisationChoisie = attaquant.Canalisations[choix - canalisationStartIndex];
            attaquant.PointsDeMana += canalisationChoisie.GainMana;
            attaquant.PointsDeVie = attaquant.PointsDeVie + canalisationChoisie.GainPV;
            Console.WriteLine($"{attaquant.Nom} utilise {canalisationChoisie.Nom} et récupère {canalisationChoisie.GainMana} points de mana et {canalisationChoisie.GainPV} points de vie.");
        }
        else
        {
            Console.WriteLine("Choix non valide. Veuillez choisir une option valide.");
        }
    }

    private static double CalculerDegats(Pokemon attaquant, Pokemon defenseur, Attaque attaque)
    {
        double multiplicateurType = ObtenirMultiplicateurType(attaquant.Type, defenseur.Type);
        double reductionDefense = (100 - defenseur.Defense) / 100.0;
        return attaque.Puissance * reductionDefense * multiplicateurType;
    }

    private static void IAChoisitAction(Pokemon pokemonSauvage, Pokemon pokemonJoueur, int niveauPokemon)
    {
        int niveauIA = (niveauPokemon >= 1 && niveauPokemon <= 3) ? 1 : (niveauPokemon <= 7) ? 2 : 3; // Niveau 3 n'est pas implémenté ici
        Random rnd = new Random();

        switch (niveauIA)
        {
            case 1:
                // IA Niveau 1 - Choix aléatoire simple entre attaque et canalisation
                if (pokemonSauvage.Canalisations.Count > 0 && rnd.Next(2) == 0) // 50% de chance d'utiliser une canalisation si disponible
                {
                    var canalisation = pokemonSauvage.Canalisations[rnd.Next(pokemonSauvage.Canalisations.Count)];
                    pokemonSauvage.PointsDeMana += canalisation.GainMana;
                    pokemonSauvage.PointsDeVie += canalisation.GainPV;
                    Console.WriteLine($"{pokemonSauvage.Nom} utilise {canalisation.Nom}, récupérant {canalisation.GainMana} PM et {canalisation.GainPV} PV.");
                }
                else
                {
                    var attaque = pokemonSauvage.Attaques[rnd.Next(pokemonSauvage.Attaques.Count)];
                    pokemonJoueur.PointsDeVie -= attaque.Puissance;
                    Console.WriteLine($"{pokemonSauvage.Nom} utilise {attaque.Nom} et inflige {attaque.Puissance} de dégâts.");
                }
                break;
            case 2:
                // IA Niveau 2 - Préfère attaquer si les PV du joueur sont bas ou si son mana est suffisant, sinon canalise
                bool devraitAttaquer = pokemonJoueur.PointsDeVie < 30 || pokemonSauvage.PointsDeMana > 20;
                if (devraitAttaquer)
                {
                    var attaque = pokemonSauvage.Attaques[rnd.Next(pokemonSauvage.Attaques.Count)];
                    if (pokemonSauvage.PointsDeMana >= attaque.CoutMana)
                    {
                        pokemonSauvage.PointsDeMana -= attaque.CoutMana;
                        pokemonJoueur.PointsDeVie -= attaque.Puissance;
                        Console.WriteLine($"{pokemonSauvage.Nom} utilise {attaque.Nom} et inflige {attaque.Puissance} de dégâts.");
                    }
                    else if (pokemonSauvage.Canalisations.Count > 0)
                    {
                        var canalisation = pokemonSauvage.Canalisations[rnd.Next(pokemonSauvage.Canalisations.Count)];
                        pokemonSauvage.PointsDeMana += canalisation.GainMana;
                        pokemonSauvage.PointsDeVie += canalisation.GainPV;
                        Console.WriteLine($"{pokemonSauvage.Nom} utilise {canalisation.Nom}, récupérant {canalisation.GainMana} PM et {canalisation.GainPV} PV.");
                    }
                }
                else if (pokemonSauvage.Canalisations.Count > 0)
                {
                    var canalisation = pokemonSauvage.Canalisations[rnd.Next(pokemonSauvage.Canalisations.Count)];
                    pokemonSauvage.PointsDeMana += canalisation.GainMana;
                    pokemonSauvage.PointsDeVie += canalisation.GainPV;
                    Console.WriteLine($"{pokemonSauvage.Nom} utilise {canalisation.Nom}, récupérant {canalisation.GainMana} PM et {canalisation.GainPV} PV.");
                }
                break;
            case 3:
                // IA Niveau 3 - Utilise une stratégie plus avancée
                if (pokemonSauvage.PointsDeVie < 20 && pokemonSauvage.Canalisations.Any(c => c.GainPV > 0))
                {
                    // Si les PV sont bas, prioriser la canalisation pour regagner des PV
                    var canalisation = pokemonSauvage.Canalisations.FirstOrDefault(c => c.GainPV > 0);
                    pokemonSauvage.PointsDeMana += canalisation.GainMana;
                    pokemonSauvage.PointsDeVie = pokemonSauvage.PointsDeVie + canalisation.GainPV;
                    Console.WriteLine($"{pokemonSauvage.Nom} utilise {canalisation.Nom}, récupérant {canalisation.GainMana} PM et {canalisation.GainPV} PV.");
                }
                else if (pokemonSauvage.PointsDeMana < 20 && pokemonSauvage.Canalisations.Any(c => c.GainMana > 0))
                {
                    // Si le mana est bas, prioriser la canalisation pour regagner du mana
                    var canalisation = pokemonSauvage.Canalisations.FirstOrDefault(c => c.GainMana > 0);
                    pokemonSauvage.PointsDeMana += canalisation.GainMana;
                    Console.WriteLine($"{pokemonSauvage.Nom} utilise {canalisation.Nom} pour récupérer {canalisation.GainMana} PM.");
                }
                else
                {
                    // Sinon, choisir une attaque basée sur le type et les PV du Pokémon joueur pour maximiser les dégâts
                    var attaqueChoisie = pokemonSauvage.Attaques.OrderBy(a => rnd.Next()).FirstOrDefault(); // Exemple simplifié, peut être amélioré
                    if (pokemonSauvage.PointsDeMana >= attaqueChoisie.CoutMana)
                    {
                        pokemonSauvage.PointsDeMana -= attaqueChoisie.CoutMana;
                        pokemonJoueur.PointsDeVie -= attaqueChoisie.Puissance;
                        Console.WriteLine($"{pokemonSauvage.Nom} utilise {attaqueChoisie.Nom} et inflige {attaqueChoisie.Puissance} de dégâts.");
                    }
                }
                break;

        }
    }


    private static void AfficherStats(string proprietaire, Pokemon pokemon)
    {
        Console.WriteLine($"{proprietaire} {pokemon.Nom}: PV {pokemon.PointsDeVie}, Mana {pokemon.PointsDeMana}");

    }

    private static double ObtenirMultiplicateurType(TypePokemon typeAttaquant, TypePokemon typeDefenseur)
    {
        // Feu
        if (typeAttaquant == TypePokemon.Feu)
        {
            if (typeDefenseur == TypePokemon.Plante ||
                typeDefenseur == TypePokemon.Glace)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Eau ||
                     typeDefenseur == TypePokemon.Feu ||
                     typeDefenseur == TypePokemon.Roche ||
                     typeDefenseur == TypePokemon.Vol)
            {
                return 0.5;
            }
        }
        // Eau
        else if (typeAttaquant == TypePokemon.Eau)
        {
            if (typeDefenseur == TypePokemon.Feu ||
                typeDefenseur == TypePokemon.Roche)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Eau ||
                     typeDefenseur == TypePokemon.Plante)
            {
                return 0.5;
            }
        }
        // Plante
        else if (typeAttaquant == TypePokemon.Plante)
        {
            if (typeDefenseur == TypePokemon.Eau ||
                typeDefenseur == TypePokemon.Roche)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Feu ||
                     typeDefenseur == TypePokemon.Plante ||
                     typeDefenseur == TypePokemon.Vol)
            {
                return 0.5;
            }
        }
        // Électrique
        else if (typeAttaquant == TypePokemon.Electrique)
        {
            if (typeDefenseur == TypePokemon.Eau ||
                typeDefenseur == TypePokemon.Vol)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Electrique ||
                     typeDefenseur == TypePokemon.Plante)
            {
                return 0.5;
            }
        }
        else if (typeAttaquant == TypePokemon.Glace)
        {
            if (typeDefenseur == TypePokemon.Plante ||
                typeDefenseur == TypePokemon.Vol)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Glace)
            {
                return 0.5;
            }
        }
        // Combat
        else if (typeAttaquant == TypePokemon.Combat)
        {
            if (typeDefenseur == TypePokemon.Normal ||
                typeDefenseur == TypePokemon.Glace ||
                typeDefenseur == TypePokemon.Roche)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Psy)
            {
                return 0.5;
            }
        }
        // Roche
        else if (typeAttaquant == TypePokemon.Roche)
        {
            if (typeDefenseur == TypePokemon.Feu ||
                typeDefenseur == TypePokemon.Glace ||
                typeDefenseur == TypePokemon.Vol)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Normal)
            {
                return 0.5;
            }
        }
        // Psy
        else if (typeAttaquant == TypePokemon.Psy)
        {
            if (typeDefenseur == TypePokemon.Combat)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Psy)
            {
                return 0.5;
            }
        }
        // Vol
        else if (typeAttaquant == TypePokemon.Vol)
        {
            if (typeDefenseur == TypePokemon.Plante ||
                typeDefenseur == TypePokemon.Combat)
            {
                return 1.5;
            }
            else if (typeDefenseur == TypePokemon.Electrique ||
                     typeDefenseur == TypePokemon.Roche)
            {
                return 0.5;
            }


        }
        return 1.0;

    }

}
