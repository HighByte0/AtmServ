using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtmView.Entities
{
    public class Client : Entity<int>
    {
        public string Name { get; set; }
        public string Raison_social { get; set; }
        public string Secteur_Activite { get; set; }
        public string Adresse_Courrier { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEnd { get; set; }
        public contract? ContractStatut { get; set; }
        public int Agency_Id { get; set; }

        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }
    }

    public enum contract
    {
        En_Cours,//0
        terminer,//1
    }
}