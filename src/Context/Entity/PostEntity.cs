using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Context.Entity
{
    public class PostEntity
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShowcaseImage { get; set; }

        public List<CategoryEntity> Categories { get; set; }
    }
}
