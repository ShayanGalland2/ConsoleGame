public static class PokemonFactory
{


    public static Pokemon CreerPokemon(string nom, int niveau)
    {
        Pokemon pokemon = new Pokemon { Nom = nom, Niveau = niveau };

        switch (nom.ToLower())
        {
            case "pikachu":
                pokemon.Attaque = 55 + niveau * 2;
                pokemon.Defense = 40 + niveau * 2;
                pokemon.Vitesse = 90 + niveau * 2;
                pokemon.PointsDeVie = 35 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 10;
                pokemon.Attaques.Add(new Attaque("Eclair", 40, 20));
                break;
            case "bulbasaur":
                pokemon.Attaque = 49 + niveau * 2;
                pokemon.Defense = 49 + niveau * 2;
                pokemon.Vitesse = 45 + niveau * 2;
                pokemon.PointsDeVie = 45 + niveau * 3;
                pokemon.PointsDeMana = 65;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 20;
                pokemon.Attaques.Add(new Attaque("Fouet Lianes", 45, 25));
                break;
            case "charmander":
                pokemon.Attaque = 52 + niveau * 2;
                pokemon.Defense = 43 + niveau * 2;
                pokemon.Vitesse = 65 + niveau * 2;
                pokemon.PointsDeVie = 39 + niveau * 3;
                pokemon.PointsDeMana = 60;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 15;
                pokemon.Attaques.Add(new Attaque("Flammeche", 40, 20));
                pokemon.Attaques.Add(new Attaque("Griffe", 35, 15));
                break;
            case "squirtle":
                pokemon.Attaque = 48 + niveau * 2;
                pokemon.Defense = 65 + niveau * 2;
                pokemon.Vitesse = 43 + niveau * 2;
                pokemon.PointsDeVie = 44 + niveau * 3;
                pokemon.PointsDeMana = 55;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 18;
                pokemon.Attaques.Add(new Attaque("Pistolet à O", 40, 20));
                pokemon.Attaques.Add(new Attaque("Charge", 30, 10));
                break;
            case "jigglypuff":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 20 + niveau * 2;
                pokemon.Vitesse = 20 + niveau * 2;
                pokemon.PointsDeVie = 115 + niveau * 3;
                pokemon.PointsDeMana = 85;
                pokemon.Precision = 70;
                pokemon.ChanceApparition = 13;
                pokemon.Attaques.Add(new Attaque("Chant Canon", 50, 25));
                pokemon.Attaques.Add(new Attaque("Roulade", 40, 20));
                break;

            case "meowth":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 35 + niveau * 2;
                pokemon.Vitesse = 90 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 40;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 12;
                pokemon.Attaques.Add(new Attaque("Griffe", 40, 20));
                break;
            case "psyduck":
                pokemon.Attaque = 52 + niveau * 2;
                pokemon.Defense = 48 + niveau * 2;
                pokemon.Vitesse = 55 + niveau * 2;
                pokemon.PointsDeVie = 50 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 16;
                pokemon.Attaques.Add(new Attaque("Hydrocanon", 60, 30));
                break;
            case "abra":
                pokemon.Attaque = 20 + niveau * 2;
                pokemon.Defense = 15 + niveau * 2;
                pokemon.Vitesse = 90 + niveau * 2;
                pokemon.PointsDeVie = 25 + niveau * 3;
                pokemon.PointsDeMana = 90;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 10;
                pokemon.Attaques.Add(new Attaque("Teleport", 0, 15));
                break;
            case "machop":
                pokemon.Attaque = 80 + niveau * 2;
                pokemon.Defense = 50 + niveau * 2;
                pokemon.Vitesse = 35 + niveau * 2;
                pokemon.PointsDeVie = 70 + niveau * 3;
                pokemon.PointsDeMana = 30;
                pokemon.Precision = 80;
                pokemon.ChanceApparition = 14;
                pokemon.Attaques.Add(new Attaque("Poing Karate", 50, 25));
                break;
            case "geodude":
                pokemon.Attaque = 80 + niveau * 2;
                pokemon.Defense = 100 + niveau * 2;
                pokemon.Vitesse = 20 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 30;
                pokemon.Precision = 80;
                pokemon.ChanceApparition = 11;
                pokemon.Attaques.Add(new Attaque("Roulade", 40, 20));
                break;
            case "pidgey":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 40 + niveau * 2;
                pokemon.Vitesse = 56 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 17;
                pokemon.Attaques.Add(new Attaque("Tornade", 40, 20));
                break;
            case "rattata":
                pokemon.Attaque = 56 + niveau * 2;
                pokemon.Defense = 35 + niveau * 2;
                pokemon.Vitesse = 72 + niveau * 2;
                pokemon.PointsDeVie = 30 + niveau * 3;
                pokemon.PointsDeMana = 40;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 19;
                pokemon.Attaques.Add(new Attaque("Morsure", 60, 30));
                break;
            case "charizard":
                pokemon.Attaque = 84 + niveau * 3;
                pokemon.Defense = 78 + niveau * 3;
                pokemon.Vitesse = 100 + niveau * 3;
                pokemon.PointsDeVie = 78 + niveau * 4;
                pokemon.PointsDeMana = 78 + niveau * 2;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 5;
                pokemon.Attaques.Add(new Attaque("Lance-Flammes", 90, 35));
                pokemon.Attaques.Add(new Attaque("Dracogriffe", 80, 30));
                pokemon.Attaques.Add(new Attaque("Colère", 120, 45));
                pokemon.Attaques.Add(new Attaque("Déflagration", 110, 40));
                break;


            default:
                throw new ArgumentException("Nom de Pokémon non reconnu.");
        }

        // Ajustement des stats basé sur le niveau
        AjusterStatsParNiveau(pokemon, niveau);

        return pokemon;
    }



    private static void AjusterStatsParNiveau(Pokemon pokemon, int niveau)
    {
        // Ici, vous pouvez ajuster encore plus les stats si nécessaire
        // Exemple: pokemon.Attaque *= 1 + (niveau * 0.1);
    }

    public static Pokemon AttribuerPokemonAleatoire(int niveau)
    {
        var pokemons = new List<(string nom, double chanceApparition)>
    {
        ("Pikachu", 0.2), // 20% chance
        ("Bulbasaur", 0.3), // 30% chance
        // Ajouter d'autres Pokémon ici
    };

        // Ajuster les chances d'apparition basées sur le niveau
        var totalChance = pokemons.Sum(p => p.chanceApparition);
        var ajustedPokemons = pokemons.Select(p =>
        {
            var ajustedChance = p.chanceApparition - ((p.chanceApparition / totalChance) * (niveau / 100.0)); // Exemple d'ajustement
            return (p.nom, ajustedChance: Math.Max(ajustedChance, 0.01)); // Assure un minimum de chance d'apparition
        }).ToList();

        double valeurAleatoire = new Random().NextDouble();
        double sommeCumulative = 0.0;
        foreach (var pokemon in ajustedPokemons)
        {
            sommeCumulative += pokemon.ajustedChance;
            if (valeurAleatoire <= sommeCumulative)
            {
                return CreerPokemon(pokemon.nom, niveau);
            }
        }

        // Retourne un Pokémon par défaut si aucun n'a été sélectionné
        return CreerPokemon(ajustedPokemons.First().nom, niveau);
    }


}
