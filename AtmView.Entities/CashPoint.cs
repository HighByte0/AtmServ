using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("CashPoint")]
    public class CashPoint : Entity<int>
    {
        public string CashPointName { get; set; }
        public int? Profile_Id { get; set; }

        [ForeignKey("Profile_Id")]
        public virtual CashPointProfile CashPointProfile { get; set; }

        public string Location { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public int? Address_Id { get; set; }
        [ForeignKey("Address_Id")]
        public virtual Address Address { get; set; }


        [MaxLength(50)]
        public string Organisation { get; set; }

        [MaxLength(50)]
        public string SoftweareRelease { get; set; }


        public DateTime? InstallationDate { get; set; }

        [MaxLength(50)]
        public string System { get; set; }

        [MaxLength(50)]
        public string HostName { get; set; }


        public int? PortNumber { get; set; }


        public bool? EnableTLS { get; set; }
        public int? SerialNumber { get; set; }
        public virtual ICollection<CashPointContact> CashPointContacts { get; set; }//ICollection
                                                                                    //public virtual ICollection<State> States { get; set; }
        public virtual ICollection<Parameters> Parameters { get; set; }
        //public virtual ICollection<Incident> Incidents { get; set; }
        //public virtual ICollection<JournalEntry> JournalEntrys { get; set; }
        //public virtual ICollection<JobAtm> JobAtms { get; set; }
        //public virtual ICollection<BugAtm> BugAtms { get; set; }
        //public virtual ICollection<AtmError> AtmErrors { get; set; }
        //public virtual ICollection<TempAlim> TempAlims { get; set; }

    }

}


