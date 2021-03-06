﻿namespace Bloggable.Data
{
    using System;
    using System.Data.Entity;

    using Bloggable.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IBloggableDbContext : IDisposable
    {
        IDbSet<User> Users { get; }

        IDbSet<IdentityRole> Roles { get; }

        IDbSet<Post> Posts { get; }

        IDbSet<Page> Pages { get; }

        IDbSet<Tag> Tags { get; }

        IDbSet<Comment> Comments { get; }

        IDbSet<Rating> Ratings { get; }

        IDbSet<SearchTerm> SearchTerms { get; }

        IDbSet<Feedback> Feedback { get; }

        IDbSet<Referral> Referrals { get; }

        IDbSet<Setting> Settings { get; }

        IDbSet<AdministrationLog> AdministrationLogs { get; }

        int SaveChanges();
    }
}
