using System.Collections.Generic;

namespace PersonalPortfolio.Models
{
    public class BlogViewModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }

    public class Post
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
