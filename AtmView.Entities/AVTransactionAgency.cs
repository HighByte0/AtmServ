using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AVTransactionAgency")]
    public class AVTransactionAgency : Entite, IEquatable<AVTransactionAgency>
    {

        [Key]
        [Column(Order = 1)]
        public int Agency_Id { get; set; }
        [Key]
        [Column(Order = 2)]
        [DisplayFormat(DataFormatString = "{0:s}",
               ApplyFormatInEditMode = true)]
        public DateTime TransactionDate { get; set; }
        public String AutorisationNumber { get; set; }
        [Key]
        [Column(Order = 3)]
        public Int64? Withdrawal { get; set; }
        public Int64? Deposit { get; set; }
        public Int64? SoldeOuverture { get; set; }
        public Int64? SoldeCloture { get; set; }

        public int Statut { get; set; }

        public bool Equals(AVTransactionAgency other)
        {
            throw new NotImplementedException();
        }
    }
}
