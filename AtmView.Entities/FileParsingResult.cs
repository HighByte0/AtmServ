using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;
using System.IO;
namespace AtmView.Entities
{
    public class FileParsingResult
    {
        public string FileName { get; set; }
        public string Hashfile { get; set; }
        public long duration { get; set; }//duration
        public DateTime ParsingDate { get; set; }//ParsingDate
        public string FileType { get; set; }//FileType
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long NbrTransactions { get; set; }
        public double MontantTotal { get; set; }
        public DataFrame Transactions { get; set; }
        public int NbrGAB { get; set; }
        public List<GabInfos> GABInfos { get; set; }



    }


} //class for reco results
