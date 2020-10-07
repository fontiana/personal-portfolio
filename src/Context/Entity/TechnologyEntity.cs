using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Context.Entity
{
    public class TechnologyEntity
    {
        [Key]
        public int TechnologyId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
    }
}
