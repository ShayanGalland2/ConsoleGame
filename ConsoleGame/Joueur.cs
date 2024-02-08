using System;
using System.Collections.Generic;
using System.IO;

public class Joueur
{
    public List<Pokemon> CollectionPokemon { get; private set; } = new List<Pokemon>();

    public Joueur()
    {
        ChargerCollectionDepuisFichier("collect.txt");
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
        foreach (var pokemon in CollectionPokemon)
        {
            Console.WriteLine($"{pokemon.Nom} - Niveau {pokemon.Niveau}");
        }
    }

    public Pokemon ChoisirPokemonPourCombat()
    {
        AfficherCollection();
        Console.WriteLine("Choisissez un Pokémon pour le combat (entrez le numéro) :");
        int index = int.Parse(Console.ReadLine()) - 1;
        return CollectionPokemon[index];
    }
}
