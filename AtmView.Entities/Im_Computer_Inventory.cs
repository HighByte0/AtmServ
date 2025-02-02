using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    public class Im_Computer_Inventory
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string SerialNumber { get; set; }

        [MaxLength(50)]
        public string Marque { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }

        [MaxLength(50)]
        public string ComputerType { get; set; }

        [MaxLength(50)]
        public string OperatingSystem { get; set; }

        [MaxLength(50)]
        public string CPU { get; set; }

        [MaxLength(50)]
        public string RAM { get; set; }

        [MaxLength(50)]
        public string Storage { get; set; }

        [MaxLength(20)]
        public string StorageType { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [MaxLength(30)]
        public string PurchaseOrderNumber { get; set; }

        [MaxLength(50)]
        public string Warranty { get; set; }

        [MaxLength(50)]
        public string WarrantyLength { get; set; }

        public DateTime? WarrantyExpirationDate { get; set; }

        public DateTime? DeliveryDate { get; set; }
    }
}
