using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("BinsAssociation")]
    public  class BinsAssociation : Entity<int>
    {
        // Node type 
        public int Node_Type { get; set; }
        [ForeignKey("Node_Type")]
        public virtual NodeType NodeType { get; set; }

        // Nom du node a configuré
        public string Name { get; set; }
        // Bin config 
        public int Bin_Id { get; set; }
        [ForeignKey("Bin_Id")]
        public virtual BinConfiguration BinConfiguration { get; set; }

      
    }
}
