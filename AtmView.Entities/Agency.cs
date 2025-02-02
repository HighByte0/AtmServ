using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("Agency")]
    public class Agency : Entity<string>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Profile { get; set; }

        [MaxLength(50)]
        public string Location { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

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
        //public virtual ICollection<AtmContact> AtmContacts { get; set; }//ICollection


        //[MaxLength(50)]
        //public string HostName { get; set; }


        //public int? PortNumber { get; set; }


        //public bool EnableTLS { get; set; }

        //public int MyProperty1 { get; set; }


        //public int Agence_Id { get; set; }
        //[ForeignKey("Agence_Id")]
        //public virtual Agence Agence { get; set; }




        public virtual ICollection<DoneOrderAgency> DoneOrderAgencys { get; set; }
        public virtual ICollection<ParametersAgency> ParameterAgencys { get; set; }
        public virtual ICollection<OrderAgency> OrderAgencys { get; set; }
       
        public virtual ICollection<FactorAgency> FactorAgencys { get; set; }
        public virtual ICollection<PredictorAgency> PredictorAgencys { get; set; }
        public virtual ICollection<TempAlim> TempAlims { get; set; }



    }
}
