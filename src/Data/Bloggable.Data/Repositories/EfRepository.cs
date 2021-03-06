﻿namespace Bloggable.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Bloggable.Data.Contracts.Repositories;

    public class EfRepository<T> : IRepository<T>, IDisposable
        where T : class
    {
        public EfRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public virtual IQueryable<T> All() => this.DbSet.AsQueryable();

        public virtual T GetById(params object[] id) => this.DbSet.Find(id);

        public virtual void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public virtual void Update(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(params object[] id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public int SaveChanges() => this.Context.SaveChanges();

        public void Dispose() => this.Context.Dispose();
    }
}
