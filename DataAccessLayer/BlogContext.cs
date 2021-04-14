using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer
{
    public class BlogContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) 
            : base(options)
        {
        }


    }
}
