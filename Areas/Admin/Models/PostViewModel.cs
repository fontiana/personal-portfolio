using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Areas.Admin.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public object MyProperty { get; set; }
        public IFormFile Image { get; set; }
    }
}
