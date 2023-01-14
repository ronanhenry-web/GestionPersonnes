using System;
using System.Collections.Generic;

namespace DemoDAO;

public class PersonneDaoSqlImpl : IPersonneDao
{
    public void Insert(Personne p)
    {
        // 1. Connexion
        using var conn = DbUtil.GetConnection();
        // 2. Requête SQL
        using var comm = conn.CreateCommand();
        comm.CommandText =
            " INSERT INTO personnes( prenom, nom, email, naissance ) VALUES ( @prenom, @nom, @email, @naissance ); SELECT @@IDENTITY FROM personnes ";
        // 3. Paramètres
        comm.Parameters.AddWithValue("@prenom", p.Prenom);
        comm.Parameters.AddWithValue("@nom", p.Nom);
        comm.Parameters.AddWithValue("@email", p.Email);
        comm.Parameters.AddWithValue("@naissance", p.DateNaissance);
        // 4. Execution
        p.Id = Convert.ToInt32(comm.ExecuteScalar());
        // 5. TODO Récupération du ou des id voir la requete @@IDENTITY
    }

    public void Update(Personne p)
    {
        // 1. Connexion
        using var conn = DbUtil.GetConnection();
        // 2. Requête SQL
        using var comm = conn.CreateCommand();
        comm.CommandText =
            " UPDATE personnes SET prenom=@prenom, nom=@nom, email=@email, naissance=@naissance WHERE id = @id ";
        // 3. Paramètres
        comm.Parameters.AddWithValue("@id", p.Id);
        comm.Parameters.AddWithValue("@prenom", p.Prenom);
        comm.Parameters.AddWithValue("@nom", p.Nom);
        comm.Parameters.AddWithValue("@email", p.Email);
        comm.Parameters.AddWithValue("@naissance", p.DateNaissance);
        // 4. Execution
        comm.ExecuteNonQuery();
    }

    public void Delete(Personne p)
    {
        // Délégation à la méthode DeleteById
        DeleteById(p.Id);
    }

    public void DeleteById(int id)
    {
        // 1. Connexion
        using var conn = DbUtil.GetConnection();
        // 2. Requête SQL
        using var comm = conn.CreateCommand();
        comm.CommandText = " DELETE FROM personnes WHERE id = @id ";
        // 3. Paramètres
        comm.Parameters.AddWithValue("@id", id);
        // 4. Execution
        comm.ExecuteNonQuery();
    }

    /// <returns> null si aucun résultat </returns>
    public Personne SelectById(int id)
    {
        using var conn = DbUtil.GetConnection();
        // 2. Requête SQL
        using var comm = conn.CreateCommand();

        comm.CommandText = " SELECT prenom, nom, email, naissance, id FROM personnes WHERE id = @id ";
        // 3. Paramètres
        comm.Parameters.AddWithValue("@id", id);

        using var reader = comm.ExecuteReader();

        Personne result = null;

        // 4. Execution
        if (reader.Read())
        {
            result = new Personne()
            {
                Prenom = reader.GetString(0),
                Nom = reader.GetString(1),
                Email = reader.GetString(2),
                DateNaissance = reader.GetDateTime(3),
                Id = reader.GetInt32(4)
            };
        }

        return result;
    }

    public List<Personne> SelectAll()
    {
        // 1. Connexion
        using var conn = DbUtil.GetConnection();
        // 2. Requête SQL
        using var comm = conn.CreateCommand();
        comm.CommandText = " SELECT prenom, nom, email, naissance, id FROM personnes ";
        // 3. Paramètres (si besoin)
        // 4. Execution
        using var reader = comm.ExecuteReader();
        // 5. Exploiter les résultats
        var result = new List<Personne>();
        while (reader.Read())
        {
            result.Add(new Personne
            {
                Prenom = reader.GetString(0),
                Nom = reader.GetString(1),
                Email = reader.GetString(2),
                DateNaissance = reader.GetDateTime(3),
                Id = reader.GetInt32(4)
            });
        }

        return result;
    }
}