using System;
namespace PersonalPortfolio.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DashedTitle { get { return Title.ToLower().Replace(" ", "-"); } }
        public string Description { get; set; }
        public string Showcase { get; set; }
        public string Url { get; set; }
        public string Technologies { get; set; }
    }
}
