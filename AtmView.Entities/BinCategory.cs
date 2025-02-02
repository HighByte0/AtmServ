using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BinCategory")]
    public class BinCategory : Entity<int>
    {
        public string Type { get; set; }
        public string color { get; set; }
    }
}
