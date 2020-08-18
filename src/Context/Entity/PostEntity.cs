using System.Collections.Generic;

namespace PersonalPortfolio.Context.Entity
{
    public class PostEntity
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShowcaseImage { get; set; }

        public List<CategoryEntity> Categories { get; set; }
    }
}
