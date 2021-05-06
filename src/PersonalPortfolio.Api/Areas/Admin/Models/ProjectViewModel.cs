using System;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string TechStack { get; set; }
        public string Url { get; set; }
        public IFormFile Image { get; set; }
    }
}
