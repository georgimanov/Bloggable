﻿namespace Bloggable.Services.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Bloggable.Data.Contracts.Repositories;
    using Bloggable.Data.Models;
    using Bloggable.Services.Data.Contracts;

    public class PostsDataService : IPostsDataService
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsDataService(IDeletableEntityRepository<Post> posts)
        {
            if (posts == null)
            {
                throw new ArgumentNullException(nameof(posts));
            }

            this.posts = posts;
        }

        public IQueryable<Post> GetAll(Expression<Func<Post, bool>> filter = null, bool includeDeleted = false)
        {
            var allPosts = includeDeleted ? this.posts.AllWithDeleted() : this.posts.All();

            if (filter != null)
            {
                allPosts = allPosts.Where(filter);
            }

            return allPosts;
        }

        public IQueryable<Post> GetByTag(string tag, bool includeDeleted = false)
        {
            var allPosts = includeDeleted ? this.posts.AllWithDeleted() : this.posts.All();

            var postsByTag = allPosts.Where(p => p.Tags.Any(t => t.Name == tag));

            return postsByTag;
        }

        public IQueryable<Post> GetPagePosts(
            int page,
            int pageSize,
            Expression<Func<Post, object>> orderKeySelector,
            bool ascending = true,
            bool includeDeleted = false)
        {
            if (page < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "page should be non-negative number.");
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize should be non-negative number.");
            }

            var allPosts = this.GetAll(null, includeDeleted);

            var orderedPosts = ascending 
                ? allPosts.OrderBy(orderKeySelector) 
                : allPosts.OrderByDescending(orderKeySelector);

            var postsPage = orderedPosts.Skip(page * pageSize).Take(pageSize);

            return postsPage;
        }
    }
}
