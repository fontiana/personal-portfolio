using System.Collections.Generic;

namespace PersonalPortfolio.Models
{
    public class IndexViewModel
    {
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
        public List<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
    }
}
