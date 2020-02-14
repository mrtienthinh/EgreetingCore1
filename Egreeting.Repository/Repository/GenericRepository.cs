using Egreeting.Models.AppContext;
using Egreeting.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Egreeting.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal EgreetingContext Context;
        internal DbSet<T> DbSet;
        public string connectionString;

        public GenericRepository()
        {
        }

        public GenericRepository(EgreetingContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public IQueryable<T> All
        {
            get { return DbSet; }
        }

        public IQueryable<T> AllNoTracking
        {
            get { return DbSet.AsNoTracking(); }
        }

        public T Find(object id)
        {
            return DbSet.Find(id);
        }

        public void Insert(T entityToInsert)
        {
            DbSet.Add(entityToInsert);
        }

        public void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;

        }

        public void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }


        public void Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            this.Delete(entityToDelete);
        }


        public void DeleteAll(List<T> entities)
        {
            foreach (T entity in entities)
            {
                this.Delete(entity);
            }
        }

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }


        public void SetAutoDetectChangesEnabled(bool isAutoDetectChangesEnabled)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = isAutoDetectChangesEnabled;
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
