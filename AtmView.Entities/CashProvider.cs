using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{

    [Table("CashProvider")]
    public class CashProvider : Entity<int>
    {
        //[Key]
        //public string CashProvider_Id { get; set; }
        public string CashProvider_Name { get; set; }
        public string CashProvider_Adress { get; set; }
        public string CashProvider_Phone { get; set; }
        public string CashProvider_Fax { get; set; }
        public string CashProvider_Email { get; set; }
        public string CashProvider_ICE { get; set; }
        public string CashProvider_ContactName { get; set; }
        public string CashProvider_IdFiscal { get; set; }



        public virtual ICollection<DoneOrder> DoneOrders { get; set; }
        public virtual ICollection<Parameters> Parameters { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Atm> Atm { get; set; }
        public virtual IList<Invoice> Invoices { get; set; }

    }
}


