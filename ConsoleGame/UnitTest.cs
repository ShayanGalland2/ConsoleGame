global using NUnit.Framework;
using FluentAssertions;
using System;
using System.IO;
using System.Collections.Generic;

namespace Game.Tests
{
    public class CarteManagerTests
    {
        private CarteManager _carteManager;

        [SetUp]
        public void Setup()
        {
            _carteManager = new CarteManager("map.txt");
        }

        [Test]
        public void ChargerCarteDepuisFichier_CarteValide_RetourneCarte()
        {
            var carte = _carteManager.ChargerCarteDepuisFichier("map.txt");

            carte.Should().NotBeNull();
            carte.Length.Should().BeGreaterThan(0);
        }
    }

    public class JoueurTests
    {
        private Joueur _joueur;

        [SetUp]
        public void Setup()
        {
            _joueur = new Joueur();

        }

        [Test]
        public void AjouterExperience_ExperienceAjoutee_ExperienceEtNiveauMiseAJour()
        {
            int experienceInitiale = _joueur.Experience;
            int niveauInitial = _joueur.Niveau;

            _joueur.AjouterExperience(100);

            _joueur.Experience.Should().BeGreaterThan(experienceInitiale);
            _joueur.Niveau.Should().BeGreaterThanOrEqualTo(niveauInitial);
        }

    }

    public class PokemonFactoryTests
    {
        [Test]
        [TestCase("pikachu", 5)]
        [TestCase("charmander", 1)]
        public void CreerPokemon_AvecNomEtNiveauValides_RetournePokemon(string nom, int niveau)
        {
            var pokemon = PokemonFactory.CreerPokemon(nom, niveau);

            pokemon.Should().NotBeNull();
            pokemon.Nom.Should().Be(nom);
            pokemon.Niveau.Should().Be(niveau);
            pokemon.Attaques.Should().NotBeEmpty();
        }

    }


    // Tests pour les interactions avec les PNJ
    public class PNJTests
    {
        [Test]
        public void InteragirAvecPNJ_DonneCharizard_ajouteCharizardAuJoueur()
        {
            var joueur = new Joueur();
            var carteManager = new CarteManager("map.txt");

            // Simuler l'interaction avec Maxime
            carteManager.InteragirAvecPNJ('M', joueur);

            joueur.CollectionPokemon.Should().Contain(p => p.Nom == "charizard" && p.Niveau == 5);
        }
    }


}
