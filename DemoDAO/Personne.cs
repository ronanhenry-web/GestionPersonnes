using System;

namespace DemoDAO;

// exemple de DTO
public class Personne
{
   public int Id { get; set; }
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public DateTime? DateNaissance { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, " +
               $"{nameof(Nom)}: {Nom}, " +
               $"{nameof(Prenom)}: {Prenom}, " +
               $"{nameof(Email)}: {Email}, " +
               $"{nameof(DateNaissance)}: {DateNaissance:dd MMMM yyyy}";
    }

    public Personne Clone()
    {
        return (Personne) MemberwiseClone();
    }
}