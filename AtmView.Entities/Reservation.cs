using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Reservation")]
    public class Reservation : Entity<int>
    {
        //[Key]
        //public string Id { get; set; }


        public Statutss ReservationStatut { get; set; }

        public Types ReservationType { get; set; }

        public int Client_Id { get; set; }
        [ForeignKey("Client_Id")]
        public virtual Client Client { get; set; }


        public int CashProvider_Id { get; set; }
        [ForeignKey("CashProvider_Id")]
        public virtual CashProvider CashProvider { get; set; }

        public Int64? Total { get; set; }

        public DateTime Date { get; set; }
    }
}
public enum Statutss
{
    Created,//0
    Validated,//1
    Pending,//2
    Canceled,//3
    Closed //4
}
public enum Types
{
    Retrait,//0
    Depot,//1
}