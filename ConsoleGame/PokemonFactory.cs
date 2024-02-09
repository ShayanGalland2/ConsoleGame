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
                pokemon.Vitesse = 10 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 0.2;
                pokemon.Attaques.Add(new Attaque("Eclair", 20, 20));
                pokemon.Attaques.Add(new Attaque("Queue de Fer", 35, 25));
                pokemon.Canalisations.Add(new Canalisation("Recharge", gainMana: 30));
                pokemon.Type = TypePokemon.Electrique;
                break;
            case "bulbasaur":
                pokemon.Attaque = 49 + niveau * 2;
                pokemon.Defense = 49 + niveau * 2;
                pokemon.Vitesse = 5 + niveau * 2;
                pokemon.PointsDeVie = 60 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 0.3;
                pokemon.Attaques.Add(new Attaque("Fouet Lianes", 45, 30));
                pokemon.Attaques.Add(new Attaque("Tranch'Herbe", 30, 20));
                pokemon.Canalisations.Add(new Canalisation("Synthèse", gainPV: 20));
                pokemon.Type = TypePokemon.Plante;
                break;
            case "charmander":
                pokemon.Attaque = 52 + niveau * 2;
                pokemon.Defense = 43 + niveau * 2;
                pokemon.Vitesse = 15 + niveau * 2;
                pokemon.PointsDeVie = 39 + niveau * 3;
                pokemon.PointsDeMana = 40;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 15;
                pokemon.Attaques.Add(new Attaque("Flammeche", 40, 20));
                pokemon.Attaques.Add(new Attaque("Griffe", 35, 15));
                pokemon.Canalisations.Add(new Canalisation("Echauffement", gainMana: 50));
                pokemon.Type = TypePokemon.Feu;
                break;
            case "squirtle":
                pokemon.Attaque = 48 + niveau * 2;
                pokemon.Defense = 65 + niveau * 2;
                pokemon.Vitesse = 15 + niveau * 2;
                pokemon.PointsDeVie = 44 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 18;
                pokemon.Attaques.Add(new Attaque("Pistolet à O", 40, 20));
                pokemon.Attaques.Add(new Attaque("Charge", 30, 10));
                pokemon.Canalisations.Add(new Canalisation("Echauffement", gainPV: 30));
                pokemon.Type = TypePokemon.Eau;
                break;
            case "jigglypuff":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 20 + niveau * 2;
                pokemon.Vitesse = 20 + niveau * 2;
                pokemon.PointsDeVie = 115 + niveau * 3;
                pokemon.PointsDeMana = 70;
                pokemon.Precision = 70;
                pokemon.ChanceApparition = 13;
                pokemon.Attaques.Add(new Attaque("Chant Canon", 50, 25));
                pokemon.Attaques.Add(new Attaque("Roulade", 40, 20));
                pokemon.Type = TypePokemon.Normal;
                break;
            case "meowth":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 35 + niveau * 2;
                pokemon.Vitesse = 15 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 70;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 12;
                pokemon.Attaques.Add(new Attaque("Griffe", 70, 60));
                pokemon.Canalisations.Add(new Canalisation("Echauffement", gainMana: 60));

                pokemon.Type = TypePokemon.Normal;
                break;
            case "psyduck":
                pokemon.Attaque = 52 + niveau * 2;
                pokemon.Defense = 48 + niveau * 2;
                pokemon.Vitesse = 20 + niveau * 2;
                pokemon.PointsDeVie = 50 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 85;
                pokemon.ChanceApparition = 16;
                pokemon.Attaques.Add(new Attaque("Hydrocanon", 60, 30));
                pokemon.Attaques.Add(new Attaque("Jet", 30, 10));

                pokemon.Type = TypePokemon.Eau;
                break;
            case "abra":
                pokemon.Attaque = 20 + niveau * 2;
                pokemon.Defense = 15 + niveau * 2;
                pokemon.Vitesse = 10 + niveau * 2;
                pokemon.PointsDeVie = 25 + niveau * 3;
                pokemon.PointsDeMana = 90;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 10;
                pokemon.Attaques.Add(new Attaque("Teleport", 20, 15));
                pokemon.Type = TypePokemon.Psy;
                break;
            case "machop":
                pokemon.Attaque = 80 + niveau * 2;
                pokemon.Defense = 50 + niveau * 2;
                pokemon.Vitesse = 20 + niveau * 2;
                pokemon.PointsDeVie = 70 + niveau * 3;
                pokemon.PointsDeMana = 30;
                pokemon.Precision = 80;
                pokemon.ChanceApparition = 14;
                pokemon.Attaques.Add(new Attaque("Poing Karate", 50, 25));
                pokemon.Canalisations.Add(new Canalisation("Prière", gainMana: 60));
                pokemon.Canalisations.Add(new Canalisation("Concentration", gainPV: 30));
                pokemon.Type = TypePokemon.Combat;
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
                pokemon.Type = TypePokemon.Roche;
                break;
            case "pidgey":
                pokemon.Attaque = 45 + niveau * 2;
                pokemon.Defense = 40 + niveau * 2;
                pokemon.Vitesse = 30 + niveau * 2;
                pokemon.PointsDeVie = 40 + niveau * 3;
                pokemon.PointsDeMana = 50;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 17;
                pokemon.Attaques.Add(new Attaque("Tornade", 40, 30));
                pokemon.Attaques.Add(new Attaque("Tempête", 30, 20));
                pokemon.Attaques.Add(new Attaque("Vent", 20, 10));
                pokemon.Canalisations.Add(new Canalisation("Soins intensifs", gainPV: 10));
                pokemon.Type = TypePokemon.Vol;
                break;
            case "rattata":
                pokemon.Attaque = 56 + niveau * 2;
                pokemon.Defense = 35 + niveau * 2;
                pokemon.Vitesse = 10 + niveau * 2;
                pokemon.PointsDeVie = 20 + niveau * 3;
                pokemon.PointsDeMana = 40;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 19;
                pokemon.Attaques.Add(new Attaque("Morsure", 60, 40));
                pokemon.Type = TypePokemon.Normal;
                break;
            case "charizard":
                pokemon.Attaque = 84 + niveau * 3;
                pokemon.Defense = 78 + niveau * 3;
                pokemon.Vitesse = 60 + niveau * 3;
                pokemon.PointsDeVie = 78 + niveau * 4;
                pokemon.PointsDeMana = 250 + niveau * 2;
                pokemon.Precision = 90;
                pokemon.ChanceApparition = 5;
                pokemon.Attaques.Add(new Attaque("Lance-Flammes", 90, 35));
                pokemon.Attaques.Add(new Attaque("Dracogriffe", 80, 30));
                pokemon.Attaques.Add(new Attaque("Colère", 120, 45));
                pokemon.Attaques.Add(new Attaque("Déflagration", 110, 40));
                pokemon.Canalisations.Add(new Canalisation("Cotérisation", gainPV: 40));
                pokemon.Type = TypePokemon.Feu;
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
        // vide
    }

    public static Pokemon AttribuerPokemonAleatoire(int niveau)
    {
        var pokemons = new List<(string nom, double chanceApparition)>
    {
            ("pikachu", 0.2), // 20% chance d'apparition
            ("bulbasaur", 0.3), // 30% chance d'apparition
            ("charmander", 0.15), // 15% chance d'apparition
            ("squirtle", 0.18), // 18% chance d'apparition
            ("jigglypuff", 0.13), // 13% chance d'apparition
            ("meowth", 0.12), // 12% chance d'apparition
            ("psyduck", 0.16), // 16% chance d'apparition
            ("abra", 0.1), // 10% chance d'apparition
            ("machop", 0.14), // 14% chance d'apparition
            ("geodude", 0.11), // 11% chance d'apparition
            ("pidgey", 0.17), // 17% chance d'apparition
            ("rattata", 0.19), // 19% chance d'apparition
            ("charizard", 0.05) // 5% chance d'apparition
    };

        var totalChance = pokemons.Sum(p => p.chanceApparition);
        var ajustedPokemons = pokemons.Select(p =>
        {
            var ajustedChance = p.chanceApparition - ((p.chanceApparition / totalChance) * (niveau / 100.0));
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

        // Pokemon par default
        return CreerPokemon(ajustedPokemons.First().nom, niveau);
    }


}
