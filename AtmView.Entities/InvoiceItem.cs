namespace AtmView.Entities
{
    public class InvoiceItem
    {
        public int ID { get; set; }
        public int? OrderTypeID { get; set; }
        public int? InvoiceID { get; set; }
        public int Quantity { get; set; }
        public bool Taxable { get; set; }
        public virtual OrderType OrderType { get; set; }
        public virtual Invoice Invoice { get; set; }

        #region calculated fields
        public double Total
        {
            get
            {
                return OrderType == null ? 0 : Quantity * this.OrderType.UnitPrice;
            }
            protected set { } // Declare an empty set so that it cannot be assigned but stored in db
        }
        #endregion
    }
}