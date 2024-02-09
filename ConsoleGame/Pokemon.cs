public enum TypePokemon
{
    Normal,
    Feu,
    Eau,
    Plante,
    Electrique,
    Glace,
    Combat,
    Roche,
    Psy,
    Vol
}

public class Attaque
{
    public string Nom { get; set; }
    public int Puissance { get; set; }
    public int CoutMana { get; set; }
    public Attaque(string nom, int puissance, int coutMana)
    {
        Nom = nom;
        Puissance = puissance;
        CoutMana = coutMana;
    }
}

public class Pokemon
{
    public TypePokemon Type { get; set; }

    public string Nom { get; set; }
    public int Niveau { get; set; }
    public int Attaque { get; set; }
    public int Defense { get; set; }
    public int Vitesse { get; set; }
    public int PointsDeVie { get; set; }
    public int PointsDeMana { get; set; }
    public int Precision { get; set; }
    public double ChanceApparition { get; set; }
    public List<Attaque> Attaques { get; set; }

    public Pokemon()
    {
        Attaques = new List<Attaque>();
    }
}
