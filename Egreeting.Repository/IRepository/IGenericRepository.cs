using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Repository.IRepository
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Gets all objects from database 
        /// </summary>
        IQueryable<T> All { get; }

        /// <summary>
        /// Gets all objects from database with no tracking
        /// </summary>
        IQueryable<T> AllNoTracking { get; }

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="id">Specified the search keys.</param>
        T Find(object id);

        /// <summary>
        /// Insert object to database.
        /// </summary>
        /// <param name="entity">Specified the object to save.</param>
        void Insert(T entityToInsert);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="entity">Specified the object to save.</param>
        void Update(T entityToUpdate);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="id">Specified a existing object to delete.</param>
        void Delete(object id);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="id">Specified a existing object to delete.</param>
        void Delete(T entityToDelete);

        /// <summary>
        /// Delete list object from database.
        /// </summary>
        /// <param name="id">Specified a existing object to delete.</param>
        void DeleteAll(List<T> entities);

        /// <summary>
        /// Save change to database.
        /// </summary>
        /// <param name="t">Specified the object to save.</param>
        void Save();

        /// <summary>
        /// set isAutoDetectChangesEnabled=false de tang hieu nang khi thuc hien them moi, xoa,sua  
        /// </summary>
        /// <param name="isAutoDetectChangesEnabled"></param>
        void SetAutoDetectChangesEnabled(bool isAutoDetectChangesEnabled);
    }
}
