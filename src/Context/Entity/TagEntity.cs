using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Context.Entity
{
    public class TagEntity
    {
        [Key]
        public int TagId { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
    }
}
