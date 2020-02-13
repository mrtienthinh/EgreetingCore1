using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Business.IBusiness
{
    public interface IGenericBusiness<T> where T : class
    {
        IQueryable<T> All { get; }

        IQueryable<T> AllNoTracking { get; }

        T Find(object id);

        T Insert(T entityToInsert);

        T Update(T entityToUpdate);

        T UpdateNotModifiedDate(T entityToUpdate);

        void Delete(object id);

        void DeleteAll(List<T> entities);

        void Save();

        void Dispose();

        void SetAutoDetectChangesEnabled(bool isAutoDetectChangesEnabled);
    }
}
