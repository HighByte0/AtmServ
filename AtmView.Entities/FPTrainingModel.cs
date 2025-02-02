using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Entities
{
    [Table("TrainData2")]
    public class FPTrainingModel : Entite
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

        public double is_HM_Fraud { get; set; }

        public double is_IF_Fraud { get; set; }

        public double is_RC_Fraud { get; set; }

        public double is_TimeDiff_Fraud { get; set; }

        public double is_UnusualHour_Fraud { get; set; }

        public double is_Location_Fraud { get; set; }

        public double PINError { get; set; }

        public double isnot_Consistent { get; set; }

        public double ExpiredCard { get; set; }

        public double NbrDeclinedTransctions { get; set; }

        public double ManyDeclinedTransactions { get; set; }

        public double isMultiplePINEntry { get; set; }

        public double IsLateNightOrEarlyMorning { get; set; }

        public double FrequencyChange { get; set; }

        public double isRepeated { get; set; }

        public double is_belongsToOrder { get; set; }

        public bool is_Fraud { get; set; }
    }
}
