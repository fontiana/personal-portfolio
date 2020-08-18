using System.Collections.Generic;

namespace PersonalPortfolio.Context.Entity
{
    public class ProjectEntity
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ShowcaseImage { get; set; }
        public string Images { get; set; }
        public List<TechnologyEntity> Technologies { get; set; }
    }
}
