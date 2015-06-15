﻿namespace Bloggable.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    using Bloggable.Data.Contracts;
    using Bloggable.Data.Contracts.CodeFirstConventions;
    using Bloggable.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class BloggableDbContext : IdentityDbContext<User>, IBloggableDbContext
    {
        public BloggableDbContext()
            : base("DefaultConnection", false)
        {
        }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Page> Pages { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<SearchTerm> SearchTerms { get; set; }

        public virtual IDbSet<Feedback> Feedback { get; set; }

        public virtual IDbSet<Referral> Referrals { get; set; }

        public static BloggableDbContext Create()
        {
            return new BloggableDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Without this call EntityFramework won't be able to configure the identity model
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add<IsUnicodeAttributeConvention>();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            var auditInfoEntries = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entry in auditInfoEntries)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
