using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BugStatut")]
    public class BugStatut : Entity<int>
    {
        public string Name { get; set; }////1 nouveau , 2 affecte, 3 realisé, 4 cloture

    }
}
