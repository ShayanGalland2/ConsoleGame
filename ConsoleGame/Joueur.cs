using System;
using System.Collections.Generic;
using System.IO;

public class Joueur
{
    public List<Pokemon> CollectionPokemon { get; private set; } = new List<Pokemon>();

    public Joueur()
    {
        ChargerCollectionDepuisFichier("collect.txt");
        ChargerInventaire();
        ChargerExperience(); // Ajoutez cette ligne pour charger l'expérience au démarrage
    }

    private void ChargerInventaire()
    {
        string path = "playInv.txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    if (int.TryParse(parts[1], out int value))
                    {
                        switch (key)
                        {
                            case "pexp":
                                inventaire["potion d'experience"] = value;
                                break;
                            case "pniv":
                                inventaire["potion de niveau"] = value;
                                break;
                            case "pvie":
                                inventaire["potion de vie"] = value;
                                break;
                            case "prev":
                                inventaire["potion revitalisante"] = value;
                                break;
                            case "key":
                                inventaire["clé"] = value;
                                break;
                        }
                    }
                }
            }
        }
        else
        {

            ReinitialiserExperienceEtInventaire();
        }
    }


    public void UtiliserObjet(string objet)
    {
        if (inventaire.ContainsKey(objet) && inventaire[objet] > 0)
        {
            switch (objet)
            {
                case "potion d'experience":
                    AjouterExperience(50); // Ajoute 50 XP
                    Console.WriteLine("Vous avez utilisé une potion d'expérience. +50 XP!");
                    break;
                case "potion de niveau":
                    // Calcule l'XP nécessaire pour le prochain niveau et l'ajoute
                    int xpNecessaire = ExperienceNecessairePourNiveau(Niveau + 1) - Experience;
                    AjouterExperience(xpNecessaire);
                    Console.WriteLine("Vous avez utilisé une potion de niveau. Niveau augmenté!");
                    break;

            }
            inventaire[objet]--; // Décrémente le nombre d'objets après utilisation
        }
        else
        {
            Console.WriteLine($"Vous n'avez pas de {objet}.");
        }
    }

    public void VerifierEtAttribuerPotionsRevitalisantes()
    {
        if (Niveau % 5 == 0)
        {
            AjouterObjetInventaire("potion revitalisante");
            Console.WriteLine("Vous avez reçu une potion revitalisante pour avoir atteint un niveau multiple de 5!");
        }
    }

    public int Experience { get; private set; }
    public int Niveau { get; private set; } = 1;

    public void AjouterExperience(int xpGagnee)
    {
        Experience += xpGagnee;
        while (Experience >= ExperienceNecessairePourNiveau(Niveau))
        {
            Experience -= ExperienceNecessairePourNiveau(Niveau);
            Niveau++;
            if (Niveau > 50) // Limite du niveau à 50
            {
                Niveau = 50;
                Experience = 0; // Reset l'expérience si le niveau max est atteint
                break;
            }
        }
    }

    public int ExperienceNecessairePourNiveau(int niveauActuel)
    {
        return (int)(10 * Math.Pow(1.5, niveauActuel - 1));
    }

    private Dictionary<string, int> inventaire = new Dictionary<string, int>();

    public void AjouterObjetInventaire(string objet)
    {
        if (inventaire.ContainsKey(objet))
        {
            inventaire[objet]++;
        }
        else
        {
            inventaire.Add(objet, 1);
        }
    }

    public void AfficherInventaire()
    {
        Console.WriteLine("Inventaire :");
        foreach (var item in inventaire)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
    }


    public void UtiliserPotion()
    {
        Console.WriteLine("Choisissez une potion à utiliser :");
        if (inventaire.ContainsKey("potion de vie") && inventaire["potion de vie"] > 0)
        {
            Console.WriteLine("1. Potion de vie (+50 HP)");
        }
        if (inventaire.ContainsKey("potion revitalisante") && inventaire["potion revitalisante"] > 0)
        {
            Console.WriteLine("2. Potion revitalisante (faire revivre un Pokémon KO)");
        }
        string choixPotion = Console.ReadLine();
        switch (choixPotion)
        {
            case "1":
                if (inventaire["potion de vie"] > 0)
                {
                    Console.WriteLine("Choisissez un Pokémon pour lui redonner 50 HP.");
                    var pokemonChoisi = ChoisirPokemonPourCombat();
                    pokemonChoisi.PointsDeVie += 50;
                    inventaire["potion de vie"]--;
                    Console.WriteLine($"{pokemonChoisi.Nom} a maintenant {pokemonChoisi.PointsDeVie} HP.");
                }
                break;
            case "2":
                if (inventaire["potion revitalisante"] > 0)
                {
                    Console.WriteLine("Choisissez un Pokémon KO pour le réanimer.");
                    var pokemonChoisi = ChoisirPokemonKO();
                    if (pokemonChoisi != null)
                    {
                        pokemonChoisi.EstKO = false;
                        pokemonChoisi.KoRestantMatchs = 0;

                        inventaire["potion revitalisante"]--;
                        Console.WriteLine($"{pokemonChoisi.Nom} a été réanimé et peut désormais combattre.");
                    }
                    else
                    {
                        Console.WriteLine("Aucun Pokémon KO n'a été sélectionné.");
                    }
                }
                break;

            default:
                Console.WriteLine("Choix invalide.");
                break;
        }
    }

    public bool TousPokemonsKO()
    {
        return CollectionPokemon.All(pokemon => pokemon.EstKO);
    }



    public Pokemon ChoisirPokemonKO()
    {
        var pokemonsKO = CollectionPokemon.Where(p => p.EstKO).ToList();

        if (pokemonsKO.Count == 0)
        {
            Console.WriteLine("Vous n'avez aucun Pokémon KO.");
            return null;
        }

        Console.WriteLine("Choisissez un Pokémon KO à réanimer :");
        for (int i = 0; i < pokemonsKO.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pokemonsKO[i].Nom}");
        }

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= pokemonsKO.Count)
            {
                return pokemonsKO[choix - 1];
            }
            else
            {
                Console.WriteLine("Choix invalide. Veuillez choisir un numéro valide.");
            }
        }
    }


    private void ChargerExperience()
    {
        string contenu = File.ReadAllText("playXp.txt");
        string prefixeXp = "xp=";
        int indexXp = contenu.IndexOf(prefixeXp);
        if (indexXp != -1)
        {
            string xpStr = contenu.Substring(indexXp + prefixeXp.Length);
            if (int.TryParse(xpStr, out int xp))
            {
                this.Experience = xp;
                while (Experience >= ExperienceNecessairePourNiveau(Niveau) && Niveau < 50)
                {
                    Experience -= ExperienceNecessairePourNiveau(Niveau++);
                }
            }
        }
    }

    public void SauvegarderExperience()
    {

        File.WriteAllText("playXp.txt", $"xp={Experience}");


    }

    public void SauvegarderInventaire()
    {
        using (StreamWriter sw = new StreamWriter("playInv.txt"))
        {
            if (inventaire.ContainsKey("potion d'experience"))
                sw.WriteLine($"pexp={inventaire["potion d'experience"]}");
            else
                sw.WriteLine("pexp=0");

            if (inventaire.ContainsKey("potion de niveau"))
                sw.WriteLine($"pniv={inventaire["potion de niveau"]}");
            else
                sw.WriteLine("pniv=0");

            if (inventaire.ContainsKey("potion de vie"))
                sw.WriteLine($"pvie={inventaire["potion de vie"]}");
            else
                sw.WriteLine("pvie=0");

            if (inventaire.ContainsKey("potion revitalisante"))
                sw.WriteLine($"prev={inventaire["potion revitalisante"]}");
            else
                sw.WriteLine("prev=0");

            if (inventaire.ContainsKey("clé"))
                sw.WriteLine($"key={inventaire["clé"]}");
            else
                sw.WriteLine("key=0");

        }
    }

    public bool PeutEchangerPotions()
    {
        return inventaire.ContainsKey("potion d'experience") && inventaire["potion d'experience"] > 0 &&
               inventaire.ContainsKey("potion de vie") && inventaire["potion de vie"] > 0 &&
               inventaire.ContainsKey("potion de niveau") && inventaire["potion de niveau"] > 0;
    }

    public void EchangerPotionsPourRevitalisante()
    {
        inventaire["potion d'experience"]--;
        inventaire["potion de vie"]--;
        inventaire["potion de niveau"]--;
        if (!inventaire.ContainsKey("potion revitalisante"))
        {
            inventaire["potion revitalisante"] = 1;
        }
        else
        {
            inventaire["potion revitalisante"]++;
        }
    }


    public void ReinitialiserExperienceEtInventaire()
    {
        inventaire.Clear(); 
        //Réinitialise l'inventaire
        inventaire.Add("pexp", 0);
        inventaire.Add("pniv", 0);
        inventaire.Add("pvie", 0);
        inventaire.Add("prev", 0);
        inventaire.Add("key", 0);
        Experience = 0;
        Niveau = 1;
        SauvegarderExperience(); // Sauvegarde l'expérience réinitialisée
    }


    private void ChargerCollectionDepuisFichier(string cheminFichier)
    {
        try
        {
            using (StreamReader sr = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = sr.ReadLine()) != null)
                {
                    var elements = ligne.Split(' ');
                    var nom = elements[0];
                    var niveau = int.Parse(elements[1]);
                    var pokemon = PokemonFactory.CreerPokemon(nom, niveau);
                    CollectionPokemon.Add(pokemon);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Erreur lors du chargement de la collection de Pokémon : " + e.Message);
        }
    }

    public void AfficherCollection()
    {
        Console.WriteLine("Votre collection de Pokémon :");

        if (CollectionPokemon.Count == 0)
        {
            Console.WriteLine("Vous n'avez aucun Pokémon dans votre collection.");
            return;
        }

        foreach (var pokemon in CollectionPokemon)
        {
            Console.WriteLine($"Nom: {pokemon.Nom}");
            Console.WriteLine($"Niveau: {pokemon.Niveau}");
            Console.WriteLine($"Attaque: {pokemon.Attaque}");
            Console.WriteLine($"Défense: {pokemon.Defense}");
            Console.WriteLine($"Vitesse: {pokemon.Vitesse}");
            Console.WriteLine($"Points de Vie: {pokemon.PointsDeVie}");
            Console.WriteLine($"Points de Mana: {pokemon.PointsDeMana}");
            Console.WriteLine($"Précision: {pokemon.Precision}");
            Console.WriteLine("Attaques:");
            foreach (var attaque in pokemon.Attaques)
            {
                Console.WriteLine($"- {attaque.Nom} (Puissance: {attaque.Puissance}, Coût Mana: {attaque.CoutMana})");
            }
            Console.WriteLine();
        }
    }


    public Pokemon ChoisirPokemonPourCombat()
    {
        Console.WriteLine("Choisissez un Pokémon pour le combat :");
        var pokemonValides = CollectionPokemon
            .Select((p, index) => $"{index + 1}. {p.Nom} - {(p.KoRestantMatchs > 0 ? $"KO pour {p.KoRestantMatchs} matchs" : "Disponible")}")
            .ToList();

        foreach (var pokemon in pokemonValides)
        {
            Console.WriteLine(pokemon);
        }

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choix) && choix > 0 && choix <= CollectionPokemon.Count)
            {
                var pokemonChoisi = CollectionPokemon[choix - 1];
                if (pokemonChoisi.KoRestantMatchs > 0)
                {
                    Console.WriteLine("Ce Pokémon est KO. Veuillez en choisir un autre.");
                    continue;
                }
                return pokemonChoisi;
            }
            else
            {
                Console.WriteLine("Sélection invalide. Veuillez réessayer.");
            }
        }
    }



    public void SauvegarderCollection()
    {
        using (StreamWriter sw = new StreamWriter("collect.txt"))
        {
            foreach (var pokemon in CollectionPokemon)
            {
                sw.WriteLine($"{pokemon.Nom} {pokemon.Niveau}");
            }
        }
    }

    public void ReinitialiserCollection()
    {
        File.Copy("baseCollect.txt", "collect.txt", true); // True pour écraser le fichier s'il existe déjà

        CollectionPokemon.Clear(); // Efface la collection actuelle
        ChargerCollectionDepuisFichier("baseCollect.txt"); // Charge la collection de base
    }
    
}
