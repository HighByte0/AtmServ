using AtmView.DAO.Common;
using AtmView.Entities;
using Microsoft.Data.Analysis;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmView.Services
{
    public interface IFraudPredictionService //: IEntiteService<FPTrainingModel>
    {

        /*DataFrame DataSorting(DataFrame avtransactions);
        DataFrame Time_between_trans(DataFrame data);
        DataFrame Velocity_per_hour(DataFrame data);
        DataFrame AddRandomPINAttempts(DataFrame data);
        DataFrame Cvm_methods(DataFrame data);
        DataFrame IssuerCity(DataFrame data);
        DataFrame Experation_date(DataFrame data);
        DataFrame Scale_exp(DataFrame data);
        DataFrame Label_Encoding(DataFrame data, string columnName);
        DataFrame Data_scalingI(DataFrame data, string col);
        DataFrame Data_scalingD(DataFrame data, string col);
        DataFrame Convert_boolToInt(DataFrame data, string col);

        //##########################################################################


        DataFrame is_PIN_Fraud(DataFrame data);
        DataFrame is_RC_Fraud(DataFrame data);
        DataFrame is_IF_Fraud(DataFrame data);
        DataFrame is_CVM_Fraud(DataFrame data);
        DataFrame is_expDate_Fraud(DataFrame data);
        DataFrame is_HM_Fraud(DataFrame data);
        DataFrame IsFraud(DataFrame data);*/


        //##########################################################################
        /*
        void Create_table(string tableName, string connectionString);
        void Delete_table(string tableName, string connectionString);
        void BulkCopyDataFrame(DataFrame data, string tableName, string connectionString);
        void SaveData(DataFrame training_data, string TableName, string ConnectionString);
        void InsertPredictedValue(string ConnectionString, string cardNumber, string dateString, string columnName, object columnValue);
        */
        //##########################################################################

        //List<string> ML_model(DataFrame tarining_data);      
        bool Prediction(DataFrame dataFrame);
        //(ITransformer, List<string>) Hyper_paramTuner(MLContext mlContext, IDataView TrainSet, IDataView TestSet);
        (bool, string) Predict(AVTransaction transaction);
        void train_model();
        string calculeAll();
        //List<string> average();
        string HistoryPred();

        //FraudPrediction test();
        //##########################################################################
        /* List<RowData> GetTransasction(string connectionString, string searchValue, string columnName);
         DataFrame SearchTransasction(string connectionString, string valuesearch, string columnname);
         DataFrame map(List<RowData> rows);
         DataFrame FindTransaction(DataFrame SearchTran, string ConnectionString, string cardNumber, string dateString);*/
    }
}
