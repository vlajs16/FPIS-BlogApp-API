using DataAccessLayer;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData
{
    public class Seed
    {
        public static void SeedCategories(BlogContext context)
        {
            if (!context.Categories.Any())
            {
                var categoriesData = System.IO.File.ReadAllText("../Seed/Data/CategorySeed.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);

                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }

                context.SaveChanges();
            }
        }

        public static void SeedArticles(BlogContext context)
        {
            if (!context.Articles.Any())
            {
                var articlesData = System.IO.File.ReadAllText("../Seed/Data/ArticleSeed.json");
                var articles = JsonConvert.DeserializeObject<List<Article>>(articlesData);

                foreach (var article in articles)
                {
                    article.Category = context.Categories.FirstOrDefault(x => x.Name == article.Category.Name);
                    context.Articles.Add(article);
                }

                context.SaveChanges();
            }
        }
    }
}
