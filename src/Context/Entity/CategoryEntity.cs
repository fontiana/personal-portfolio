using System.ComponentModel.DataAnnotations;

namespace PersonalPortfolio.Context.Entity
{
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
    }
}
