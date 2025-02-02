using Microsoft.Data.Analysis;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers.LightGbm;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ML.DataOperationsCatalog;
using AtmView.Common;
using AtmView.Entities;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace AtmView.Services
{

    public class FPTrainingModelService : FPCalculeFeatures
    {
        AtmViewContext db;
        //public IDbSet<FPTrainingModel> _dbset;
        public FPTrainingModelService(AtmViewContext db):base(db)
        {
            this.db = db;
            //_dbset = dbset;
        }
        public List<string> ML_model(DataFrame tarining_data)
        {
            string res = "";
            MLContext mlContext = new MLContext();

            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(tarining_data, testFraction: 0.2);

            ITransformer model = BuildAndTrainModel(mlContext, splitDataView.TrainSet, splitDataView.TestSet).Item1;
            res = BuildAndTrainModel(mlContext, splitDataView.TrainSet, splitDataView.TestSet).Item2;
            //ITransformer model = Hyper_paramTuner(mlContext, splitDataView.TrainSet, splitDataView.TestSet);
            //new ML_param { model = model, mlcontext = mlContext };*/


            //ITransformer model = Hyper_paramTuner(mlContext, splitDataView.TrainSet, splitDataView.TestSet).Item1;
            //List<string> bestacc = Hyper_paramTuner(mlContext, splitDataView.TrainSet, splitDataView.TestSet).Item2;

            mlContext.Model.Save(model, null, "C:/Users/Atm-View/Desktop/atmviewv5 - Original/Models/model5.zip");


            return new List<string>() { res };
            //return (int)tarining_data.Rows.Count;

        }

        public (ITransformer, string) BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet, IDataView splitTestSet)
        {

            var numericColumns = new[] { "Type_encoded_scaled", "Amount_scaled", "Statut_scaled", "CardNumber_encoded_scaled", "time_between_transactions_scaled", "frequecy_per_hour_scaled", 
                                         "Average_Amount", "Accumulated_Amount", "min", "max", "Lat", "Long", "Distance_between_transactions", "speed", "Average_Speed", "VelocityByLocation", 
                                         "StandardDeviation", "NbrDeclinedTransctions" };
            var categoricalColumns = new[] { "isCashPresented_toInt", "isCashTaken_toInt", "isCashRetracted_toInt", "isCashoutError_toInt", "ExistInHost_toInt"};
            var numericConcatenatedData = mlContext.Transforms.Concatenate("NumericFeatures", numericColumns);
            var categoricalConcatenatedData = mlContext.Transforms.Concatenate("CategoricalFeatures", categoricalColumns);

            var convertedCategoricalData = mlContext.Transforms.Conversion.ConvertType("CategoricalFeatures", "CategoricalFeatures", DataKind.Double);
            var concatenatedData = numericConcatenatedData
                .Append(categoricalConcatenatedData)
                .Append(convertedCategoricalData)
                .Append(mlContext.Transforms.Concatenate("Features", "NumericFeatures", "CategoricalFeatures"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", "is_Fraud"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label"));


            var trainer1 = mlContext.BinaryClassification.Trainers
                .FastTree(labelColumnName: "is_Fraud", featureColumnName: "Features");
            var trainer2 = mlContext.BinaryClassification.Trainers
                .LbfgsLogisticRegression(labelColumnName: "is_Fraud", featureColumnName: "Features",optimizationTolerance: (float)1e-5);
            var trainer3 = mlContext.BinaryClassification.Trainers.LdSvm(labelColumnName: "is_Fraud", featureColumnName: "Features");

            var trainer = mlContext.BinaryClassification.Trainers.LightGbm(new LightGbmBinaryTrainer.Options()
            {
                NumberOfIterations = 100000,
                LearningRate = 0.2001066f,
                NumberOfLeaves = 7,
                MinimumExampleCountPerLeaf = 10,
                UseCategoricalSplit = false,
                HandleMissingValue = false,
                MinimumExampleCountPerGroup = 200,
                MaximumCategoricalSplitPointCount = 16,
                CategoricalSmoothing = 10,
                L2CategoricalRegularization = 5,
                Booster = new GradientBooster.Options() { L2Regularization = 1, L1Regularization = 0 },
                LabelColumnName = "is_Fraud",
                FeatureColumnName = "Features"
            });

            var trainer4 = mlContext.BinaryClassification.Trainers.FastTree(new FastTreeBinaryTrainer.Options
            {
                NumberOfLeaves = 10,
                NumberOfTrees = 50,
                LabelColumnName = "is_Fraud",
                FeatureColumnName = "Features"
            });


            var trainer5  = mlContext.BinaryClassification.Trainers.FastForest(featureColumnName: "Features",
            labelColumnName: "is_Fraud",
            numberOfLeaves: 10,
            numberOfTrees: 100,
            minimumExampleCountPerLeaf: 10);

            var trainPipeline = concatenatedData.Append(mlContext.Transforms.Conversion.ConvertType("Features", "Features", DataKind.Single))
            .Append(trainer5);


            // <SnippetReturnModel>

            var model = trainPipeline.Fit(splitTrainSet);
            string eval = Evaluate(mlContext, splitTrainSet, trainPipeline);
            //string eval = "";
            return (model, eval);
            // </SnippetReturnModel>
        }
        /*public IQueryable<FPTrainingModel> GetAll(System.Linq.Expressions.Expression<Func<FPTrainingModel, bool>> predicate)
        {
            return _dbset.Where(predicate).AsNoTracking().AsQueryable();
        }
        */
        public DataFrame GetTrainingData()
        {
            db.Database.CommandTimeout = 10000;
            List<FPTrainingModel> Transactions = db.FPTrainingData.AsNoTracking().ToList();

            //IEnumerable<FPTrainingModel> Transactions = db.FPTrainingData.AsNoTracking();

            //List<FPTrainingModel> Transactions = GetAll(trx => trx.CardNumber != null).ToList();
            //List<FPTrainingModel> Transactions = db.FPTrainingData.AsNoTracking().Where(trx => trx.CardNumber != null).AsStreaming().ToList();

            /*
            int batchSize = 1000;
            List<FPTrainingModel> Transactions = new List<FPTrainingModel>();
            int totalCount = db.FPTrainingData.Count();

            for (int i = 0; i < totalCount; i += batchSize)
            {
                var batch = db.FPTrainingData
                    .Where(trx => trx.CardNumber != null)
                    .OrderBy(trx => trx.CardNumber) // Add appropriate ordering to ensure consistent batches
                    .Skip(i)
                    .Take(batchSize)
                    .ToList();

                Transactions.AddRange(batch);
            }
            */



            DataFrame dataFrame = new DataFrame();
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isCashPresented_tofloat", Transactions.Select(entry => entry.isCashPresented_toInt)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isCashTaken_tofloat", Transactions.Select(entry => entry.isCashTaken_toInt)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isCashRetracted_tofloat", Transactions.Select(entry => entry.isCashRetracted_toInt)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isCashoutError_tofloat", Transactions.Select(entry => entry.isCashoutError_toInt)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("ExistInHost_tofloat", Transactions.Select(entry => entry.ExistInHost_toInt)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Type_encoded_scaled", Transactions.Select(entry => entry.Type_encoded_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Amount_scaled", Transactions.Select(entry => entry.Amount_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Statut_scaled", Transactions.Select(entry => entry.Statut_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("CardNumber_encoded_scaled", Transactions.Select(entry => entry.CardNumber_encoded_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("time_between_transactions_scaled", Transactions.Select(entry => entry.time_between_transactions_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("frequecy_per_hour_scaled", Transactions.Select(entry => entry.frequecy_per_hour_scaled)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Average_Amount", Transactions.Select(entry => entry.Average_Amount)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Accumulated_Amount", Transactions.Select(entry => entry.Accumulated_Amount)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("min", Transactions.Select(entry => entry.min)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("max", Transactions.Select(entry => entry.max)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Lat", Transactions.Select(entry => entry.Lat)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Long", Transactions.Select(entry => entry.Long)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Distance_between_transactions", Transactions.Select(entry => entry.Distance_between_transactions)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("speed", Transactions.Select(entry => entry.speed)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("Average_Speed", Transactions.Select(entry => entry.Average_Speed)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("VelocityByLocation", Transactions.Select(entry => entry.VelocityByLocation)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("StandardDeviation", Transactions.Select(entry => entry.StandardDeviation)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("LocationName_encoded", Transactions.Select(entry => entry.LocationName_encoded)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_HM_Fraud", Transactions.Select(entry => entry.is_HM_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_IF_Fraud", Transactions.Select(entry => entry.is_IF_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_RC_Fraud", Transactions.Select(entry => entry.is_RC_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_TimeDiff_Fraud", Transactions.Select(entry => entry.is_TimeDiff_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_UnusualHour_Fraud", Transactions.Select(entry => entry.is_UnusualHour_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_Location_Fraud", Transactions.Select(entry => entry.is_Location_Fraud)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("PINError", Transactions.Select(entry => entry.PINError)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isnot_Consistent", Transactions.Select(entry => entry.isnot_Consistent)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("ExpiredCard", Transactions.Select(entry => entry.ExpiredCard)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("NbrDeclinedTransctions", Transactions.Select(entry => entry.NbrDeclinedTransctions)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("ManyDeclinedTrabsactions", Transactions.Select(entry => entry.ManyDeclinedTransactions)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isMultiplePINEntry", Transactions.Select(entry => entry.isMultiplePINEntry)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("IsLateNightOrEarlyMorning", Transactions.Select(entry => entry.IsLateNightOrEarlyMorning)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("FrequencyChange", Transactions.Select(entry => entry.FrequencyChange)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("isRepeated", Transactions.Select(entry => entry.isRepeated)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<double>("is_belongsToOrder", Transactions.Select(entry => entry.is_belongsToOrder)));
            dataFrame.Columns.Add(new PrimitiveDataFrameColumn<bool>("is_Fraud", Transactions.Select(entry => entry.is_Fraud)));




            return dataFrame;

        }

        private string Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            //=============== Cross-validating to get model's accuracy metrics ===============
            var crossValidationResults = mlContext.BinaryClassification.CrossValidateNonCalibrated(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "is_Fraud");
            string eval = PrintBinaryClassificationFoldsAverageMetrics(crossValidationResults);
            return eval;
        }
        public string PrintBinaryClassificationFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<BinaryClassificationMetrics>> crossValResults)
        {
            var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);
            var F1score = metricsInMultipleFolds.Select(m => m.F1Score);
            var AccuracyValues = metricsInMultipleFolds.Select(m => m.Accuracy);
            var confusionMatrix = metricsInMultipleFolds.Select(c => c.ConfusionMatrix);
            var AccuracyAverage = AccuracyValues.Average();
            var AccuraciesStdDeviation = CalculateStandardDeviation(AccuracyValues);
            var AccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(AccuracyValues);

            string eval = $"*       Average Accuracy:    {AccuracyAverage:0.###}  - Standard deviation: ({AccuraciesStdDeviation:#.###})  - Confidence Interval 95%: ({AccuraciesConfidenceInterval95:#.###})  - F1_score:({F1score.Average():#.###})";

            string matrixString = "";

            foreach (var result in crossValResults)
            {
                var confusionMatrix_ = result.Metrics.ConfusionMatrix.GetFormattedConfusionTable();
                matrixString += "\n"+confusionMatrix_;
            }

            return eval +"\n"+matrixString;


        }

        public double CalculateStandardDeviation(IEnumerable<double> values)
        {
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / (values.Count() - 1));
            return standardDeviation;
        }

        public double CalculateConfidenceInterval95(IEnumerable<double> values)
        {
            double confidenceInterval95 = 1.96 * CalculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1));
            return confidenceInterval95;
        }


        public ITransformer AlgoTuner(MLContext mlContext, IDataView TrainSet, IDataView TestSet)
        {
            var bestModel = default(ITransformer);

            return bestModel;
        }



        public (ITransformer, List<string>) Hyper_paramTuner(MLContext mlContext, IDataView TrainSet, IDataView TestSet)
        {
            var bestModel = default(ITransformer);
            var bestAccuracy = 0.0;
            // </SnippetCreateMLContext>
            // <SnippetCallLoadData>
            // </SnippetCallLoadData>

            var numericColumns = new[] { "ExpiryDifference_scaled", "Amount_scaled", "Statut_scaled", "n_PIN_attempts_scaled", "cvm_methods_scaled", "time_between_transactions_scaled", "frequecy_per_hour_scaled" };
            var categoricalColumns = new[] { "isCashPresented_toInt", "isCashTaken_toInt", "isCashRetracted_toInt", "isCashoutError_toInt", "ExistInHost_toInt" };


            var numericConcatenatedData = mlContext.Transforms.Concatenate("NumericFeatures", numericColumns);
            var categoricalConcatenatedData = mlContext.Transforms.Concatenate("CategoricalFeatures", categoricalColumns);

            var convertedCategoricalData = mlContext.Transforms.Conversion.ConvertType("CategoricalFeatures", "CategoricalFeatures", DataKind.Double);


            /*var concatenatedData = mlContext.Transforms.Concatenate("Features", featureColumns)
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", "is_Fraud"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label"));*/


            var concatenatedData = numericConcatenatedData
                .Append(categoricalConcatenatedData)
                .Append(convertedCategoricalData)
                .Append(mlContext.Transforms.Concatenate("Features", "NumericFeatures", "CategoricalFeatures"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", "is_Fraud"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label"));
            //var trainerOptions = new LightGbmBinaryTrainer.Options { };
            var trainerObtionsFastTree = new FastTreeBinaryTrainer.Options { };

            List<string> bestAccuracies = new List<string> { };

            for (int iterations = 5000; iterations <= 10000; iterations += 1000)
            {
                for (float learningRate = 0.1f; learningRate <= 0.5f; learningRate += 0.1f)
                {
                    /*
                    trainerOptions = new LightGbmBinaryTrainer.Options
                    {
                        NumberOfIterations = iterations,
                        LearningRate = learningRate,
                        // Set other hyperparameters as needed
                        LabelColumnName = "is_Fraud",
                        FeatureColumnName = "Features",
                        NumberOfLeaves = 7,
                        MinimumExampleCountPerLeaf = 10,
                        UseCategoricalSplit = false,
                        HandleMissingValue = false,
                        MinimumExampleCountPerGroup = 200,
                        MaximumCategoricalSplitPointCount = 16,
                        CategoricalSmoothing = 10,
                        L2CategoricalRegularization = 5,
                        Booster = new GradientBooster.Options() { L2Regularization = 1, L1Regularization = 0 }
                    };
                    var trainer = mlContext.BinaryClassification.Trainers.LightGbm(trainerOptions);
                    */

                    trainerObtionsFastTree = new FastTreeBinaryTrainer.Options
                    {
                        LearningRate = learningRate,
                        NumberOfLeaves = 10,
                        NumberOfTrees = 50,
                        LabelColumnName = "is_Fraud",
                        FeatureColumnName = "Features"
                    };
                    var trainer4 = mlContext.BinaryClassification.Trainers.FastTree(trainerObtionsFastTree);


                    var trainPipeline = mlContext.Transforms.Conversion.ConvertType("Label", "is_Fraud", DataKind.Boolean)
                        .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "is_Fraud", inputColumnName: nameof(Fraud_Data.is_Fraud)))
                        .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: nameof(Fraud_Data.is_Fraud)))
                        .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"));



                    var trainPipeline1 = concatenatedData.Append(mlContext.Transforms.Conversion.ConvertType("Features", "Features", DataKind.Single))
                    .Append(trainer4);
                    var model = trainPipeline1.Fit(TrainSet);

                    // Evaluate the model
                    var predictions = model.Transform(TestSet);
                    var metrics = mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "Label");
                    var accuracy = metrics.Accuracy;

                    if (accuracy > bestAccuracy)
                    {
                        bestAccuracy = accuracy;
                        bestModel = model;
                    }

                    string bestacc = $"bestAccuracy:{bestAccuracy}";
                    bestAccuracies.Add(bestacc);

                }

            }
            //Console.WriteLine($"Iterations = {trainerOptions.NumberOfIterations} and learning rate = {trainerOptions.LearningRate}");
            string bestlearningRate = $"bestlearningRate= {trainerObtionsFastTree.LearningRate}";
            bestAccuracies.Add(bestlearningRate);
            return (bestModel, bestAccuracies);
            // Use bestModel for predictions

        }
        /*
        public int TrainModeltest()
        {
            DataFrame TrainingData = GetTrainingData();
            int lenght = ML_model(TrainingData);
            return lenght;
        }*/


        public void TrainModel()
        {
            DataFrame TrainingData = GetTrainingData();
            ML_model(TrainingData);
        }

       

    }



}
