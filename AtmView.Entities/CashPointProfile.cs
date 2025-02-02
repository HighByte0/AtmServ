using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CashPointProfile")]
    public class CashPointProfile : Entity<int>
    {

        public string Name { get; set; }



    }
}
