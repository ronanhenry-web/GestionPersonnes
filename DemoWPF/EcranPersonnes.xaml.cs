using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DemoDAO;

namespace DemoWPF
{
    public partial class EcranPersonnes : Window
    { 
	    // Emplacement du fichier de données sur le disque
	private const string PersonnesCsv = @"../../../personnes.csv";

	private IPersonneDao _personneDao = new PersonneDaoSqlImpl();

	// Listes des personnes qui sera liée (bindée) à la grille de données (DataGrid)
	private ObservableCollection<Personne> ListePersonnes;

	// Position dans cette liste
	private int Index = 0;

	// Objet chargé de refleter les données du formulaire et lié (bindé) aux champs de saisies
	private Personne FormData { get; set; }

	// Executé une fois au démarrage de l'application
	public EcranPersonnes() {
		ListePersonnes = new(_personneDao.SelectAll());
		// Dessine la fenetre
		InitializeComponent();
		// Affiche les contacts du fichier csv dans la grille de données
//		LireCsv();
		// Binding entre les Properties d'une personne (FormData) et les champs de saisies
		if ( ListePersonnes.Count == 0 )
		{
			// Affiche un formulaire vide
			Nouveau();
		}
		else
		{
			// Affiche le premier contact s'il y en a un.
			Afficher( ListePersonnes[ 0 ] );
		}
		// Binding entre les Properties des personnes (Liste) et les colonnes de la grille (DataGrid)
		DataGrid1.DataContext = ListePersonnes;
		DataGrid1.ItemsSource = ListePersonnes;
	}

	private void Ajouter( object sender , RoutedEventArgs ev )
	{
		Console.WriteLine( "Ajouter" );

		// Conversions de DateTime en DateOnly
		FormData.DateNaissance = DateNaissance.SelectedDate.HasValue == false
			? null : DateNaissance.SelectedDate.Value.Date;

		// Affectation des saisies utilisateur (manuellement) à une instance de Personne
		 FormData.Prenom = Prenom.Text;
		 FormData.Nom = Nom.Text;
		 FormData.Email = Email.Text;

		Console.WriteLine( FormData );
		ListePersonnes.Add( FormData.Clone() );
		Index++;
//		EcrireCsv();
		_personneDao.Insert( FormData );
	}

	private void Modifier( object sender , RoutedEventArgs ev )
	{
		Console.WriteLine( "Modifier" );

		// Conversions de DateTime en DateOnly
		FormData.DateNaissance = DateNaissance.SelectedDate.HasValue == false 
			? null : DateNaissance.SelectedDate.Value.Date ;

		// Affectation des saisies utilisateur (manuellement) à une instance de Personne
		 FormData.Prenom = Prenom.Text;
		 FormData.Nom = Nom.Text;
		 FormData.Email = Email.Text;

		Console.WriteLine( FormData );
		ListePersonnes[ Index ] = FormData.Clone();
//		EcrireCsv();
		_personneDao.Update( FormData );
	}

	private void Nouveau( object sender, RoutedEventArgs ev )
	{
		Nouveau();
	}

	private void Nouveau()
	{
		FormData = new Personne();
		FormData.Email = "@"; // placeholder
		DataContext = FormData;
		Email.Text = "@"; // placeholder
	}

	private void Supprimer( object sender, RoutedEventArgs ev )
	{
		// on peut supprimer un contact si la liste n'est pas vide
		if ( Index <= ListePersonnes.Count - 1 )
		{
			ListePersonnes.RemoveAt( Index );
			if ( ListePersonnes.Count == 0 )
			{
				// plus personne dans la liste, afficher un formulaire vide
				Nouveau();
			}
			else if ( Index > ListePersonnes.Count - 1 )
			{
				// on a supprimé la dernière personne de la liste, afficher la précédente
				--Index;
				Afficher( ListePersonnes[ Index ] );
			}
			else
			{
				// on a supprimé une personne au milieu de la liste, afficher la suivante
				Afficher( ListePersonnes[ Index ] );
			}
		}
//		EcrireCsv();
		_personneDao.Delete( FormData );
	}

	private void Precedent( object sender, RoutedEventArgs ev )
	{
		if ( Index > 0 )
		{
			--Index;
			Afficher( ListePersonnes[ Index ] );
		}
		else
		{
			// carrousel : on revient à la fin de la liste
			Index = ListePersonnes.Count - 1;
			Afficher( ListePersonnes[ Index ] );
		}
	}

	private void Suivant( object sender, RoutedEventArgs ev )
	{
		if ( Index < ListePersonnes.Count - 1 )
		{
			++Index;
			Afficher( ListePersonnes[ Index ] );
		}
		else
		{
			// carrousel : on revient au début de la liste
			Index = 0;
			Afficher( ListePersonnes[ Index ] );
		}
	}

	// Affiche les données d'une personne dans le formulaire
	// en se préoccupant du Binding entre les champs de saisies et les Properties de la personne
	private void Afficher( Personne p )
	{
		DataContext = FormData = p.Clone();
	}
    }
}