using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("OrderCategory")]
    public class OrderCategory : Entity<int>
    {

        public string Name { get; set; }//Atm // Branch // Client



    }
}
