using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AtmView.Entities
{
    public class Invoice
    {

        [DisplayName("Numéro de facture")]
        public int ID { get; set; }
        //public int? ClientID { get; set; }

        [DisplayName("Date début")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [DisplayName("Date fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        [DisplayName("Totale TTC")]
        public double SalesTaxPercent { get; set; }

        [DisplayName("Payment Amount")]
        public double PaymentAmount { get; set; }
        // public double AmountDue { get; set; }
        [DisplayName("Statut")]
        public string Status
        {
            get;
            //if ((GrandTotal == PaymentAmount) && (AmountDue == 0))
            //{
            //return "Successful";
            //}

            //return "Pending";

            set;

        }


        //public int? CashProvider_Id { get; set; }

        // The CashProvider of CashProvider is automatically stroed in the CashProvider
        public virtual CashProvider CashProvider { get; set; }

        // Inovices items will be laaded autmatically in this
        public virtual IList<InvoiceItem> InvoiceItems { get; set; }


        #region Calculated Fields
        public double TotalSalesTax
        {
            get
            {
                return SubTotal * SalesTaxPercent / 100;
            }

            /*protected*/
            set { } // Declare an empty set so that it cannot be assigned but stored in db
        }

        [DisplayName("Totale de TVA")]
        public double SubTotal
        {
            get
            {
                return InvoiceItems == null ? 0 : InvoiceItems.Sum(item => item.Total);
            }
            /*protected*/
            set { } // Declare an empty set so that it cannot be assigned but stored in db
        }

        [DisplayName("Grand Total")]
        public double GrandTotal
        {
            get
            {
                return SubTotal + TotalSalesTax;
            }
            /*protected*/
            set { }   // Declare an empty set so that it cannot be assigned but stored in db
        }
        [DisplayName("Total hors taxe")]
        public double AmountDue
        {
            get
            {

                return GrandTotal - PaymentAmount;

            }
            /*protected*/
            set { }
        }

        #endregion
    }
}