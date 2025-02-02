using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Package")]
    public class Package : Entity<int>
    {
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public int Type { get; set; }
        public string ZipName { get; set; }
        public string ZipPath { get; set; }
        public DateTime CreationDate { get; set; }
        public string Tag { get; set; }
        public string HotFixId { get; set; }
        public bool? Dynamic { get; set; }
        public bool? RebootAfter { get; set; }
    }
}
