using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class Im_Atm_Inventory
    {
        [Key]
        public int Id { get; set; }

        public DateTime? InstallationDate { get; set; }

        [MaxLength(20)]
        public string LocationType { get; set; }

        [MaxLength(100)]
        public string Communication { get; set; }

        [MaxLength(100)]
        public string Security { get; set; }

        [MaxLength(100)]
        public string EPP { get; set; }

        [MaxLength(100)]
        public string ScreenDisplay { get; set; }

        [MaxLength(100)]
        public string CashDispenser { get; set; }

        [MaxLength(100)]
        public string CardReader { get; set; }

        [MaxLength(100)]
        public string Printers { get; set; }

        [MaxLength(100)]
        public string PowerSupply { get; set; }

        [MaxLength(100)]
        public string Dimension { get; set; }

        [MaxLength(10)]
        public string TouchScreen { get; set; }

        public int Camera { get; set; }

        [MaxLength(100)]
        public string EnvironmentalConditions { get; set; }

        [MaxLength(100)]
        public string SupportedLanguages { get; set; }

        [MaxLength(40)]
        public string Cost { get; set; }

        [MaxLength(40)]
        public string CostCurrency { get; set; }

        [MaxLength(150)]
        public string Notes { get; set; }

        [MaxLength(128)]
        public string Atm_id { get; set; }

        public int Computer_id { get; set; }
        [ForeignKey("Atm_id")]
        public virtual Atm Atm { get; set; }
        [ForeignKey("Computer_id")]
        public virtual Im_Computer_Inventory Computer { get; set; }

        public virtual ICollection<Im_Move_Inventory> MoveInventories { get; set; }
    }
}
