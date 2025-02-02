using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("ConfigCassette")]
    public class ConfigCassette : Entity<int>
    {
        public ConfigCassette()
        {

        }

        // public int Id { get; set; }


        //[MaxLength(100)]
        //public string Name { get; set; }

        public string Currency { get; set; }
        public string Type { get; set; }

        public string Edition { get; set; }

        public int? CassetteSetupId { get; set; }

        [ForeignKey("CassetteSetupId")]
        public virtual CassetteSetup CassetteSetup { get; set; }
    }
}
