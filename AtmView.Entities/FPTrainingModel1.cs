using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("TrainData1")]
    public class FPTrainingModel_ : Entite
    {
        [Key]
        [Column(Order = 1)]
        public string AtmID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime TransactionDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public int TransactionNumber { get; set; }

        public string CardNumber { get; set; }

        public double isCashPresented_toInt { get; set; }

        public double isCashTaken_toInt { get; set; }

        public double isCashRetracted_toInt { get; set; }

        public double isCashoutError_toInt { get; set; }

        public double ExistInHost_toInt { get; set; }

        public double Type_encoded_scaled { get; set; }

        public double Amount_scaled { get; set; }

        public double Statut_scaled { get; set; }

        public double CardNumber_encoded_scaled { get; set; }

        public double time_between_transactions_scaled { get; set; }

        public double frequecy_per_hour_scaled { get; set; }

        public double Average_Amount { get; set; }

        public double Accumulated_Amount { get; set; }

        public double min { get; set; }

        public double max { get; set; }

        public double? Lat { get; set; }

        public double? Long { get; set; }

        public double? Distance_between_transactions { get; set; }

        public double? speed { get; set; }

        public double? Average_Speed { get; set; }

        public double VelocityByLocation { get; set; }

        public double StandardDeviation { get; set; }
        public double LocationName_encoded { get; set; }

        public bool is_HM_Fraud { get; set; }

        public bool is_IF_Fraud { get; set; }

        public bool is_RC_Fraud { get; set; }

        public bool is_TimeDiff_Fraud { get; set; }

        public bool is_UnusualHour_Fraud { get; set; }

        public bool is_Location_Fraud { get; set; }

        public bool PINError { get; set; }

        public bool isnot_Consistent { get; set; }

        public bool ExpiredCard { get; set; }

        public double NbrDeclinedTransctions { get; set; }

        public bool ManyDeclinedTransactions { get; set; }

        public bool isMultiplePINEntry { get; set; }

        public bool IsLateNightOrEarlyMorning { get; set; }

        public bool FrequencyChange { get; set; }

        public bool isRepeated { get; set; }

        public bool is_belongsToOrder { get; set; }

        public bool is_Fraud { get; set; }
    }
}
