using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.API.Core.Dao.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Récuperation de l'ensemble des éléments d'une table
        IEnumerable<TEntity> GetAll(int nb);

        // Récuperation de l'ensemble des éléments d'une table
        IEnumerable<TEntity> GetAll();

        // Récuperation d'un élement d'une table via son ID unique
        TEntity GetById(long id);

        // Insertion d'un élément au sein d'une table
        bool Create(TEntity entity);

        // Suppression d'un élement d'une table via son ID unique
        bool Delete(long id);

        // Mise à jour d'un élement d'une table via son ID unique
        bool Update(TEntity entity, long id);
    }
}
