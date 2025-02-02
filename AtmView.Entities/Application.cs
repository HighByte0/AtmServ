using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Application")]
    public class Application : Entity<int>
    {

        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(250)]
        public string Titre { get; set; }
    }
}
