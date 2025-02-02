using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AtmView.Entities
{
    [Table("FileHashcode")]
    public class FileHashcode
    {

        public string HashCode { get; set; }
        public bool isReconciled { get; set; }
    }
}
