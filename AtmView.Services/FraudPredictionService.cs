using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;


using Microsoft.ML;
using Microsoft.ML.Data;

using System.Collections;
using AtmView.Entities;
using AtmView.DAO.Common;

using AtmView;
using System.IO;
using log4net;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using System.Web.Configuration;
using System.Diagnostics;
using static iTextSharp.text.pdf.AcroFields;
using Microsoft.AspNet.SignalR.Transports;
using System.Text;


namespace AtmView.Services
{

    public class ModelInput
    {
        int size = 23;
        [VectorType(8)]
        [ColumnName("dense_input")]
        //[CustomVectorType]
        public double[] Features { get; set; }


    }


    public class Prediction
    {

        ILog ExceptionLogger = LogManager.GetLogger("ExceptionLogger");



        [VectorType(1)]
        [ColumnName("Output_Layer")]
        public float[] is_Fraud { get; set; }
        /*

        public void ChangeColumnName(string newColumnName)
        {
            var property = typeof(Prediction).GetProperty(nameof(is_Fraud));
            ExceptionLogger.Info("property?????????" + property.Name);
            var attribute = (ColumnNameAttribute)Attribute.GetCustomAttribute(property, typeof(ColumnNameAttribute));

            if (attribute != null)
            {
                ExceptionLogger.Info("attribute?????????"+attribute.);
            }
            else
            {
                ExceptionLogger.Info("ColumnName attribute not found on property.");
            }
        }*/

    }
    [Serializable]
    public class FraudPredictionService : GenericRepo<FPTrainingModel>, IFraudPredictionService
    {

        public FPCalculeFeatures _FPCalculeFeatures;
        public AtmViewContext db;
        public IAVTransactionService _AVTransactionService;
        public FPTrainingModelService _FPTrainingModelService;
        public IUnitOfWork _unitOfWork;
        ILog ExceptionLogger = LogManager.GetLogger("ExceptionLogger");
        public Prediction _predcction;

        public FraudPredictionService(FPCalculeFeatures __FPCalculeFeatures, AtmViewContext db, IAVTransactionService aVTransactionService, FPTrainingModelService FPTrainingModelService, IUnitOfWork unitOfWork, Prediction predcction) : base(db)
        {
            _FPCalculeFeatures = __FPCalculeFeatures;
            this.db = db;
            _AVTransactionService = aVTransactionService;
            _FPTrainingModelService = FPTrainingModelService;
            _unitOfWork = unitOfWork;
            _predcction = predcction;

        }



        /*public FraudPredictionService(IUnitOfWork unitOfWork, IGenericRepo<FPTrainingModel> repository) : base(unitOfWork, repository)
{
}*/



        public bool SetupMlnetModel(string tensorFlowModelFilePath, Fraud_Data data)
        {


            var modelPath = tensorFlowModelFilePath;
            var outputColumnNames = new string[] { "Output_Layer" };
            var inputColumnNames = new string[]
                    {
                    "isCashPresented_toInt",
                    "isCashTaken_toInt",
                    "isCashRetracted_toInt",
                    "isCashoutError_toInt",
                    "ExistInHost_toInt",
                    "Type_encoded_scaled",
                    "Amount_scaled",
                    "Statut_scaled",
                    "CardNumber_encoded_scaled",
                    "time_between_transactions_scaled",
                    "frequecy_per_hour_scaled",
                    "Average_Amount",
                    "Accumulated_Amount",
                    "min",
                    "max",
                    "Lat",
                    "Long",
                    "Distance_between_transactions",
                    "speed",
                    "Average_Speed",
                    "VelocityByLocation",
                    "StandardDeviation",
                    "LocationName_encoded",
                    "NbrDeclinedTransctions"
                    };
            //var inputColumnNames = new[] {"dense_input" };
            var mlContext = new MLContext();
            var pipeline = mlContext.Transforms.ApplyOnnxModel(outputColumnNames, inputColumnNames, modelPath);


            var dataView = mlContext.Data.LoadFromEnumerable(new[] { data });

            var transformedValues = pipeline.Fit(dataView);

            var test = transformedValues.Transform(dataView);

            var output = mlContext.Data.CreateEnumerable<FraudPrediction>(test, reuseRowObject: false);

            return Convert.ToBoolean(output.FirstOrDefault());
        }

        public (ITransformer, int) LoadModel(string modelLocation, MLContext mLContext)
        {
            ITransformer transformer = null;
            int ismodel = 0;

            //ModelInput modelinput= new ModelInput();
            // Create IDataView from empty list to obtain input data schema
            var data = mLContext.Data.LoadFromEnumerable(new List<ModelInput>());




            ExceptionLogger.Info(" im in LoadModel");
            try
            {
                var pipeline = mLContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { "Output_Layer" }, inputColumnNames: new[] { "dense_input" });
                ExceptionLogger.Info("load model  is done");
                ExceptionLogger.Info("fitting!!!!!!!!!!");
                var model = pipeline.Fit(data);
                ismodel = 1;
                return (model, ismodel);

            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("load model function " + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                ismodel = 0;
                return (transformer, ismodel);
            }
            // Define scoring pipeline

            // Fit scoring pipeline



        }

        public Prediction PredictDataUsingModel(ITransformer model, MLContext mlContext, ModelInput input)
        {
            ExceptionLogger.Info("I am in PredictDataUsingModel funtion");
            ExceptionLogger.Info("sampleStatement.Features.Length" + input.Features.Length);

            try
            {
                var predictEngine = mlContext.Model.CreatePredictionEngine<ModelInput, Prediction>(model);
                var predOutput = predictEngine.Predict(input);
                // Get the PropertyInfo object for the property

                ExceptionLogger.Info(" PredictDataUsingModel funtion is done" + predOutput.is_Fraud + "!!!!!!!!!!!!" + nameof(predOutput.is_Fraud) + "!!!!!!!!!!!!!!");
                return predOutput;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("predict data using model function " + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                return null;
            }

        }


        public Prediction test(ModelInput data)
        {
            Prediction bools = new Prediction();
            ExceptionLogger.Info("I am in test funtion");



            try
            {
                var mlContext = new MLContext();


                string modeltPath = WebConfigurationManager.AppSettings["ModelFile"];
                var aux = LoadModel(modeltPath, mlContext);
                int isModel = aux.Item2;
                if (isModel == 1)
                {
                    ExceptionLogger.Info("Model exist!!!!!!! and in good layer names " + isModel);
                    var model = aux.Item1;


                    bools.is_Fraud = new float[] { 0 };
                    if (model != null)
                        bools = PredictDataUsingModel(model, mlContext, data);
                }

                else
                {
                    ExceptionLogger.Info("wrong layer names!!!!!!!!!!" + isModel);
                    bools.is_Fraud = new float[] { 0 };
                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.Info(" test fct exeption" + ex);
            }

            ExceptionLogger.Info(" test fct funtion is done");




            return bools;


        }



        public bool Prediction(DataFrame dataFrame)
        {
            ExceptionLogger.Info("Im in Prediction Function");
            List<ModelInput> resultList = new List<ModelInput>();
            try
            {
                for (int i = 0; i < dataFrame.Columns[0].Length; i++)
                {
                    ModelInput modelInput = new ModelInput();

                    modelInput.Features = new double[]
                    {
                        (double)dataFrame["Type_encoded_scaled"][i],
                        (double)dataFrame["Statut_scaled"][i],
                        (double)dataFrame["CardNumber_encoded_scaled"][i],
                        (double)dataFrame["Distance_between_transactions"][i],
                        (double)dataFrame["is_HM_Fraud"][i],
                        (double)dataFrame["is_UnusualHour_Fraud"][i],
                        (double)dataFrame["isnot_Consistent"][i],
                        //(double)dataFrame["NbrDeclinedTransctions"][i],
                        (double)dataFrame["IsLateNightOrEarlyMorning"][i]

                    };

                    resultList.Add(modelInput);
                }
                ExceptionLogger.Info("Creating mOdelInput exception done");


            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Creating mOdelInput exception" + ex);
            }


            ModelInput sampleStatement = resultList[0];
            ExceptionLogger.Info("sampleStatement.Features.Length" + sampleStatement.Features.Length);

            Prediction resultPrediction = test(sampleStatement);
            float res = resultPrediction.is_Fraud[0];
            bool resu = Convert.ToBoolean((int)Math.Round(res));
            return resu;

        }



        public FraudPrediction PredictSingleResult(MLContext mlContext, ITransformer model, Fraud_Data input)
        {


            var predictEngine = mlContext.Model.CreatePredictionEngine<Fraud_Data, FraudPrediction>(model);
            var predOutput = predictEngine.Predict(input);
            return predOutput;
        }


        public (bool, string) Predict(AVTransaction Transaction)
        {


           
            StringBuilder cardNumberToSearch = new StringBuilder(Transaction.CardNumber.Trim('"')) ;

            //ExceptionLogger.Info("Predict function : cardNumber is:!!!!!!"+ cardNumberToSearch);
            if (cardNumberToSearch == null || cardNumberToSearch.Equals(""))
            {
                ExceptionLogger.Info("Predict function : cardNumber is:null ");
                return (false, "");
            }
            ExceptionLogger.Info("Predict function : cardNumber is not null ");

            //List<AVTransaction> trxbycard = db.AVTransactions.Where(t => t.CardNumber == cardNumberToSearch).ToList();
            AVTransaction trxbycard = db.AVTransactions
            .Where(t => t.CardNumber.Equals(cardNumberToSearch.ToString(), StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(t => t.TransactionDate)
            .FirstOrDefault();


            List<AVTransaction> all = new List<AVTransaction>();
            all.Add(Transaction);

            int rowsToSkip = 0;
            if (trxbycard != null)
            {
                rowsToSkip = 1;
                all.Add(trxbycard);
            }




            //all.Add(Transaction);


            DataFrame trx = new DataFrame();

            trx.Columns.Add(new StringDataFrameColumn("AtmID", all.Select(entry => entry.AtmID)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<DateTime>("TransactionDate", all.Select(entry => entry.TransactionDate)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("TransactionNumber", all.Select(entry => entry.TransactionNumber)));
            trx.Columns.Add(new StringDataFrameColumn("CardNumber", all.Select(entry => entry.CardNumber)));
            trx.Columns.Add(new StringDataFrameColumn("Rib", all.Select(entry => entry.Rib)));
            trx.Columns.Add(new StringDataFrameColumn("Type", all.Select(entry => entry.Type)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("Amount", all.Select(entry => entry.Amount)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("Statut", all.Select(entry => entry.Statut)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashPresented", all.Select(entry => entry.isCashPresented)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashTaken", all.Select(entry => entry.isCashTaken)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashRetracted", all.Select(entry => entry.isCashRetracted)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashoutError", all.Select(entry => entry.isCashoutError)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("ExistInHost", all.Select(entry => entry.ExistInHost)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("IsRejected", all.Select(entry => entry.IsRejected)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("is_FraudP", all.Select(entry => entry.is_FraudP)));
            //ExceptionLogger.Info("function predict :conversion to dataframe is done ");
            trx = _FPCalculeFeatures.CalculeFeatures(trx);
            long cc = trx.Rows.Count;

            //ExceptionLogger.Info("function predict :feature calculation for the transaction is done");
            trx = trx.Head((int)(trx.Rows.Count - rowsToSkip));
            bool result = false;
            //ExceptionLogger.Info("data shape!!!!!!!!! columns"+trx.Columns.Count+"rows"+trx.Rows.Count);
            //ExceptionLogger.Info("valueeeeess!!!!!!!!!!!!!!!!!!!");
            /*foreach (var val in trx.Columns)
            {
                ExceptionLogger.Info(val);
            }*/

            //ExceptionLogger.Info("valueeeeess!!!!!!!!!!!!!!!!!!!");
            try
            {
                result = Prediction(trx);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("predicition " + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }




            /*string[] subTargets = { "is_HM_Fraud",
                                    "is_IF_Fraud",
                                    "is_RC_Fraud",
                                    "is_TimeDiff_Fraud",
                                    "is_UnusualHour_Fraud",
                                    "is_Location_Fraud",
                                    "PINError",
                                    "isnot_Consistent",
                                    "ExpiredCard",
                                    "ManyDeclinedTransactions",
                                    "isMultiplePINEntry",
                                    "IsLateNightOrEarlyMorning",
                                    "FrequencyChange",
                                    "isRepeated",
                                    "is_belongsToOrder"};*/


            string[] subTargets = { "isnot_Consistent",
                                    "isMultiplePINEntry",
                                    "isRepeated",
                                    "is_HM_Fraud",
                                    "ManyDeclinedTransactions",
                                    "is_RC_Fraud",
                                    "is_UnusualHour_Fraud",
                                    "is_Location_Fraud",
                                    "PINError",
                                    "ExpiredCard",
                                    "FrequencyChange",
                                    "is_TimeDiff_Fraud",
                                    "IsLateNightOrEarlyMorning",
                                    "is_IF_Fraud",
                                    "is_belongsToOrder"};


            string[] AllReasons = { "Suspected Time Distance Inconsistency",
                                    "Repeated PIN Attempts",
                                    "Duplicate Transactions",
                                    "High Amount",
                                    "Many Declined Transactions",
                                    "Invalid Response code",
                                    "Unexpected Transaction Time",
                                    "Suspected Transaction Location",
                                    "PIN Error",
                                    "Expired Card",
                                    "Unusual Frequency",
                                    "Irregular Time Difference",
                                    "Late Night Or Early Morning",
                                    "Low Account Balance",
                                    "Suspected TransactionOrder"};


            List<string> Reasons = new List<string>();
            string FraudReasons = "No Fraud Detected";


            if (result == true)
            {
                int i = 0;
                foreach (string R in subTargets)
                {
                    try
                    {
                        //ExceptionLogger.Info("Value issss:"+ trx.Columns[R][0]+trx.Columns[R][0].GetType());
                        double val = (double)trx.Columns[R][0];
                        if (val == 1)
                        {
                            Reasons.Add(AllReasons[i]);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Info("Error in Fraud Reason:" + ex.Message + ex.InnerException.Message);
                    }
                    i++;
                }
                FraudReasons = string.Join(", ", Reasons);
            }

            //ExceptionLogger.Info("FraudReasons are :!!!!!" + FraudReasons+"!!!!fin");







            try
            {
                FPTrainingModel TransactionModifiee = new FPTrainingModel
                {
                    AtmID = Convert.ToString(trx.Columns["AtmID"][0]),
                    TransactionDate = Convert.ToDateTime(trx.Columns["TransactionDate"][0]),
                    TransactionNumber = Convert.ToInt32(trx.Columns["TransactionNumber"][0]),
                    CardNumber = Convert.ToString(trx.Columns["CardNumber"][0]),
                    isCashPresented_toInt = Convert.ToDouble(trx.Columns["isCashPresented_tofloat"][0]),
                    isCashTaken_toInt = Convert.ToDouble(trx.Columns["isCashTaken_tofloat"][0]),
                    isCashRetracted_toInt = Convert.ToDouble(trx.Columns["isCashRetracted_tofloat"][0]),
                    isCashoutError_toInt = Convert.ToDouble(trx.Columns["isCashoutError_tofloat"][0]),
                    ExistInHost_toInt = Convert.ToDouble(trx.Columns["ExistInHost_tofloat"][0]),
                    Type_encoded_scaled = Convert.ToDouble(trx.Columns["Type_encoded_scaled"][0]),
                    Amount_scaled = Convert.ToDouble(trx.Columns["Amount_scaled"][0]),
                    Statut_scaled = Convert.ToDouble(trx.Columns["Statut_scaled"][0]),
                    CardNumber_encoded_scaled = Convert.ToDouble(trx.Columns["CardNumber_encoded_scaled"][0]),
                    time_between_transactions_scaled = Convert.ToDouble(trx.Columns["time_between_transactions_scaled"][0]),
                    frequecy_per_hour_scaled = Convert.ToDouble(trx.Columns["frequecy_per_hour_scaled"][0]),
                    Average_Amount = Convert.ToDouble(trx.Columns["Average_Amount"][0]),
                    Accumulated_Amount = Convert.ToDouble(trx.Columns["Accumulated_Amount"][0]),
                    min = Convert.ToDouble(trx.Columns["min"][0]),
                    max = Convert.ToDouble(trx.Columns["max"][0]),
                    Lat = Convert.ToDouble(trx.Columns["Lat"][0]),
                    Long = Convert.ToDouble(trx.Columns["Long"][0]),
                    Distance_between_transactions = Convert.ToDouble(trx.Columns["Distance_between_transactions"][0]),
                    speed = Convert.ToDouble(trx.Columns["speed"][0]),
                    Average_Speed = Convert.ToDouble(trx.Columns["Average_Speed"][0]),
                    VelocityByLocation = Convert.ToDouble(trx.Columns["VelocityByLocation"][0]),
                    StandardDeviation = Convert.ToDouble(trx.Columns["StandardDeviation"][0]),
                    LocationName_encoded = Convert.ToDouble(trx.Columns["LocationName_encoded"][0]),
                    is_HM_Fraud = Convert.ToDouble(trx.Columns["is_HM_Fraud"][0]),
                    is_IF_Fraud = Convert.ToDouble(trx.Columns["is_IF_Fraud"][0]),
                    is_RC_Fraud = Convert.ToDouble(trx.Columns["is_RC_Fraud"][0]),
                    is_TimeDiff_Fraud = Convert.ToDouble(trx.Columns["is_TimeDiff_Fraud"][0]),
                    is_UnusualHour_Fraud = Convert.ToDouble(trx.Columns["is_UnusualHour_Fraud"][0]),
                    is_Location_Fraud = Convert.ToDouble(trx.Columns["is_Location_Fraud"][0]),
                    PINError = Convert.ToDouble(trx.Columns["PINError"][0]),
                    isnot_Consistent = Convert.ToDouble(trx.Columns["isnot_Consistent"][0]),
                    ExpiredCard = Convert.ToDouble(trx.Columns["ExpiredCard"][0]),
                    NbrDeclinedTransctions = Convert.ToDouble(trx.Columns["NbrDeclinedTransctions"][0]),
                    ManyDeclinedTransactions = Convert.ToDouble(trx.Columns["ManyDeclinedTransactions"][0]),
                    isMultiplePINEntry = Convert.ToDouble(trx.Columns["isMultiplePINEntry"][0]),
                    IsLateNightOrEarlyMorning = Convert.ToDouble(trx.Columns["IsLateNightOrEarlyMorning"][0]),
                    FrequencyChange = Convert.ToDouble(trx.Columns["FrequencyChange"][0]),
                    isRepeated = Convert.ToDouble(trx.Columns["isRepeated"][0]),
                    is_belongsToOrder = Convert.ToDouble(trx.Columns["is_belongsToOrder"][0]),
                    is_Fraud = Convert.ToBoolean(trx.Columns["is_Fraud"][0])
                };


                ExceptionLogger.Info("AtmID: " + TransactionModifiee.AtmID + "Transdate: " + TransactionModifiee.TransactionDate + "TransNumber: " + TransactionModifiee.TransactionNumber + "***********************************");

                Create(TransactionModifiee);
                int lastProcessedIndex = LoadCheckpoint() + 1;
                SaveCheckpoint(lastProcessedIndex);
                ExceptionLogger.Info("function predict :transaction saved successfully");

                TransactionModifiee = null;

            }
            catch (Exception ex)
            {
                // Log the exception details
                ExceptionLogger.Info("Error:" + ex.Message);

                // Check for inner exception
                if (ex.InnerException != null)
                {
                    // Log inner exception details
                    ExceptionLogger.Info(ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                }

                // Re-throw the exception or handle it appropriately
                throw;
            }





            trx = null;
            trxbycard = null;
            cardNumberToSearch = null;
            all = null;



            return (result, FraudReasons);
        }

        public string HistoryPred()
        {
            string Output;
            string Modelpath = WebConfigurationManager.AppSettings["ModelFile"];
            if (System.IO.File.Exists(Modelpath))
            {
                string[] subTargets = { "isnot_Consistent",
                                    "isMultiplePINEntry",
                                    "isRepeated",
                                    "is_HM_Fraud",
                                    "ManyDeclinedTransactions",
                                    "is_RC_Fraud",
                                    "is_UnusualHour_Fraud",
                                    "is_Location_Fraud",
                                    "PINError",
                                    "ExpiredCard",
                                    "FrequencyChange",
                                    "is_TimeDiff_Fraud",
                                    "IsLateNightOrEarlyMorning",
                                    "is_IF_Fraud",
                                    "is_belongsToOrder"};


                string[] AllReasons = { "Suspected Time Distance Inconsistency",
                                    "Repeated PIN Attempts",
                                    "Duplicate Transactions",
                                    "High Amount",
                                    "Many Declined Transactions",
                                    "Invalid Response code",
                                    "Unexpected Transaction Time",
                                    "Suspected Transaction Location",
                                    "PIN Error",
                                    "Expired Card",
                                    "Unusual Frequency",
                                    "Irregular Time Difference",
                                    "Late Night Or Early Morning",
                                    "Low Account Balance",
                                    "Suspected TransactionOrder"};

                int count = db.FPTrainingData.Count();



                for (int i = 0; i < count; i++)
                {
                    FPTrainingModel Trx = db.FPTrainingData.OrderByDescending(t => t.TransactionDate).Skip(i).FirstOrDefault();
                    //FPTrainingModel Trx = db.FPTrainingData.OrderByDescending(t => t.TransactionDate).Where(t => t.CardNumber.Equals("4393******3163")).Skip(70).FirstOrDefault();
                    ExceptionLogger.Info("Im in Prediction Function, this is the trx = " + Trx.AtmID + " " + Trx.CardNumber + " " + Trx.TransactionDate);

                    ModelInput modelInput = new ModelInput();
                    try
                    {
                        modelInput.Features = new double[]
                        {
                        (double)Trx.Type_encoded_scaled,
                        (double)Trx.Statut_scaled,
                        (double)Trx.CardNumber_encoded_scaled,
                        (double)Trx.Distance_between_transactions,
                        (double)Trx.is_HM_Fraud,
                        (double)Trx.is_UnusualHour_Fraud,
                        (double)Trx.isnot_Consistent,
                        //(double)dataFrame["NbrDeclinedTransctions"][i],
                        (double)Trx.IsLateNightOrEarlyMorning

                        };
                        //ExceptionLogger.Info("Creating mOdelInput done");


                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Info("Creating mOdelInput exception" + ex);
                    }


                    //ModelInput sampleStatement = resultList[0];
                    //ExceptionLogger.Info("sampleStatement.Features.Length" + modelInput.Features.Length);

                    Prediction resultPrediction = test(modelInput);
                    float res = resultPrediction.is_Fraud[0];
                    bool resu = Convert.ToBoolean((int)Math.Round(res));
                    List<string> Reasons = new List<string>();
                    string FraudReasons = "No Fraud Detected";

                    //resu = true;
                    if (resu == true)
                    {
                        int j = 0;
                        foreach (string R in subTargets)
                        {
                            try
                            {
                                var propertyInfo = typeof(FPTrainingModel).GetProperty(R);
                                if (propertyInfo != null)
                                {
                                    object propertyValue = propertyInfo.GetValue(Trx);
                                    //ExceptionLogger.Info(propertyValue);
                                    if ((double)propertyValue == 1)
                                    {
                                        Reasons.Add(AllReasons[j]); // Add the property name to Reasons list
                                                                    //ExceptionLogger.Info("the reason is:" + R + "andthe value is :" + propertyValue);
                                    }
                                }


                            }
                            catch (Exception ex)
                            {
                                ExceptionLogger.Info("Error in Fraud Reason:" + ex.Message + ex.InnerException.Message);
                            }
                            j++;
                        }
                        FraudReasons = string.Join(", ", Reasons);
                    }




                    AVTransaction AvTrx = db.AVTransactions.Where(t => t.AtmID == Trx.AtmID && t.TransactionNumber == Trx.TransactionNumber && t.TransactionDate == Trx.TransactionDate).FirstOrDefault();

                    AvTrx.is_FraudP = resu;
                    AvTrx.FraudReason = FraudReasons;

                    try
                    {
                        // Mark the property is_FraudP as modified in the Entity Entry
                        db.Entry(AvTrx).Property(x => x.is_FraudP).IsModified = true;
                        db.Entry(AvTrx).Property(x => x.FraudReason).IsModified = true;

                        // Save changes to the database
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.Info(ex.Message);
                    }

                    
                    //ExceptionLogger.Info("Result of prediction" + resu);


                }

                Output = "HistoryPredcition is done";


            }
            else
            {
                ExceptionLogger.Info("Couldnt predict Historydata : Model hasnt trained yet");

                Output = "Couldnt predict Historydata : Model hasnt trained yet";
            }


            return Output;


        }


        public virtual void Create(FPTrainingModel entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                Add(entity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {

                ExceptionLogger.Info("Error during entity creation:" + ex.Message);


                if (ex.InnerException != null)
                {
                    ExceptionLogger.Info(ex.InnerException + "Inner Exception: {Message}" + ex.InnerException.Message);
                }

                throw;
            }
        }



        public List<FPTrainingModel> ConvertDataFrameToList(DataFrame dataFrame)
        {
            List<FPTrainingModel> entities = new List<FPTrainingModel>();

            Parallel.ForEach(dataFrame.Rows, row =>
            {
                FPTrainingModel entity = new FPTrainingModel
                {
                    AtmID = Convert.ToString(row[0]),
                    TransactionDate = Convert.ToDateTime(row[1]),
                    TransactionNumber = Convert.ToInt32(row[2]),
                    CardNumber = Convert.ToString(row[3]),
                    isCashPresented_toInt = Convert.ToDouble(row[5]),
                    isCashTaken_toInt = Convert.ToDouble(row[6]),
                    isCashRetracted_toInt = Convert.ToDouble(row[7]),
                    isCashoutError_toInt = Convert.ToDouble(row[8]),
                    ExistInHost_toInt = Convert.ToDouble(row[9]),
                    Type_encoded_scaled = Convert.ToDouble(row[10]),
                    Amount_scaled = Convert.ToDouble(row[11]),
                    Statut_scaled = Convert.ToDouble(row[12]),
                    CardNumber_encoded_scaled = Convert.ToDouble(row[13]),
                    time_between_transactions_scaled = Convert.ToDouble(row[14]),
                    frequecy_per_hour_scaled = Convert.ToDouble(row[15]),
                    //is_Fraud = Convert.ToBoolean(row[16]),
                    Average_Amount = Convert.ToDouble(row[16]),
                    Accumulated_Amount = Convert.ToDouble(row[17]),
                    min = Convert.ToDouble(row[18]),
                    max = Convert.ToDouble(row[19]),
                    Lat = Convert.ToDouble(row[20]),
                    Long = Convert.ToDouble(row[21]),
                    Distance_between_transactions = Convert.ToDouble(row[22]),
                    speed = Convert.ToDouble(row[23]),
                    Average_Speed = Convert.ToDouble(row[24]),
                    VelocityByLocation = Convert.ToDouble(row[25]),
                    StandardDeviation = Convert.ToDouble(row[26]),
                    LocationName_encoded = Convert.ToDouble(row[27]),
                    is_HM_Fraud = Convert.ToDouble(row[28]),
                    is_IF_Fraud = Convert.ToDouble(row[29]),
                    is_RC_Fraud = Convert.ToDouble(row[30]),
                    is_TimeDiff_Fraud = Convert.ToDouble(row[31]),
                    is_UnusualHour_Fraud = Convert.ToDouble(row[32]),
                    is_Location_Fraud = Convert.ToDouble(row[33]),
                    PINError = Convert.ToDouble(row[34]),
                    isnot_Consistent = Convert.ToDouble(row[35]),
                    ExpiredCard = Convert.ToDouble(row[36]),
                    NbrDeclinedTransctions = Convert.ToDouble(row[37]),
                    ManyDeclinedTransactions = Convert.ToDouble(row[38]),
                    isMultiplePINEntry = Convert.ToDouble(row[39]),
                    IsLateNightOrEarlyMorning = Convert.ToDouble(row[40]),
                    FrequencyChange = Convert.ToDouble(row[41]),
                    isRepeated = Convert.ToDouble(row[42]),
                    is_belongsToOrder = Convert.ToDouble(row[43]),
                    is_Fraud = Convert.ToBoolean(row[44]),
                };


                lock (entities)
                {
                    entities.Add(entity);
                }
            });

            return entities;
        }





        public void calculateChunks(List<AVTransaction> all)
        {

            DataFrame trx = new DataFrame();

            trx.Columns.Add(new StringDataFrameColumn("AtmID", all.Select(entry => entry.AtmID)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<DateTime>("TransactionDate", all.Select(entry => entry.TransactionDate)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("TransactionNumber", all.Select(entry => entry.TransactionNumber)));
            trx.Columns.Add(new StringDataFrameColumn("CardNumber", all.Select(entry => entry.CardNumber)));
            //trx.Columns.Add(new StringDataFrameColumn("Rib", all.Select(entry => entry.Rib)));
            trx.Columns.Add(new StringDataFrameColumn("Type", all.Select(entry => entry.Type)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("Amount", all.Select(entry => entry.Amount)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<int>("Statut", all.Select(entry => entry.Statut)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashPresented", all.Select(entry => entry.isCashPresented)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashTaken", all.Select(entry => entry.isCashTaken)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashRetracted", all.Select(entry => entry.isCashRetracted)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashoutError", all.Select(entry => entry.isCashoutError)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("ExistInHost", all.Select(entry => entry.ExistInHost)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("IsRejected", all.Select(entry => entry.IsRejected)));
            trx.Columns.Add(new PrimitiveDataFrameColumn<bool>("is_FraudP", all.Select(entry => entry.is_FraudP)));
            //ExceptionLogger.Info("function calculateChunks :conversion to dataframe is done ");
            //trx.Columns.Remove("is_FraudP");
            DataFrame data = new DataFrame();
            try
            {
                data = _FPCalculeFeatures.CalculeFeatures(trx);
                //ExceptionLogger.Info("function calculateChunks :feature calculation forthe chunk is done");

            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("caculate features in calculate chunks" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }


            List<string> cols = new List<string>();
            foreach (var col in data.Columns) cols.Add(col.Name);

            try
            {
                List<FPTrainingModel> Transactions = ConvertDataFrameToList(data);
                //ExceptionLogger.Info("function calculateChunks :conversion to FPTrainingModel transaction class is done");
                db.FPTrainingData.AddRange(Transactions);
                db.SaveChanges();
                //ExceptionLogger.Info("function calculateChunks :save is done");

            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("saving data" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }





            all = null;
            trx = null;
            data = null;
            //GC.Collect();

        }




        public string calculeAll()
        {
            db.Database.CommandTimeout = 10000;
            int lastProcessedIndex = LoadCheckpoint();
            ExceptionLogger.Info("loading from txt file");
            List<AVTransaction> all = new List<AVTransaction>();


            try
            {
                all = _AVTransactionService.GetAll(t => !string.IsNullOrEmpty(t.CardNumber) && System.Data.Entity.SqlServer.SqlFunctions.PatIndex("[0-9][0-9][0-9][0-9]%[0-9][0-9][0-9][0-9]", t.CardNumber) > 0).Select(t => new AVTransaction
                {
                    AtmID = t.AtmID,
                    TransactionDate = t.TransactionDate,
                    TransactionNumber = t.TransactionNumber,
                    CardNumber = t.CardNumber,
                    Type = t.Type,
                    Amount = t.Amount,
                    Statut = t.Statut,
                    isCashPresented = t.isCashPresented,
                    isCashTaken = t.isCashTaken,
                    isCashRetracted = t.isCashRetracted,
                    isCashoutError = t.isCashoutError,
                    ExistInHost = t.ExistInHost,
                    IsRejected = t.IsRejected,
                    //is_FraudP = t.is_FraudP
                }).OrderByDescending(t => t.TransactionDate).Skip(lastProcessedIndex).ToList();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("importing data" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }

            //ExceptionLogger.Info("data imported succesfully!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            int count = all.Count;
            int batch = 80000;
            /*try
            {
               
                if (count < batch) batch = count / 10;
                ExceptionLogger.Info("batch iszzzzzzzzzzzzzzzzz" + batch);
            }
            catch(Exception ex)
            {
                ExceptionLogger.Info("importing data" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }*/

            int lastSuccessfullyProcessedIndex = lastProcessedIndex;

            for (int i = 0; i < count; i += batch)
            {

                int rowsToTake = Math.Min(batch, count - i);

                try
                {
                    calculateChunks(all.Skip(i).Take(rowsToTake).ToList());
                    //ExceptionLogger.Info("chunk" + i + "is calculated  !!!!!!!!!!!!!!!!!!!!");
                    lastSuccessfullyProcessedIndex = lastProcessedIndex + i + rowsToTake;
                    SaveCheckpoint(lastSuccessfullyProcessedIndex);
                    //ExceptionLogger.Info("last index is saved !!!!!!!!!!!!!!!!!!!!!!!!!");
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Info("calculating features" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                    SaveCheckpoint(lastSuccessfullyProcessedIndex);
                    all = null;
                    throw;
                }



            }


            all = null;
            ExceptionLogger.Info("the whole calculating feature is completed!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return "done";
        }


        private void SaveCheckpoint(int index)
        {

            string checkpointPath = WebConfigurationManager.AppSettings["checkpointFile"];

            ExceptionLogger.Info("path od saving is heeeeeere" + checkpointPath);
            File.WriteAllText(checkpointPath, index.ToString());

        }

        private int LoadCheckpoint()
        {
            try
            {

                //string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;


                //string checkpointPath = Path.Combine(currentDirectory, "checkpoint.txt");
                string checkpointPath = WebConfigurationManager.AppSettings["checkpointFile"];
                ExceptionLogger.Info("path of loading is heeeeeere" + checkpointPath);
                try
                {
                    string checkpointText = File.ReadAllText(checkpointPath);
                    return int.Parse(checkpointText);

                }
                catch (Exception ex)
                {
                    ExceptionLogger.Info(ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                }
                return 0;


            }
            catch (FileNotFoundException)
            {

                return 0;
            }
        }


        public void train_model()
        {
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Training.py");


            string path = WebConfigurationManager.AppSettings["TrainingFile"];
            string modelsavePath = WebConfigurationManager.AppSettings["SAVEModel"];
            string pythonInterpreter = WebConfigurationManager.AppSettings["PythonPath"];
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AVAtmViewContextD"].ConnectionString;
            ExceptionLogger.Info("loading paths!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            ExceptionLogger.Info("connectionString" + connectionString);
            ExceptionLogger.Info("pythonInterpreter" + pythonInterpreter);
            ExceptionLogger.Info("modelsavePath" + modelsavePath);
            ExceptionLogger.Info("path to tarining.py" + path);



            string arguments = $"\"{path}\"  \"{modelsavePath}\" \"{connectionString}\"";
            ExceptionLogger.Info("test1");
            Process process = new Process();
            try
            {


                process.StartInfo.FileName = pythonInterpreter;
                ExceptionLogger.Info("test2");
                process.StartInfo.Arguments = arguments;
                ExceptionLogger.Info("test3");
                process.StartInfo.UseShellExecute = false;
                ExceptionLogger.Info("test4");
                process.StartInfo.RedirectStandardOutput = true;
                ExceptionLogger.Info("test5");
                process.StartInfo.RedirectStandardError = true;
                ExceptionLogger.Info("test6");
                process.StartInfo.CreateNoWindow = true;
                ExceptionLogger.Info("test7");


                process.Start();
                ExceptionLogger.Info("starting python  process !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("python process exeception !!!!" + ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
            }


            string output = null;
            string error = null;
            ExceptionLogger.Info("test8");
            try
            {
                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                ExceptionLogger.Info("getting output !!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            }

            catch (Exception ex)
            {
                ExceptionLogger.Info(ex.InnerException + "Inner erroor : {Message}" + ex.InnerException.Message);
                ExceptionLogger.Info("process error!!!!!!!!" + error);
            }

            ExceptionLogger.Info("affichage output !!!!!!!!!!!!!!!!!!!!!!!!!!!!" + output);



        }





        /*public List<string> average()
        {
            List<AVTransaction> test = db.AVTransactions.Where(t => t.CardNumber == "4393******9999").Take(100).ToList();
            DataFrame data = new DataFrame();

            data.Columns.Add(new StringDataFrameColumn("AtmID", test.Select(entry => entry.AtmID)));
            data.Columns.Add(new PrimitiveDataFrameColumn<DateTime>("TransactionDate", test.Select(entry => entry.TransactionDate)));
            data.Columns.Add(new PrimitiveDataFrameColumn<int>("TransactionNumber", test.Select(entry => entry.TransactionNumber)));
            data.Columns.Add(new StringDataFrameColumn("CardNumber", test.Select(entry => entry.CardNumber)));
            data.Columns.Add(new StringDataFrameColumn("Rib", test.Select(entry => entry.Rib)));
            data.Columns.Add(new StringDataFrameColumn("Type", test.Select(entry => entry.Type)));
            data.Columns.Add(new PrimitiveDataFrameColumn<int>("Amount", test.Select(entry => entry.Amount)));
            data.Columns.Add(new PrimitiveDataFrameColumn<int>("Statut", test.Select(entry => entry.Statut)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashPresented", test.Select(entry => entry.isCashPresented)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashTaken", test.Select(entry => entry.isCashTaken)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashRetracted", test.Select(entry => entry.isCashRetracted)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("isCashoutError", test.Select(entry => entry.isCashoutError)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("ExistInHost", test.Select(entry => entry.ExistInHost)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("IsRejected", test.Select(entry => entry.IsRejected)));
            data.Columns.Add(new PrimitiveDataFrameColumn<bool>("is_FraudP", test.Select(entry => entry.is_FraudP)));






            data = _FPCalculeFeatures.CalculeFeatures(data);




            List<string> Columns = new List<string>();
            foreach (var col in data.Columns)
            {
                Columns.Add("column: " + col.Name + ",Type: " + col.DataType);

            }
            List<bool> is_Fraud = new List<bool>();
            foreach (var val4 in data.Columns["is_Fraud"])
            {
                is_Fraud.Add((bool)val4);
            }

            return Columns;
        }*/



    }




    public class Fraud_Data
    {
        [LoadColumn(0)]
        public int isCashPresented_toInt { get; set; }

        [LoadColumn(1)]
        public int isCashTaken_toInt { get; set; }

        [LoadColumn(2)]
        public int isCashRetracted_toInt { get; set; }

        [LoadColumn(3)]
        public int isCashoutError_toInt { get; set; }

        [LoadColumn(4)]
        public int ExistInHost_toInt { get; set; }

        [LoadColumn(5)]
        public double Type_encoded_scaled { get; set; }

        [LoadColumn(6)]
        public double Amount_scaled { get; set; }

        [LoadColumn(7)]
        public double Statut_scaled { get; set; }

        [LoadColumn(8)]
        public double CardNumber_encoded_scaled { get; set; }

        [LoadColumn(9)]
        public double time_between_transactions_scaled { get; set; }

        [LoadColumn(10)]
        public double frequecy_per_hour_scaled { get; set; }

        [LoadColumn(11)]
        public double Average_Amount { get; set; }

        [LoadColumn(12)]
        public double Accumulated_Amount { get; set; }

        [LoadColumn(13)]
        public double min { get; set; }

        [LoadColumn(14)]
        public double max { get; set; }

        [LoadColumn(15)]
        public double Lat { get; set; }

        [LoadColumn(16)]
        public double Long { get; set; }


        [LoadColumn(17)]
        public double Distance_between_transactions { get; set; }

        [LoadColumn(18)]
        public double speed { get; set; }

        [LoadColumn(19)]
        public double Average_Speed { get; set; }

        [LoadColumn(20)]
        public double VelocityByLocation { get; set; }

        [LoadColumn(21)]
        public double StandardDeviation { get; set; }
        [LoadColumn(22)]
        public int LocationName_encoded { get; set; }


        [LoadColumn(23)]
        public bool is_HM_Fraud { get; set; }

        [LoadColumn(24)]
        public bool is_IF_Fraud { get; set; }

        [LoadColumn(25)]
        public bool is_RC_Fraud { get; set; }

        [LoadColumn(26)]
        public bool is_TimeDiff_Fraud { get; set; }

        [LoadColumn(27)]
        public bool is_UnusualHour_Fraud { get; set; }

        [LoadColumn(28)]
        public bool is_Location_Fraud { get; set; }

        [LoadColumn(29)]
        public bool PINError { get; set; }

        [LoadColumn(30)]
        public bool isnot_Consistent { get; set; }

        [LoadColumn(31)]
        public bool ExpiredCard { get; set; }

        [LoadColumn(32)]
        public double NbrDeclinedTransctions { get; set; }

        [LoadColumn(33)]
        public bool ManyDeclinedTransactions { get; set; }

        [LoadColumn(34)]
        public bool isMultiplePINEntry { get; set; }

        [LoadColumn(35)]
        public bool IsLateNightOrEarlyMorning { get; set; }

        [LoadColumn(36)]
        public bool FrequencyChange { get; set; }

        [LoadColumn(37)]
        public bool isRepeated { get; set; }

        [LoadColumn(38)]
        public bool is_belongsToOrder { get; set; }

        [LoadColumn(39)]
        public bool is_Fraud { get; set; }
    }


    public class FraudPrediction : Fraud_Data
    {

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        [ColumnName("Probability")]
        public float Probability { get; set; }
    }





    // RowData class is public
    public class RowData : IEnumerable<KeyValuePair<string, object>>
    {
        private Dictionary<string, object> columnData = new Dictionary<string, object>();

        public void AddColumnData(string columnName, object columnValue)
        {
            columnData.Add(columnName, columnValue);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return columnData.GetEnumerator();
        }

        public object GetValue(string columnName)
        {
            if (columnData.TryGetValue(columnName, out var value))
            {
                return value;
            }

            return null; // or throw an exception if needed
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ML_param
    {
        public ITransformer model;

        public MLContext mlcontext;
    }


    public class ModelTuningResult
    {
        public string AlgorithmName { get; set; }
        public double Accuracy { get; set; }
    }









    public class DataFrameDataReader : IDataReader
    {
        private readonly DataFrame _data;
        private int _rowIndex = -1;

        public DataFrameDataReader(DataFrame data)
        {
            _data = data;
        }

        public void Close()
        {
            // No need to implement for this simplified example
        }

        public void Dispose()
        {
            // No need to implement for this simplified example
        }

        public bool Read()
        {
            if (_rowIndex < _data.Rows.Count - 1)
            {
                _rowIndex++;
                return true;
            }
            return false;
        }

        public int FieldCount
        {
            get
            {
                return _data.Columns.Count;
            }
        }

        int IDataReader.Depth => throw new NotImplementedException();

        bool IDataReader.IsClosed => throw new NotImplementedException();

        int IDataReader.RecordsAffected => throw new NotImplementedException();

        object IDataRecord.this[string name] => throw new NotImplementedException();

        object IDataRecord.this[int i] => throw new NotImplementedException();

        public bool IsDBNull(int i)
        {
            return _data[_rowIndex, i] == null;
        }

        public int GetInt32(int i)
        {
            return (int)_data[_rowIndex, i];
        }

        public object GetValue(int i)
        {
            return _data[_rowIndex, i];
        }

        DataTable IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        bool IDataReader.NextResult()
        {
            throw new NotImplementedException();
        }

        bool IDataRecord.GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        byte IDataRecord.GetByte(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        double IDataRecord.GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        Type IDataRecord.GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        float IDataRecord.GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        Guid IDataRecord.GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        short IDataRecord.GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        long IDataRecord.GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetName(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        string IDataRecord.GetString(int i)
        {
            throw new NotImplementedException();
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        // Implement other methods and properties as needed

        // ...

        // Implement the methods and properties required by the IDataReader interface here.
    }

}
