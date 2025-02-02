using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("RecoParams")]
    public class RecoParams
    {
        public int ID {  get; set; }
        
        public string FileType { get; set; }
        public string ParsingMethode { get; set; }
        public string Features { get; set; }
        public string DetailTrans { get; set; }
        public bool Active { get; set; }

        public string HostPath { get; set; }
        public string ArchivePath { get; set; }
    }
}
