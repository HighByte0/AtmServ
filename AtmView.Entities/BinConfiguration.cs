using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("BinConfiguration")]
    public class BinConfiguration : Entity<int>
    {
        public string Type { get; set; }
        public string key { get; set; }
        public string color { get; set; }
        public int BinCategory_Id { get; set; }

        [ForeignKey("BinCategory_Id")]
        public virtual BinCategory BinCategory { get; set; }

    }
}
