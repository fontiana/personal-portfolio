using System;

namespace PersonalPortfolio.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ShowcaseImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
