using System;
using DemoDAO;

namespace DemoConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
	        // TODO changer par Csv Impl si besoin
	        // oubliez pas de remettre SQL impl pour le tp noté
	        IPersonneDao personnes = new PersonneDaoSqlImpl();
	        void Afficher() {
		        foreach ( var p in personnes.SelectAll() ) {
			        Console.WriteLine( p );
		        }
	        }

	        Afficher();

	        // INSERT
	        Console.WriteLine( "\nInsertion de données de test" );
	        var joe = new Personne { Prenom = "JoeJoe", Nom = "Worms" };
	        personnes.Insert( joe );
	        personnes.Insert(new Personne { Prenom = "Sully", Nom = "Gmod" });
	        personnes.Insert(new Personne { Prenom = "Ugo", Nom = "Simmer.io" });

	        Console.WriteLine( "Id auto incrementé=" + joe.Id );

	        Afficher();

	        // UPDATE
	        Console.WriteLine( "\nMise à jour de données" );
	        joe.Prenom = "Ronan";
	        joe.Nom = "Worms";
	        personnes.Update( joe );
	        Afficher();

	        // DELETE
	        Console.WriteLine( "\nSuppression de données" );
	        personnes.Delete( joe );
	        Afficher();
        }
    }
}