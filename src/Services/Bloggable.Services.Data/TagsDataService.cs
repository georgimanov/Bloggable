﻿namespace Bloggable.Services.Data
{
    using System;
    using System.Linq;

    using Bloggable.Data.Contracts.Repositories;
    using Bloggable.Data.Models;
    using Bloggable.Services.Data.Contracts;

    public class TagsDataService : ITagsDataService
    {
        private readonly IRepository<Tag> tags;

        public TagsDataService(IRepository<Tag> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            this.tags = tags;
        }

        public Tag GetById(object id) => this.tags.GetById(id);

        public Tag GetByNameOrCreate(string name)
        {
            var tag = this.tags.All().FirstOrDefault(t => t.Name == name);

            if (tag == null)
            {
                tag = new Tag { Name = name };
                this.tags.Add(tag);
                this.tags.SaveChanges();
            }

            return tag;
        }

        public IQueryable<Tag> GetMostPopularTags(int count)
        {
            var mostPopularTags = this.tags
                .All()
                .Where(t => t.Posts.Any(p => !p.IsDeleted))
                .OrderByDescending(t => t.Posts.Count(p => !p.IsDeleted))
                .Take(count);

            return mostPopularTags;
        }
    }
}
