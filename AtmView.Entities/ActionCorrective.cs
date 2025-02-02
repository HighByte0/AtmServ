using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AtmView.Entities
{
    [Table("ActionCorrective")]
    // cette classe Entity crée automatiquement des champs ID sur chaque table oui je vois
    public class ActionCorrective : Entity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string User_Id { get; set; }

        public string Color { get; set; }

        public int EmailTo { get; set; }
        public int EmailCc { get; set; }
        public int PhoneTo { get; set; }
        public string Emails { get; set; }
        public string CcEmails { get; set; }
        public string Phones { get; set; }
        public int EscalatedTo { get; set; }
        public int Duration { get; set; }
        public string EscalatedEmails { get; set; }

        //public string Error { get; set; }      
        //public int? IdErrorType { get; set; }//clé ? non  on va l'utiliser pour faire des catégorie d'erreurs

        public virtual ICollection<AtmError> AtmErrors { get; set; }
        //public List<SelectListItem> EmailToType { get; set; }
        //public List<SelectListItem> EmailCcType { get; set; }
        //public List<SelectListItem> PhoneType { get; set; }

    }
}
