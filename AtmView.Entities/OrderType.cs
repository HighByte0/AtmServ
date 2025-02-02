using System.ComponentModel;

namespace AtmView.Entities
{
    public class OrderType
    {
        public int ID { get; set; }
        [DisplayName("Item")]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Price (each)")]
        public double UnitPrice { get; set; }
    }
}