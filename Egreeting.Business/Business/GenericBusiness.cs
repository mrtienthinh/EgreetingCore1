using Egreeting.Business.IBusiness;
using Egreeting.Models.AppContext;
using Egreeting.Repository.IRepository;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Egreeting.Business.Business
{
    public class GenericBusiness<T> : IGenericBusiness<T> where T : class
    {
        protected IGenericRepository<T> repository;
        protected ILog logger;
        public EgreetingContext context;

        public GenericBusiness(ILog logger, EgreetingContext context = null)
        {
            if (context == null)
            {
                context = new EgreetingContext();
            }
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.logger = logger;
        }

        public GenericBusiness()
        {

        }

        public virtual IQueryable<T> All
        {
            get { return repository.All; }
        }

        public virtual IQueryable<T> AllNoTracking
        {
            get { return repository.AllNoTracking; }
        }

        public virtual T Find(object id)
        {
            return repository.Find(id);
        }

        public virtual T Insert(T entityToInsert)
        {
            Type t = entityToInsert.GetType();
            PropertyInfo md = t.GetProperty("CreatedDate");
            if (md != null && md.GetValue(entityToInsert, null) == null)
            {
                md.SetValue(entityToInsert, DateTime.Now, null);
            }
            repository.Insert(entityToInsert);
            return entityToInsert;
        }

        public virtual T Update(T entityToUpdate)
        {
            Type t = entityToUpdate.GetType();
            PropertyInfo md = t.GetProperty("ModifiedDate");
            if (md != null)
            {
                md.SetValue(entityToUpdate, DateTime.Now, null);
            }
            repository.Update(entityToUpdate);
            return entityToUpdate;
        }

        public virtual T UpdateNotModifiedDate(T entityToUpdate)
        {
            repository.Update(entityToUpdate);
            return entityToUpdate;
        }

        public virtual void Delete(object id)
        {
            repository.Delete(id);

        }

        public virtual void DeleteAll(List<T> entities)
        {
            repository.DeleteAll(entities);
        }

        public virtual void Save()
        {
            repository.Save();
        }

        public virtual void Dispose()
        {
            repository.Dispose();
        }

        /// <summary>
        /// set isAutoDetectChangesEnabled=false de tang hieu nang khi thuc hien them moi, xoa,sua  
        /// </summary>
        /// <param name="isAutoDetectChangesEnabled"></param>
        public void SetAutoDetectChangesEnabled(bool isAutoDetectChangesEnabled)
        {
            repository.SetAutoDetectChangesEnabled(isAutoDetectChangesEnabled);
        }
    }
}
