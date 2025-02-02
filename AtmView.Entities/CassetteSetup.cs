using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AtmView.Entities
{
    [Table("CassetteSetup")]
    public class CassetteSetup : Entity<int>
    {
        public CassetteSetup()
        {

        }

        [MaxLength(100)]
        public string Name { get; set; }

        [XmlIgnore]
        public virtual ICollection<ConfigCassette> ConfigCassettes { get; set; }
    }
}
