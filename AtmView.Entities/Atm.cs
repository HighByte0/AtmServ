using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace AtmView.Entities
{
    [Table("Atm")]
    public class Atm : Entity<string>
    {


        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Profile { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string JrnId { get; set; }

        public int Address_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address Address { get; set; }


        [MaxLength(50)]
        public string Organisation { get; set; }

        [MaxLength(50)]
        public string SoftweareRelease { get; set; }


        public DateTime? InstallationDate { get; set; }

        public int? SerialNumber { get; set; }

        [MaxLength(50)]
        public string System { get; set; }

        //contacts
        //public virtual IEnumerable<Contact> Contacts { get; set; }
        [XmlIgnore]
        public virtual ICollection<AtmContact> AtmContacts { get; set; }//ICollection


        [MaxLength(50)]
        public string HostName { get; set; }


        public int? PortNumber { get; set; }


        public bool EnableTLS { get; set; }
        public bool Actif { get; set; }


        //public int MyProperty1 { get; set; }

        public int? CassetteSetup_Id { get; set; }

        [ForeignKey("CassetteSetup_Id")]
        public virtual CassetteSetup CassetteSetup { get; set; }

        public int? Agency_Id { get; set; }

        [ForeignKey("Agency_Id")]
        public virtual Branch Branch { get; set; }


        [XmlIgnore]
        public virtual ICollection<State> States { get; set; }
        [XmlIgnore]
        public virtual ICollection<Parameters> Parameters { get; set; }
        [XmlIgnore]
        public virtual ICollection<Incident> Incidents { get; set; }
        [XmlIgnore]
        public virtual ICollection<JournalEntry> JournalEntrys { get; set; }
        [XmlIgnore]
        public virtual ICollection<JobAtm> JobAtms { get; set; }
        [XmlIgnore]
        public virtual ICollection<BugAtm> BugAtms { get; set; }
        [XmlIgnore]
        public virtual ICollection<AtmError> AtmErrors { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
        [XmlIgnore]
        public virtual ICollection<UserAtm> UserAtm { get; set; }
        [XmlIgnore]
        public virtual ICollection<TempAlim> TempAlims { get; set; }

        public virtual double? Lat { get; set; }

        public virtual double? Long { get; set; }
        public string Account { get; set; }
    }
}
