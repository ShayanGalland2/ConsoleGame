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


public class Canalisation
{
    public string Nom { get; set; }
    public int GainMana { get; set; }
    public int GainPV { get; set; }

    public Canalisation(string nom, int gainMana = 0, int gainPV = 0)
    {
        Nom = nom;
        GainMana = gainMana;
        GainPV = gainPV;
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

    public List<Canalisation> Canalisations { get; set; } = new List<Canalisation>();


    public int GainManaParCanalisation { get; set; } = 0;
    public int GainPVParCanalisation { get; set; } = 0;

    public int KoRestantMatchs { get; set; } = 0;
    public bool EstKO { get; set; } = false;

    public Pokemon()
    {
        Attaques = new List<Attaque>();
        
    }
}

