using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtmView.Entities
{
    [Table("AVAtmConfig")]
    public class AVAtmConfig : Entity<int>
    {
        public string Atm_Id { get; set; }
        public String AtmIP { get; set; }
        public int Current_Line { get; set; }
        public int Current_Jrn_Line { get; set; }
        public String Current_File { get; set; }
        public String Journal_Dir { get; set; }
        public String Agent_Version { get; set; }
        public DateTime Last_Exchange { get; set; }

        [ForeignKey("Atm_Id")]
        public virtual Atm Atm { get; set; }


    }
}

