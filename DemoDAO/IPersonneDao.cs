using System.Collections.Generic;

namespace DemoDAO
{
    public interface IPersonneDao
    {
        public void Insert(Personne p);
        public void Update(Personne p);
        public void Delete(Personne p);
        void DeleteById(int id);
        
        /// <returns> null si aucun résultat </returns>
        Personne SelectById(int id);
        public List<Personne> SelectAll();

    }
}