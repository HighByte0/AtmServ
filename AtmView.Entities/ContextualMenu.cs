using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("ContextualMenu")]
    public class ContextualMenu : Entity<int>
    {
        public string Libelle { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Url { get; set; }
        public int OrdreAffichage { get; set; }
        public string Libelle_eng { get; set; }

        public int NodeType_Id { get; set; }

        [ForeignKey("NodeType_Id")]
        public virtual NodeType NodeType { get; set; }


    }
}
