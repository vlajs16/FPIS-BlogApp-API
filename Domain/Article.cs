using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Article
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string PhotoURL { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
    }
}
