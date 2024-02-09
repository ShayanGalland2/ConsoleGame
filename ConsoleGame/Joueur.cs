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
                        // Convertit les clés en noms complets d'objets pour la cohérence avec le reste du code
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
                                // Ajoutez d'autres cas si nécessaire
                        }
                    }
                }
            }
        }
        else
        {
            // Initialiser l'inventaire avec des valeurs par défaut si le fichier n'existe pas
            ReinitialiserExperienceEtInventaire(); // Vous pouvez choisir d'appeler cette méthode ou d'initialiser l'inventaire différemment ici.
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
                    // Gérez les autres cas d'utilisation ici
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


    // Lire l'expérience et le niveau du joueur depuis playInv.txt
    private void ChargerExperience()
    {
        string contenu = File.ReadAllText("playInv.txt");
        string prefixeXp = "xp=";
        int indexXp = contenu.IndexOf(prefixeXp);
        if (indexXp != -1)
        {
            string xpStr = contenu.Substring(indexXp + prefixeXp.Length);
            if (int.TryParse(xpStr, out int xp))
            {
                this.Experience = xp;
                // Mettre à jour le niveau en fonction de l'expérience
                while (Experience >= ExperienceNecessairePourNiveau(Niveau) && Niveau < 50)
                {
                    Experience -= ExperienceNecessairePourNiveau(Niveau++);
                }
            }
        }
    }

    // Sauvegarder l'expérience et le niveau du joueur dans playInv.txt lors de la sauvegarde
    public void SauvegarderExperience()
    {

        File.WriteAllText("playInv.txt", $"xp={Experience}");


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

            // Ajoutez ici d'autres lignes pour sauvegarder d'autres éléments de l'inventaire si nécessaire.
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
        inventaire.Clear(); // Réinitialise l'inventaire
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
        for (int i = 0; i < CollectionPokemon.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {CollectionPokemon[i].Nom}");
        }

        do
        {
            
            Console.WriteLine("\nChoisissez un Pokémon pour le combat (entrez le numéro) :");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                int index = input - 1;
                if (index >= 0 && index < CollectionPokemon.Count)
                {
                    return CollectionPokemon[index];
                }
            }
            Console.WriteLine("Erreur : Veuillez entrer un numéro valide.");
        } while (true);
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
