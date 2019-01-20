using System;
namespace Personal.Portfolio.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
        
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
