using AtmView.Entities;
using log4net;
using Microsoft.Data.Analysis;
using Microsoft.ML.Trainers;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Util;
using static iTextSharp.tool.xml.html.HTML;

namespace AtmView.Services
{
    public class FPCalculeFeatures
    {
        public AtmViewContext db;
        ILog ExceptionLogger = LogManager.GetLogger("ExceptionLogger");
        public FPCalculeFeatures(AtmViewContext db)
        {
            this.db = db;
        }

        public DataFrame DataSorting(DataFrame avtransactions)
        {
            //sort by card number 
            avtransactions = avtransactions.OrderBy("CardNumber");


            //sort by datetime 
            var grouped = from card in avtransactions.Rows.Cast<DataFrameRow>()
                          group card by card[3] into cardGroup
                          select new
                          {
                              CardNumber = cardGroup.Key,
                              Rows = cardGroup.OrderByDescending(row => (DateTime)row[1])
                              //Rows = cardGroup.OrderByDescending(row => DateTime.ParseExact((string)row[1], "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture))
                          };
            //Console.WriteLine(avtransactions.Info());
            string[] columnNames = avtransactions.Columns.Select(col => col.Name).ToArray();
            DataFrameColumn[] newColumns = new DataFrameColumn[columnNames.Length];

            for (int i = 0; i < newColumns.Length; i++)
            {
                Type columnType = avtransactions.Columns[i].DataType;
                if (columnType == typeof(int))
                {
                    newColumns[i] = new Int32DataFrameColumn(columnNames[i], avtransactions.Rows.Count);
                }
                else if (columnType == typeof(DateTime))
                {
                    newColumns[i] = new PrimitiveDataFrameColumn<DateTime>(columnNames[i], avtransactions.Rows.Count);
                }
                else if (columnType == typeof(string))
                {
                    newColumns[i] = new StringDataFrameColumn(columnNames[i], avtransactions.Rows.Count);
                }
                else if (columnType == typeof(bool))
                {
                    newColumns[i] = new BooleanDataFrameColumn(columnNames[i], avtransactions.Rows.Count);
                }
                else if (columnType == typeof(double))
                {
                    newColumns[i] = new DoubleDataFrameColumn(columnNames[i], avtransactions.Rows.Count);
                }

                else
                {

                    throw new NotSupportedException($"Unsupported column type: {columnType}");
                }

                //Console.WriteLine(columnType);
            }


            DataFrame newDataFrame = new DataFrame(newColumns);

            int rowIndex = 0;
            foreach (var group in grouped)
            {
                foreach (var row in group.Rows)
                {
                    for (int i = 0; i < row.Count(); i++)
                    {
                        newDataFrame[rowIndex, i] = row[i];
                    }
                    rowIndex++;
                }
            }


            return newDataFrame;
        }


        public string cleanCardNumber(string cardNumber)
        {
            // Remove underscores, asterisks, and multiple underscores
            return Regex.Replace(cardNumber.ToString(), "[_*X]+", "");
            //return cleanedNumber;
        }
        public DataFrame Time_between_trans(DataFrame data)
        {
            //var cardColumn = data.GetColumn<string>("CardNumber");
            var cardColumn = data["CardNumber"];

            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                //ExceptionLogger.Info("Card Number is :"+c);
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            // DataFrame grouped = data.GroupBy("CardNumber").Sum("Amount");
            // DataFrameColumn uniquecards = grouped.Columns[0];



            //Console.WriteLine(uniquecards);
            //var cardColumn = data.Columns["CardNumber"];
            //string[] uniqueCardNumbers = cardColumn.OfType<string>().Distinct().ToArray();
            var Transactiondates = data.Columns["TransactionDate"];
            DateTime previousTransactionDate = (DateTime)Transactiondates[0];
            List<double> timeBetweenTransactions = new List<double>();

            int i = 0;
            int j = 0;
            int count = 0;
            StringBuilder card_ = new StringBuilder(uniqueValues[i]);
            StringBuilder card = new StringBuilder(cardNumbersWithAsterisks[j]);



            DateTime transactionDate = (DateTime)Transactiondates[j];
            //ExceptionLogger.Info("Time between Transaction will start the loop");
            while (count < (cardNumbersWithAsterisks.Count))
            {


                //card = cardNumbersWithAsterisks[j];
                card = new StringBuilder(cardNumbersWithAsterisks[j]);
                if (card != null && card_.Equals(card))
                {
                    transactionDate = (DateTime)Transactiondates[j];
                    double timeDifferenceInMinites = (transactionDate - previousTransactionDate).TotalMinutes;
                    timeBetweenTransactions.Add(timeDifferenceInMinites);
                    previousTransactionDate = transactionDate;
                    j++;
                    //card = cardNumbersWithAsterisks[j];

                }

                else
                {

                    timeBetweenTransactions.Add(0);
                    i++;
                    previousTransactionDate = transactionDate;
                    card_ = new StringBuilder(uniqueValues[i]); // Update card_


                }

                //j++;
                count++;

            }
            //ExceptionLogger.Info("Time between Transaction ended the loop");
            DataFrame aux = data.Clone();
            var Time_diff = new DoubleDataFrameColumn("time_between_transactions", timeBetweenTransactions);
            aux.Columns.Add(Time_diff);
            return aux;
        }





        public DataFrame Velocity_per_hour(DataFrame data)
        {
            var time_diff = data.Columns["time_between_transactions"];
            List<double> velocity = new List<double>();

            foreach (double t in time_diff)
            {
                double hours = t / 60; //
                double v = hours == 0 ? 0 : 1.0 / hours; // 
                velocity.Add(v);
            }

            var Velocity = new DoubleDataFrameColumn("frequecy_per_hour", velocity);
            data.Columns.Add(Velocity);

            return data;
        }


        public DataFrame AddRandomPINAttempts(DataFrame data)
        {

            //data.Columns.Add(new PrimitiveDataFrameColumn<int>("n_PIN_attempts", 1));
            List<int> PINAttempts = Enumerable.Range(2, 4).ToList();
            int datasetLength = (int)data.Rows.Count;

            // Create a Random object to generate random numbers
            Random random = new Random();
            int numElements = (int)(0.03 * datasetLength);
            // Select 'numElements' random indices from the dataset
            List<int> randomIndices = Enumerable.Range(0, datasetLength)
                .OrderBy(_ => random.Next())
                .Take(numElements)
                .ToList();
            //Console.WriteLine(randomIndices.Count);
            List<int> PINAttempts_ = new List<int>();
            for (int i = 0; i < datasetLength; i++)
            {
                if (randomIndices.Contains(i))
                {
                    PINAttempts_.Add(random.Next(PINAttempts.Count));
                }
                else
                {
                    PINAttempts_.Add(1);
                }
            }

            var PIN = new Int32DataFrameColumn("n_PIN_attempts", PINAttempts_);
            data.Columns.Add(PIN);


            /*foreach (var m in data.Columns["n_PIN_attempts"])
            {
                Console.WriteLine(m);
            }
            Console.WriteLine(data.Head(6));*/

            return data;
        }


        /*
        public DataFrame AddRandomPINAttemptsnew(DataFrame data)
        {
            int datasetLength = (int)data.Rows.Count;
            int numElements = (int)(0.03 * datasetLength);

            // Create a Random object to generate random numbers
            Random random = new Random();

            for (int i = 0; i < numElements; i++)
            {
                int index = random.Next(datasetLength);
                int randomPINAttempt = random.Next(2, 5);
                data.Columns["n_PIN_attempts"][index] = randomPINAttempt;
            }

            return data;
        }*/


        public DataFrame Cvm_methods(DataFrame data)
        {
            List<int> cvm_methods = new List<int> { 1, 2, 5, 8, 80 };

            int datasetLength = (int)data.Rows.Count;

            // Create a Random object to generate random numbers
            Random random = new Random();
            int numElements = (int)(0.04 * datasetLength);
            // Select 'numElements' random indices from the dataset
            List<int> randomIndices = Enumerable.Range(0, datasetLength)
                .OrderBy(_ => random.Next())
                .Take(numElements)
                .ToList();
            List<int> cvms_methods = new List<int>();
            for (int i = 0; i < datasetLength; i++)
            {
                if (randomIndices.Contains(i))
                {
                    cvms_methods.Add(random.Next(cvm_methods.Count));
                }
                else
                {
                    cvms_methods.Add(3);
                }
            }

            var CVM = new Int32DataFrameColumn("cvm_methods", cvms_methods);
            data.Columns.Add(CVM);



            return data;

        }

        /*
        public DataFrame Cvm_methodsnew(DataFrame data)
        {
            List<int> cvm_methods = new List<int> { 1, 2, 5, 8, 80 };
            int datasetLength = (int)data.Rows.Count;
            int sampleSize = (int)(0.04 * datasetLength);

            // Use a HashSet to track selected indices
            HashSet<int> selectedIndices = new HashSet<int>();
            Random random = new Random();

            for (int i = 0; i < sampleSize; i++)
            {
                int index;
                do
                {
                    index = random.Next(datasetLength);
                } while (!selectedIndices.Add(index));

                int randomCvmMethod = i < sampleSize ? cvm_methods[i % cvm_methods.Count] : 3;
                data.Columns["cvm_methods"][index] = randomCvmMethod;
            }

            return data;
        }
        */


        public DataFrame IssuerCity(DataFrame data)
        {
            int datasetLength = (int)data.Rows.Count;

            // odd issuer cities
            List<string> oddCities = new List<string> { "Marrakech", "El ayoun", "Dakhla", "Agadir" };
            // normal issuer cities
            List<string> normalCities = new List<string> { "Casablanca", "Rabat", "El mohammadia" };

            // Create a single Random object
            Random random = new Random();

            List<string> issuerCity = new List<string>();
            for (int i = 0; i < datasetLength; i++)
            {
                List<string> selectedCities = (i < datasetLength * 0.03) ? oddCities : normalCities;
                issuerCity.Add(selectedCities[random.Next(selectedCities.Count)]);
            }

            var issuerCities = new StringDataFrameColumn("Issuer_cities", issuerCity);
            data.Columns.Add(issuerCities);

            return data;
        }

        /*
        public DataFrame IssuerCitynew(DataFrame data)
        {
            int datasetLength = (int)data.Rows.Count;

            // Odd issuer cities
            List<string> oddCities = new List<string> { "Marrakech", "El ayoun", "Dakhla", "Agadir" };
            // Normal issuer cities
            List<string> normalCities = new List<string> { "Casablanca", "Rabat", "El mohammadia" };

            // Create a single Random object
            Random random = new Random();

            for (int i = 0; i < datasetLength; i++)
            {
                List<string> selectedCities = (i < datasetLength * 0.03) ? oddCities : normalCities;
                data.Columns["Issuer_cities"][i] = selectedCities[random.Next(selectedCities.Count)];
            }

            return data;
        }*/


        public DataFrame Experation_date(DataFrame data)
        {

            Random random = new Random();
            var transactionTime = data.Columns["TransactionDate"];
            List<double> expiryDiffrence = new List<double>();
            List<DateTime> ExpiryDate = new List<DateTime>();
            foreach (var row in transactionTime)
            {
                DateTime transactionDate = (DateTime)row;
                DateTime newExpiryDate;
                //double expDiff;
                int randomY = random.Next(1, 4);
                int randomM = random.Next(1, 13);
                int randomD = random.Next(1, 30);
                int randomH = random.Next(0, 23);
                int randomMi = random.Next(0, 59);
                int randomS = random.Next(0, 59);

                if (random.NextDouble() > 0.11) // 11% chance of generating an invalid expiry date
                {


                    newExpiryDate = transactionDate.AddYears(randomY).AddMonths(randomM).AddDays(randomD).AddHours(randomH).AddMinutes(randomMi).AddSeconds(randomS); // Subtract one year from transaction date
                                                                                                                                                                      // Update the value in the DataTable if needed
                    expiryDiffrence.Add((transactionDate - newExpiryDate).TotalMinutes);


                }
                else
                {
                    newExpiryDate = transactionDate.AddYears(-randomY).AddMonths(-randomM).AddDays(-randomD).AddHours(-randomH).AddMinutes(-randomMi).AddSeconds(-randomS); // Subtract one year from transaction date
                    expiryDiffrence.Add((transactionDate - newExpiryDate).TotalMinutes);

                }
                ExpiryDate.Add(newExpiryDate);
            }
            var ExpiryDates = new DateTimeDataFrameColumn("ExpiryDate", ExpiryDate);
            data.Columns.Add(ExpiryDates);

            var Expirydiff = new DoubleDataFrameColumn("ExpiryDifference", expiryDiffrence);
            data.Columns.Add(Expirydiff);
            return data;


        }

        public DataFrame Scale_exp(DataFrame data)
        {
            var transactiondiff = data.Columns["ExpiryDifference"];
            double scalingFactor = 2.0;
            List<double> scaleddiff = new List<double>();
            foreach (double diff in transactiondiff)
            {
                scaleddiff.Add(diff >= 0 ? diff * scalingFactor : diff / scalingFactor);

            }

            var Expirydiff_ = new DoubleDataFrameColumn("ExpiryDifference_scaled", scaleddiff);
            data.Columns.Add(Expirydiff_);
            //data.Columns.Remove("ExpiryDifference");

            return data;


        }

        public DataFrame Label_Encoding(DataFrame data, string columnName)
        {
            Dictionary<string, int> labelMapping = new Dictionary<string, int>();
            int labelCount = 0;
            var column = data.Columns[columnName];
            List<int> encoding = new List<int>();
            //int i = 0;
            foreach (var item in column)
            {
                if (item != null)
                {
                    string category = (string)item;

                    if (!labelMapping.ContainsKey(category))
                    {
                        labelMapping[category] = labelCount;
                        labelCount++;
                    }

                    encoding.Add(labelMapping[category]);
                }

                else
                {
                    encoding.Add(0);
                }

            }

            /*foreach(var item in encoding)
            {
                Console.WriteLine(item);
            }*/

            var arrColumn = new Int32DataFrameColumn(columnName + "_encoded", encoding);
            data.Columns.Add(arrColumn);
            if (columnName != "CardNumber")
            {
                data.Columns.Remove(columnName);
            }
            return data;


        }

        public DataFrame LocationNameEncoder(DataFrame data)
        {

            Dictionary<string, double> labelMapping = new Dictionary<string, double>();
            int labelCount = 0;
            var column = data.Columns["LocationName"];
            List<double> encoding = new List<double>();
            //int i = 0;
            foreach (var item in column)
            {

                string category = (string)item;
                if (category != null)
                {
                    if (!labelMapping.ContainsKey(category))
                    {
                        labelMapping[category] = labelCount;
                        labelCount++;
                    }

                    encoding.Add(labelMapping[category]);
                }

                else
                {
                    encoding.Add(0);
                }



            }

            /*foreach(var item in encoding)
            {
                Console.WriteLine(item);
            }*/

            var arrColumn = new DoubleDataFrameColumn("LocationName_encoded", encoding);
            data.Columns.Add(arrColumn);
            data.Columns.Remove("LocationName");


            return data;
        }



        public DataFrame averageAmount(DataFrame data)
        {
            DataFrame dataframe = data.GroupBy("CardNumber").Mean("Amount_scaled");
            DataFrameColumn meanData = dataframe.Columns[1];

            //DataFrameColumn uniquecards = dataframe.Columns[0];




            List<double> Amount_moy = new List<double>();
            var cardColumn = data.Columns["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }

            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();



            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();



            List<string> cardNumbersWithAsterisks = sortedList.Select(car => car.Length >= 4 ? $"{car.Substring(0, 4)}******{car.Substring(4)}" : car).ToList();


            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            while (j < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    double meanAm = (double)meanData[i];
                    Amount_moy.Add(meanAm);
                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;
            }


            var mean = new DoubleDataFrameColumn("Average_Amount", Amount_moy);
            data.Columns.Add(mean);


            return data;
        }

        public DataFrame averageAmountNew(DataFrame data)
        {
            //DataFrame dataframe = data.GroupBy("CardNumber").Mean("Amount_scaled");
            //DataFrameColumn meanData = dataframe.Columns[1];
            //DataFrameColumn unique = dataframe.Columns[1];

            var CardNumbers = data.Columns["CardNumber"];
            var Amounts = data.Columns["Amount_scaled"];
            int i = 0;

            Dictionary<string, int> counts = TransactionsCounts(data);

            Dictionary<string, double> AverageAmountPercard = new Dictionary<string, double>();
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                double Amt = ((double)Amounts[i] / counts[card1.ToString()]);

                if (AverageAmountPercard.ContainsKey(card1.ToString()))
                {

                    AverageAmountPercard[card1.ToString()] += Amt;
                }
                else
                {
                    AverageAmountPercard[card1.ToString()] = Amt;
                }

                i++;

                card1 = null;
            }

            List<double> AverageAmount = new List<double>();

            int j = 0;
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                if (AverageAmountPercard.ContainsKey(card1.ToString()))
                {

                    AverageAmount.Add(AverageAmountPercard[card1.ToString()]);
                }
                else
                {
                    AverageAmount.Add(0);
                }
                j++;
                card1 = null;



            }
            var mean = new DoubleDataFrameColumn("Average_Amount", AverageAmount);
            data.Columns.Add(mean);

            return data;
        }



        public DataFrame AccumulatedAmount(DataFrame data)
        {

            DataFrame dataframe = data.GroupBy("CardNumber").Sum("Amount_scaled");
            DataFrameColumn SumData = dataframe.Columns[1];


            List<double> AcuumulatedAmount = new List<double>();
            var cardColumn = data.Columns["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }

            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();



            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();



            List<string> cardNumbersWithAsterisks = sortedList.Select(car => car.Length >= 4 ? $"{car.Substring(0, 4)}******{car.Substring(4)}" : car).ToList();


            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            while (j < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    double SumAm = (double)SumData[i];
                    AcuumulatedAmount.Add(SumAm);
                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;
            }


            var Sum = new DoubleDataFrameColumn("Accumulated_Amount", AcuumulatedAmount);
            data.Columns.Add(Sum);


            return data;
        }
        public DataFrame AccumulatedAmountNew(DataFrame data)
        {
            var CardNumbers = data.Columns["CardNumber"];
            var Amounts = data.Columns["Amount_scaled"];
            int i = 0;

            Dictionary<string, int> counts = TransactionsCounts(data);

            Dictionary<string, double> AccumulatedAmountPerCard = new Dictionary<string, double>();
            //Dictionary<string, double> max = new Dictionary<string, double>();


            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                double amount = (double)Amounts[i];

                if (AccumulatedAmountPerCard.ContainsKey(card1.ToString()))
                {

                    AccumulatedAmountPerCard[card1.ToString()] += amount;
                }
                else
                {
                    AccumulatedAmountPerCard[card1.ToString()] = amount;
                }
                card1 = null;


                i++;
            }

            List<double> AccumulatedAmount = new List<double>();

            int j = 0;
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                if (AccumulatedAmountPerCard.ContainsKey(card1.ToString()))
                {

                    AccumulatedAmount.Add(AccumulatedAmountPerCard[card1.ToString()]);
                }
                else
                {
                    AccumulatedAmount.Add(0);
                }
                j++;
                card1 = null;


            }
            var mean = new DoubleDataFrameColumn("Accumulated_Amount", AccumulatedAmount);
            data.Columns.Add(mean);
            return data;
        }


        public DataFrame MinMax(DataFrame data)
        {
            DataFrame MinPerCard = data.GroupBy("CardNumber").Min("Amount_scaled");
            DataFrame MaxPerCard = data.GroupBy("CardNumber").Max("Amount_scaled");
            DataFrameColumn minValues = MinPerCard.Columns[1];
            DataFrameColumn MaxValues = MaxPerCard.Columns[1];


            List<double> min = new List<double>();
            List<double> max = new List<double>();




            var cardColumn = data.Columns["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }

            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();



            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();



            List<string> cardNumbersWithAsterisks = sortedList.Select(car => car.Length >= 4 ? $"{car.Substring(0, 4)}******{car.Substring(4)}" : car).ToList();


            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];


            while (j < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    double minValue = (double)minValues[i];
                    double maxValue = (double)MaxValues[i];
                    min.Add(minValue);
                    max.Add(maxValue);
                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;
            }


            var MinCol = new DoubleDataFrameColumn("min", min);
            var MaxCol = new DoubleDataFrameColumn("max", max);
            data.Columns.Add(MinCol);
            data.Columns.Add(MaxCol);



            return data;
        }

        public DataFrame MinMaxNew(DataFrame data)
        {

            var CardNumbers = data.Columns["CardNumber"];
            var Amounts = data.Columns["Amount_scaled"];
            int i = 0;



            Dictionary<string, double> minperCard = new Dictionary<string, double>();
            Dictionary<string, double> maxperCard = new Dictionary<string, double>();


            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                double amount = (double)Amounts[i];

                if (minperCard.ContainsKey(card1.ToString()))
                {
                    if (minperCard[card1.ToString()] > amount)
                        minperCard[card1.ToString()] = amount;


                }
                else
                {
                    minperCard[card1.ToString()] = amount;

                }

                if (maxperCard.ContainsKey(card1.ToString()))
                {
                    if (maxperCard[card1.ToString()] < amount)
                        maxperCard[card1.ToString()] = amount;


                }
                else
                {
                    maxperCard[card1.ToString()] = amount;

                }

                card1 = null;
                i++;
            }



            List<double> min = new List<double>();
            List<double> max = new List<double>();

            int j = 0;
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                if (minperCard.ContainsKey(card1.ToString()))
                {

                    min.Add(minperCard[card1.ToString()]);

                }
                else
                {
                    min.Add(0);
                }

                if (maxperCard.ContainsKey(card1.ToString()))
                {

                    max.Add(maxperCard[card1.ToString()]);

                }
                else
                {
                    max.Add(0);
                }
                j++;
                card1 = null;

            }
            var minValues = new DoubleDataFrameColumn("min", min);
            var maxValues = new DoubleDataFrameColumn("max", max);
            data.Columns.Add(minValues);
            data.Columns.Add(maxValues);

            return data;
        }


        public DataFrame AtmLocation(DataFrame data)
        {
            //List<double?> Lat = db.Atms.Select(c=> c.Lat).ToList();
            //List<double?> Long = db.Atms.Select(c=>c.Long).ToList();


            List<Atm> Atms = db.Atms.ToList();



            List<double> AtmLat = new List<double>();
            List<double> AtmLong = new List<double>();
            List<string> LocationsName = new List<string>();

            foreach (var Atmid in data.Columns["AtmID"])
            {
                if (Atmid != null)
                {
                    var atm = Atms.FirstOrDefault(atm_ => (string)Atmid == atm_.Id);


                    if (atm != null)
                    {
                        if (atm.Lat != null && atm.Long != null)
                        {
                            AtmLat.Add(atm.Lat.Value);
                            AtmLong.Add(atm.Long.Value);
                        }

                        else
                        {
                            AtmLat.Add(0);
                            AtmLong.Add(0);
                        }

                        LocationsName.Add(atm.Location);

                    }

                    else
                    {
                        AtmLat.Add(0);
                        AtmLong.Add(0);
                        LocationsName.Add("");
                    }

                }

                else
                {
                    AtmLat.Add(0);
                    AtmLong.Add(0);
                    LocationsName.Add("");
                }
            }
            var Lat = new DoubleDataFrameColumn("Lat", AtmLat);
            var Long = new DoubleDataFrameColumn("Long", AtmLong);
            var Locations = new StringDataFrameColumn("LocationName", LocationsName);
            data.Columns.Add(Lat);
            data.Columns.Add(Long);
            data.Columns.Add(Locations);

            return data;

        }



        public double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double EarthRadiusKm = 6371.0;
            if (lat1 != 0 && lon1 != 0 && lat2 != 0 && lon2 != 0)
            {

                double radiansLat1 = ToRadians(lat1);
                double radiansLon1 = ToRadians(lon1);
                double radiansLat2 = ToRadians(lat2);
                double radiansLon2 = ToRadians(lon2);


                double dLat = radiansLat2 - radiansLat1;
                double dLon = radiansLon2 - radiansLon1;

                // Haversine formula
                double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                           Math.Cos(radiansLat1) * Math.Cos(radiansLat2) * Math.Pow(Math.Sin(dLon / 2), 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                // Distance in kilometers
                double distance = EarthRadiusKm * c;

                return distance;
            }
            else
            {

                return 0;
            }
        }



        public DataFrame Dst_between_Trans(DataFrame data)
        {
            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();


            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();


            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();


            var Lats = data.Columns["Lat"];
            var Longs = data.Columns["Long"];



            double prevLat = (double)Lats[0];
            double prevLong = (double)Longs[0];


            List<double> DistBetweenTransactions = new List<double>();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];


            double Lat = (double)Lats[j];
            double Long = (double)Longs[j];
            while (count < (cardNumbersWithAsterisks.Count))
            {


                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    Lat = (double)Lats[j];
                    Long = (double)Longs[j];
                    double distance = 0;
                    if (Lat != 0 && Long != 0) distance = CalculateDistance(Lat, Long, prevLat, prevLong);
                    else distance = 0;
                    DistBetweenTransactions.Add(distance);
                    prevLat = Lat;
                    prevLong = Long;
                    j++;
                    //card = cardNumbersWithAsterisks[j];

                }

                else
                {

                    DistBetweenTransactions.Add(0);
                    i++;
                    //prevLat = Lat;
                    //prevLong = Long;
                    card_ = uniqueValues[i]; // Update card_


                }

                //j++;
                count++;

            }
            //DataFrame aux = data.Clone();
            var Dist_diff = new DoubleDataFrameColumn("Distance_between_transactions", DistBetweenTransactions);
            data.Columns.Add(Dist_diff);
            return data;
        }




        public Dictionary<(string CardNumber, string Location), int> CalculateVelocityPerLocation(DataFrame data)
        {
            Dictionary<(string CardNumber, string Location), int> locationFrequency = new Dictionary<(string, string), int>();

            var Locations = data.Columns["LocationName"];
            var CardNumbers = data.Columns["CardNumber"];

            int i = 0;


            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                var key= (card1.ToString(), (string)Locations[i]);

                if (locationFrequency.ContainsKey(key))
                {
                    locationFrequency[key]++;
                }
                else
                {
                    locationFrequency[key] = 1;
                }
                i++;
                card1 = null;
                //key = null;
            }

            CardNumbers = null;

            return locationFrequency;
        }
        public DataFrame VelocityPerLocation(DataFrame data)
        {
            Dictionary<(string CardNumber, string Location), int> frequencies = CalculateVelocityPerLocation(data);
            var cards = data.Columns["CardNumber"];
            var Locations = data.Columns["LocationName"];
            List<double> velocity = new List<double>();

            int j = 0;
            string loc = (string)Locations[j];
            foreach (var card in cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                var key= (card1.ToString(), (string)Locations[j]);
                int totalVisits = frequencies
                    .Where(entry => entry.Key.CardNumber == card1.ToString())
                    .Sum(entry => entry.Value);


                if (frequencies.ContainsKey(key))
                {
                    velocity.Add(frequencies[key]);
                }

                j++;
                card1 = null;

                //key = null;
            }

            var vel = new DoubleDataFrameColumn("VelocityByLocation", velocity);
            data.Columns.Add(vel);

            frequencies = null;
            return data;
        }



        public DataFrame VelocityPerLocationNew(DataFrame data)
        {
            Dictionary<(string CardNumber, string Location), int> frequencies = new Dictionary<(string CardNumber, string Location), int>();
            var cards = data.Columns["CardNumber"];
            var locations = data.Columns["LocationName"];

            Dictionary<(string CardNumber, string Location), int> transactionCounts = CalculateVelocityPerLocation(data);
            /*
            foreach (var card in cards)
            {
                string cardNumber = card1.ToString();

                if (transactionCounts.ContainsKey(cardNumber))
                {
                    transactionCounts[cardNumber]++;
                }
                else
                {
                    transactionCounts[cardNumber] = 1;
                }
            }
            */



            List<double> velocities = new List<double>();
            int j = 0;
            foreach (var card in cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                var key= (card1.ToString(), (string)locations[j]);

                if (frequencies.TryGetValue(key, out int frequency))
                {
                    velocities.Add((double)(transactionCounts[key] - frequency) / transactionCounts[key]);
                    frequencies[key]++;
                }
                else
                {
                    velocities.Add(transactionCounts[key] - 1 / transactionCounts[key]);
                    frequencies[key] = 2;
                }
                j++;
                card1 = null;
            }




            var vel = new DoubleDataFrameColumn("VelocityByLocation", velocities);
            data.Columns.Add(vel);


            frequencies = null;
            cards = null;
            transactionCounts = null;
            return data;
        }


        //Frequency change in time Difference
        public DataFrame FrequencyChange(DataFrame data)
        {
            var TimeDiff = data.Columns["time_between_transactions_scaled"];
            var means = data.GroupBy("CardNumber").Mean("time_between_transactions_scaled");
            Dictionary<string, int> transactionCount = TransactionsCounts(data);

            Dictionary<string, double> MeanValues = new Dictionary<string, double>();
            Dictionary<string, double> StdValues = new Dictionary<string, double>();
            int i = 0;
            int j = 0;



            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (MeanValues.ContainsKey(card1.ToString()))
                {
                    MeanValues[card1.ToString()] += (double)TimeDiff[i] / transactionCount[card1.ToString()];
                }
                else
                {
                    MeanValues[card1.ToString()] = (double)TimeDiff[i] / transactionCount[card1.ToString()];
                }

                if (transactionCount[card1.ToString()] > 1)
                {
                    if (StdValues.ContainsKey(card1.ToString()))
                    {
                        StdValues[card1.ToString()] += Math.Pow((double)TimeDiff[j] - (double)MeanValues[card1.ToString()], 2) / (transactionCount[card1.ToString()] - 1);
                    }
                    else
                    {
                        StdValues[card1.ToString()] = Math.Pow((double)TimeDiff[j] - (double)MeanValues[card1.ToString()], 2) / (transactionCount[card1.ToString()] - 1);
                    }
                }
                else
                {
                    StdValues[card1.ToString()] = 0;
                }
                j++;
                i++;
                card1 = null;
            }





            List<double> Means = new List<double>();
            List<double> STDs = new List<double>();

            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (MeanValues.ContainsKey(card1.ToString()))
                {
                    Means.Add(MeanValues[card1.ToString()]);
                }
                else
                {
                    Means.Add(0);
                }
                if (StdValues.ContainsKey(card1.ToString()))
                {
                    STDs.Add(StdValues[card1.ToString()]);
                }
                else
                {
                    STDs.Add(0);
                }
                card1 = null;
            }

            List<bool> FrequencyChange = new List<bool>();
            int k = 0;
            int thresholdStd = 2;
            foreach (var T in TimeDiff)
            {
                if (Math.Abs((double)T - Means[k]) > thresholdStd * STDs[k])
                {
                    FrequencyChange.Add(true);
                }
                else
                {
                    FrequencyChange.Add(false);
                }
                k++;
            }



            var changes = new BooleanDataFrameColumn("FrequencyChange", FrequencyChange);
            data.Columns.Add(changes);

            return data;
        }



        public DataFrame FrequencyChangeNew(DataFrame data)
        {
            var TimeDiff = data.Columns["time_between_transactions_scaled"];
            var means = data.GroupBy("CardNumber").Mean("time_between_transactions_scaled");
            Dictionary<string, int> transactionCount = TransactionsCounts(data);

            Dictionary<string, double> MeanValues = new Dictionary<string, double>();
            Dictionary<string, double> StdValues = new Dictionary<string, double>();
            int i = 0;
            int j = 0;



            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder(card.ToString());


                if (MeanValues.ContainsKey(card1.ToString()))
                {
                    MeanValues[card1.ToString()] += (double)TimeDiff[i] / transactionCount[card1.ToString()];
                }
                else
                {
                    MeanValues[card1.ToString()] = (double)TimeDiff[i] / transactionCount[card1.ToString()];
                }

                if (transactionCount[card1.ToString()] > 1)
                {
                    if (StdValues.ContainsKey(card1.ToString()))
                    {
                        StdValues[card1.ToString()] += Math.Pow((double)TimeDiff[j] - (double)MeanValues[card1.ToString()], 2) / (transactionCount[card1.ToString()] - 1);
                    }
                    else
                    {
                        StdValues[card1.ToString()] = Math.Pow((double)TimeDiff[j] - (double)MeanValues[card1.ToString()], 2) / (transactionCount[card1.ToString()] - 1);
                    }
                }
                else
                {
                    StdValues[card1.ToString()] = 0;
                }
                j++;
                i++;

                card1 = null;
            }





            List<double> Means = new List<double>();
            List<double> STDs = new List<double>();

            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (MeanValues.ContainsKey(card1.ToString()))
                {
                    Means.Add(MeanValues[card1.ToString()]);
                }
                else
                {
                    Means.Add(0);
                }
                if (StdValues.ContainsKey(card1.ToString()))
                {
                    STDs.Add(StdValues[card1.ToString()]);
                }
                else
                {
                    STDs.Add(0);
                }
                card1 = null;
            }

            List<double> FrequencyChange = new List<double>();
            int k = 0;
            int thresholdStd = 2;
            foreach (var T in TimeDiff)
            {
                if (Math.Abs((double)T - Means[k]) > thresholdStd * STDs[k])
                {
                    FrequencyChange.Add(1);
                }
                else
                {
                    FrequencyChange.Add(0);
                }
                k++;
            }



            var changes = new DoubleDataFrameColumn("FrequencyChange", FrequencyChange);
            data.Columns.Add(changes);

            return data;
        }




        public DataFrame Speed(DataFrame data)
        {


            List<double> speed = new List<double>();
            var timDiff = data.Columns["time_between_transactions_scaled"];
            var distDiff = data.Columns["Distance_between_transactions"];
            int i = 0;



            foreach (var time in timDiff)
            {
                if ((double)time != 0)
                {
                    speed.Add((double)distDiff[i] / (double)time / 60);
                }

                else speed.Add(0);

                i++;
            }

            var Speed = new DoubleDataFrameColumn("speed", speed);
            data.Columns.Add(Speed);
            return data;
        }


        public DataFrame AverageSpeed(DataFrame data)
        {
            DataFrame dataframe = data.GroupBy("CardNumber").Mean("speed");
            DataFrameColumn meanData = dataframe.Columns[1];

            DataFrameColumn uniquecards = dataframe.Columns[0];




            List<double> Speed_moy = new List<double>();
            var cardColumn = data.Columns["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }

            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();



            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();



            List<string> cardNumbersWithAsterisks = sortedList.Select(car => car.Length >= 4 ? $"{car.Substring(0, 4)}******{car.Substring(4)}" : car).ToList();


            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            while (j < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    double meanAm = (double)meanData[i];
                    Speed_moy.Add(meanAm);
                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;
            }


            var mean = new DoubleDataFrameColumn("Average_Speed", Speed_moy);
            data.Columns.Add(mean);


            return data;
        }

        public DataFrame AverageSpeedNew(DataFrame data)
        {
            var CardNumbers = data.Columns["CardNumber"];
            var speed = data.Columns["speed"];
            int i = 0;

            Dictionary<string, int> counts = TransactionsCounts(data);

            Dictionary<string, double?> AverageSpeedPercard = new Dictionary<string, double?>();
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                double? spd = ((double?)speed[i] / counts[card1.ToString()]);

                if (AverageSpeedPercard.ContainsKey(card1.ToString()))
                {

                    AverageSpeedPercard[card1.ToString()] += spd;
                }
                else
                {
                    AverageSpeedPercard[card1.ToString()] = spd;
                }
                card1 = null;
                card1 = null;
                i++;
            }

            List<double?> AverageSpeed = new List<double?>();

            int j = 0;
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                // var key= card1.ToString();
                if (AverageSpeedPercard.ContainsKey(card1.ToString()))
                {

                    AverageSpeed.Add(AverageSpeedPercard[card1.ToString()]);
                }
                else
                {
                    AverageSpeed.Add(0);
                }
                j++;

                card1 = null;


            }
            var mean = new DoubleDataFrameColumn("Average_Speed", AverageSpeed);
            data.Columns.Add(mean);

            return data;
        }


        public DataFrame TimeDistanceConsistency(DataFrame data)
        {
            var timeDiff = data.Columns["time_between_transactions_scaled"];
            var distance_diff = data.Columns["Distance_between_transactions"];
            List<bool> isnot_Consistent = new List<bool>();
            int i = 0;
            double consistencyScore = 0;
            double expectedTravelTime = 0;
            foreach (var t in timeDiff)
            {
                if (distance_diff[i] != null)
                {
                    double? speed = (double?)data.Columns["Average_Speed"][i];

                    if (speed != null && speed != 0)
                    {
                        expectedTravelTime = (double)((double)distance_diff[i] / speed);
                    }
                    else
                    {
                        expectedTravelTime = CalculateExpectedTravelTime((double)distance_diff[i]);
                    }


                    consistencyScore = Math.Abs((double)t / 60 - expectedTravelTime) / expectedTravelTime;
                    if (consistencyScore < 0.2) isnot_Consistent.Add(true);
                    else isnot_Consistent.Add(false);

                }
                else
                {
                    isnot_Consistent.Add(false);
                }
                i++;
            }

            var vel = new BooleanDataFrameColumn("isnot_Consistent", isnot_Consistent);
            data.Columns.Add(vel);

            return data;
        }




        public DataFrame TimeDistanceConsistencyNew(DataFrame data)
        {
            var timeDiff = data.Columns["time_between_transactions_scaled"];
            var distance_diff = data.Columns["Distance_between_transactions"];
            List<double> isnot_Consistent = new List<double>();
            int i = 0;
            double consistencyScore = 0;
            double expectedTravelTime = 0;
            foreach (var t in timeDiff)
            {
                if (distance_diff[i] != null)
                {
                    double? speed = (double?)data.Columns["Average_Speed"][i];

                    if (speed != null && speed != 0)
                    {
                        expectedTravelTime = (double)((double)distance_diff[i] / speed);
                    }
                    else
                    {
                        expectedTravelTime = CalculateExpectedTravelTime((double)distance_diff[i]);
                    }


                    consistencyScore = Math.Abs((double)t / 60 - expectedTravelTime) / expectedTravelTime;
                    if (consistencyScore < 0.2) isnot_Consistent.Add(1);
                    else isnot_Consistent.Add(0);

                }
                else
                {
                    isnot_Consistent.Add(0);
                }
                i++;
            }

            var vel = new DoubleDataFrameColumn("isnot_Consistent", isnot_Consistent);
            data.Columns.Add(vel);

            return data;
        }



        private double CalculateExpectedTravelTime(double distance)
        {

            double averageSpeed = 50.0; // in km/h
            return distance / averageSpeed;
        }


        public Dictionary<string, int> TransactionsCounts(DataFrame data)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);

                if (counts.ContainsKey(card1.ToString()))
                {
                    counts[card1.ToString()]++;
                }
                else
                {
                    counts[card1.ToString()] = 1;
                }
                card1 = null;
            }
            return counts;



        }


        public DataFrame NbrDeclinedTrans(DataFrame data)
        {

            Dictionary<string, int> NbrDeclinedTranspercard = new Dictionary<string, int>();
            var CardNumbers = data.Columns["CardNumber"];
            var is_Fraud = data.Columns["is_FraudP"];
            var rc = data.Columns["Statut_scaled"];

            int i = 0;

            //List<double> rejectedcodes = new List<double> {101, };
            List<double> rejectedcodes = new List<double>
                {
                    101, 102, 104, 109, 110, 111, 113, 116, 117, 119,
                    120, 122, 123, 124, 125, 126, 127, 128, 129, 188,
                    189, 190, 191, 192, 194, 195, 196, 197, 201, 202,
                    203, 205, 209, 210, 212, 217, 220
                };
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);

                bool is_fraud = (bool)is_Fraud[i];
                double r_code = (double)rc[i];
                if (NbrDeclinedTranspercard.ContainsKey(card1.ToString()))
                {
                    if (is_fraud == true || rejectedcodes.Contains(r_code))
                        NbrDeclinedTranspercard[card1.ToString()]++;
                }
                else
                {
                    NbrDeclinedTranspercard[card1.ToString()] = 0;
                }
                i++;
            }




            List<double> NbrDeclinedTransctions = new List<double>();
            List<bool> is_suspicious = new List<bool>();
            Dictionary<string, int> counts = TransactionsCounts(data);
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                //// var key= card1.ToString();

                if (NbrDeclinedTranspercard.TryGetValue(card1.ToString(), out int nbr))
                {
                    NbrDeclinedTransctions.Add((double)(nbr));
                    if (counts[card1.ToString()] != 0)
                    {
                        if (nbr / counts[card1.ToString()] > 1 / 2) is_suspicious.Add(true);
                        else is_suspicious.Add(false);
                    }
                    else
                    {
                        is_suspicious.Add(false);
                    }
                }
                else
                {
                    NbrDeclinedTransctions.Add(0);
                }

                card1 = null;

            }




            var vel = new DoubleDataFrameColumn("NbrDeclinedTransctions", NbrDeclinedTransctions);
            var is_Sus = new BooleanDataFrameColumn("ManyDeclinedTransactions", is_suspicious);
            data.Columns.Add(vel);
            data.Columns.Add(is_Sus);



            return data;
        }



        public DataFrame NbrDeclinedTransNew(DataFrame data)
        {

            Dictionary<string, int> NbrDeclinedTranspercard = new Dictionary<string, int>();
            var CardNumbers = data.Columns["CardNumber"];
            var is_Fraud = data.Columns["is_FraudP"];
            var rc = data.Columns["Statut_scaled"];

            int i = 0;

            //List<double> rejectedcodes = new List<double> {101, };
            List<double> rejectedcodes = new List<double>
                {
                    101, 102, 104, 109, 110, 111, 113, 116, 117, 119,
                    120, 122, 123, 124, 125, 126, 127, 128, 129, 188,
                    189, 190, 191, 192, 194, 195, 196, 197, 201, 202,
                    203, 205, 209, 210, 212, 217, 220
                };
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                //// var key= card1.ToString();
                bool is_fraud = (bool)is_Fraud[i];
                double r_code = (double)rc[i];
                if (NbrDeclinedTranspercard.ContainsKey(card1.ToString()))
                {
                    if (is_fraud == true || rejectedcodes.Contains(r_code))
                        NbrDeclinedTranspercard[card1.ToString()]++;
                }
                else
                {
                    NbrDeclinedTranspercard[card1.ToString()] = 0;
                }
                i++;
                card1 = null;
            }




            List<double> NbrDeclinedTransctions = new List<double>();
            List<double> is_suspicious = new List<double>();
            Dictionary<string, int> counts = TransactionsCounts(data);
            foreach (var card in CardNumbers)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                

                if (NbrDeclinedTranspercard.TryGetValue(card1.ToString(), out int nbr))
                {
                    NbrDeclinedTransctions.Add((double)(nbr));
                    if (counts[card1.ToString()] != 0)
                    {
                        if (nbr / counts[card1.ToString()] > 1 / 2) is_suspicious.Add(1);
                        else is_suspicious.Add(0);
                    }
                    else
                    {
                        is_suspicious.Add(0);
                    }
                }
                else
                {
                    NbrDeclinedTransctions.Add(0);
                }

                card1 = null;

            }




            var vel = new DoubleDataFrameColumn("NbrDeclinedTransctions", NbrDeclinedTransctions);
            var is_Sus = new DoubleDataFrameColumn("ManyDeclinedTransactions", is_suspicious);
            data.Columns.Add(vel);
            data.Columns.Add(is_Sus);



            return data;
        }



        public DataFrame PIN_Error(DataFrame data)
        {
            List<bool> PinError = new List<bool>();
            var stat = data.Columns["Statut_scaled"];

            foreach (var st in stat)
            {
                if ((double)st == 117 || (double)st == 126 || (double)st == 127 || (double)st == 128) PinError.Add(true);
                else PinError.Add(false);

            }

            var arrColumn = new BooleanDataFrameColumn("PINError", PinError);
            data.Columns.Add(arrColumn);

            return data;
        }

        public DataFrame PIN_ErrorNew(DataFrame data)
        {
            List<double> PinError = new List<double>();
            var stat = data.Columns["Statut_scaled"];

            foreach (var st in stat)
            {
                if ((double)st == 117 || (double)st == 126 || (double)st == 127 || (double)st == 128) PinError.Add(1);
                else PinError.Add(0);

            }

            var arrColumn = new DoubleDataFrameColumn("PINError", PinError);
            data.Columns.Add(arrColumn);

            return data;
        }


        public DataFrame MultiplePINEntry(DataFrame data)
        {
            var Cards = data.Columns["CardNumber"];
            var isPINError = data.Columns["PINError"];
            var timediff = data.Columns["time_between_transactions_scaled"];

            List<bool> isMultiplePINEntry = new List<bool>();

            int i = 0;

            foreach (var card in Cards)
            {
                if ((double)timediff[i] < 30 && (double)timediff[i] > 0 && (bool)isPINError[i] == true)
                {
                    isMultiplePINEntry.Add(true);
                }
                else
                {
                    isMultiplePINEntry.Add(true);
                }
                i++;
            }

            var arrColumn = new BooleanDataFrameColumn("isMultiplePINEntry", isMultiplePINEntry);
            data.Columns.Add(arrColumn);



            return data;
        }



        public DataFrame MultiplePINEntryNew(DataFrame data)
        {
            var Cards = data.Columns["CardNumber"];
            var isPINError = data.Columns["PINError"];
            var timediff = data.Columns["time_between_transactions_scaled"];

            List<double> isMultiplePINEntry = new List<double>();

            int i = 0;

            foreach (var card in Cards)
            {
                if ((double)timediff[i] < 30 && (double)timediff[i] > 0 && (double)isPINError[i] == 1)
                {
                    isMultiplePINEntry.Add(1);
                }
                else
                {
                    isMultiplePINEntry.Add(0);
                }
                i++;
            }

            var arrColumn = new DoubleDataFrameColumn("isMultiplePINEntry", isMultiplePINEntry);
            data.Columns.Add(arrColumn);



            return data;
        }




        public DataFrame IsLateNightOrEarlyMorning(DataFrame data)
        {
            var Dates = data.Columns["TransactionDate"];


            TimeSpan lateNightStart = new TimeSpan(23, 0, 0); // 11:00 PM
            TimeSpan earlyMorningEnd = new TimeSpan(6, 0, 0); // 6:00 AM

            List<bool> IsLateNightOrEarlyMorning = new List<bool>();
            //TimeSpan transactionTime = Dates[0].time
            foreach (var date in Dates)
            {
                DateTime date_ = (DateTime)date;
                TimeSpan transactionTime = date_.TimeOfDay;
                IsLateNightOrEarlyMorning.Add(transactionTime >= lateNightStart || transactionTime <= earlyMorningEnd);

            }

            var arrColumn = new BooleanDataFrameColumn("IsLateNightOrEarlyMorning", IsLateNightOrEarlyMorning);
            data.Columns.Add(arrColumn);


            return data;
        }



        public DataFrame IsLateNightOrEarlyMorningNew(DataFrame data)
        {
            var Dates = data.Columns["TransactionDate"];


            TimeSpan lateNightStart = new TimeSpan(23, 0, 0); // 11:00 PM
            TimeSpan earlyMorningEnd = new TimeSpan(6, 0, 0); // 6:00 AM

            List<double> IsLateNightOrEarlyMorning = new List<double>();
            //TimeSpan transactionTime = Dates[0].time
            foreach (var date in Dates)
            {
                DateTime date_ = (DateTime)date;
                TimeSpan transactionTime = date_.TimeOfDay;
                bool cdt = transactionTime >= lateNightStart || transactionTime <= earlyMorningEnd;

                if (cdt)
                    IsLateNightOrEarlyMorning.Add(1);
                else
                    IsLateNightOrEarlyMorning.Add(0);



            }

            var arrColumn = new DoubleDataFrameColumn("IsLateNightOrEarlyMorning", IsLateNightOrEarlyMorning);
            data.Columns.Add(arrColumn);


            return data;
        }


        public DataFrame IsSimultaneousTrans(DataFrame data)
        {
            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();


            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();


            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            var TimeDiffrences = data.Columns["time_between_transactions_scaled"];

            List<bool> IsSimultaneousTrans = new List<bool>();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];
            double TimeDiff = (double)TimeDiffrences[j];

            double threshold = 20 / 60;

            while (count < (cardNumbersWithAsterisks.Count))
            {


                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    TimeDiff = (double)TimeDiffrences[j];

                    IsSimultaneousTrans.Add(TimeDiff < threshold);
                    j++;
                    //card = cardNumbersWithAsterisks[j];

                }

                else
                {

                    IsSimultaneousTrans.Add(false);
                    i++;
                    card_ = uniqueValues[i]; // Update card_


                }

                //j++;
                count++;

            }

            var column = new BooleanDataFrameColumn("IsSimultaneousTransactions", IsSimultaneousTrans);
            data.Columns.Add(column);

            return data;
        }

        static double CalculateVariance(DataFrame group, string columnName)
        {
            var values = group[columnName];
            var mean = group[columnName].Mean();
            List<double> squaredDifferences = new List<double>();
            foreach (var val in values)
            {
                squaredDifferences.Add(Math.Pow((double)val - mean, 2));
            }
            var variance = squaredDifferences.Sum() / (values.Length - 1);
            return variance;
        }


        public DataFrame STD(DataFrame data)
        {

            var meanValues = data.Columns["Average_Amount"];
            Dictionary<string, int> transactionCount = TransactionsCounts(data);
            Dictionary<string, double> STDs = new Dictionary<string, double>();

            var cards = data.Columns["CardNumber"];
            var Amounts = data.Columns["Amount_scaled"];
            //double std = 0;
            int i = 0;
            foreach (var card in cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                //string key = card1.ToString();
                double amt = (double)Amounts[i];
                if (amt != 0)
                {
                    if (transactionCount[card1.ToString()] > 1)
                    {
                        if (STDs.ContainsKey(card1.ToString()))
                        {
                            STDs[card1.ToString()] += Math.Pow(amt - (double)meanValues[i], 2) / (transactionCount[card1.ToString()] - 1);
                        }
                        else
                        {
                            STDs[card1.ToString()] = Math.Pow(amt - (double)meanValues[i], 2) / (transactionCount[card1.ToString()] - 1);
                        }
                    }
                    else
                    {
                        STDs[card1.ToString()] = 0;
                    }
                }

                card1 = null;



                i++;
            }
            List<double> STDVal = new List<double>();

            foreach (var card in cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (STDs.ContainsKey(card1.ToString()))
                {
                    STDVal.Add(Math.Sqrt(STDs[card1.ToString()]));
                }
                else
                {
                    STDVal.Add(0);
                }
            }
            var STDcol = new DoubleDataFrameColumn("StandardDeviation", STDVal);
            data.Columns.Add(STDcol);



            return data;
        }


        public DataFrame ExpiredCard(DataFrame data)
        {
            List<bool> isExpired = new List<bool>();
            var stat = data.Columns["Statut_scaled"];

            foreach (var st in stat)
            {
                if ((double)st == 101) isExpired.Add(true);
                else isExpired.Add(false);

            }

            var arrColumn = new BooleanDataFrameColumn("ExpiredCard", isExpired);
            data.Columns.Add(arrColumn);

            return data;
        }


        public DataFrame ExpiredCardNew(DataFrame data)
        {
            List<double> isExpired = new List<double>();
            var stat = data.Columns["Statut_scaled"];

            foreach (var st in stat)
            {
                if ((double)st == 101) isExpired.Add(1);
                else isExpired.Add(0);

            }

            var arrColumn = new DoubleDataFrameColumn("ExpiredCard", isExpired);
            data.Columns.Add(arrColumn);

            return data;
        }


        public (List<string>, List<string>) test(DataFrame data)
        {
            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            return (cards, cardNumbersWithAsterisks);
        }


        public DataFrame RepeatedTransactions(DataFrame data)
        {


            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();



            var Amounts = data.Columns["Amount_scaled"];
            var Locations = data.Columns["LocationName_encoded"];
            var TimeDiff = data.Columns["time_between_transactions_scaled"];
            var Type = data.Columns["Type_encoded_scaled"];

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            double Amt = (double)Amounts[j];
            double Loc = (double)Locations[j];
            double Timediff = (double)TimeDiff[j];
            double type = (double)Type[j];


            double AmtP = (double)Amounts[j];
            double LocP = (double)Locations[j];
            double TimediffP = (double)TimeDiff[j];
            double typeP = (double)Type[j];

            List<bool> isRepeated = new List<bool>();
            while (count < (cardNumbersWithAsterisks.Count))
            {


                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    Amt = (double)Amounts[j];
                    Loc = (double)Locations[j];
                    Timediff = (double)TimeDiff[j];
                    type = (double)Type[j];


                    if (Amt == AmtP && LocP == Loc && Timediff <= 2 && Timediff > 0 && typeP == type)
                    {
                        isRepeated.Add(true);
                    }
                    else
                    {
                        isRepeated.Add(false);
                    }


                    AmtP = Amt;
                    LocP = Loc;
                    TimediffP = Timediff;
                    typeP = type;
                    j++;
                    //card = cardNumbersWithAsterisks[j];

                }

                else
                {

                    isRepeated.Add(false);
                    i++;
                    AmtP = Amt;
                    LocP = Loc;
                    TimediffP = Timediff;
                    typeP = type;
                    card_ = uniqueValues[i]; // Update card_


                }

                //j++;
                count++;

            }

            var Time_diff = new BooleanDataFrameColumn("isRepeated", isRepeated);
            data.Columns.Add(Time_diff);
            return data;

        }




        public DataFrame RepeatedTransactionsNew(DataFrame data)
        {


            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();



            var Amounts = data.Columns["Amount_scaled"];
            var Locations = data.Columns["LocationName_encoded"];
            var TimeDiff = data.Columns["time_between_transactions_scaled"];
            var Type = data.Columns["Type_encoded_scaled"];

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            double Amt = (double)Amounts[j];
            double Loc = (double)Locations[j];
            double Timediff = (double)TimeDiff[j];
            double type = (double)Type[j];


            double AmtP = (double)Amounts[j];
            double LocP = (double)Locations[j];
            double TimediffP = (double)TimeDiff[j];
            double typeP = (double)Type[j];

            List<double> isRepeated = new List<double>();
            while (count < (cardNumbersWithAsterisks.Count))
            {


                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    Amt = (double)Amounts[j];
                    Loc = (double)Locations[j];
                    Timediff = (double)TimeDiff[j];
                    type = (double)Type[j];


                    if (Amt == AmtP && LocP == Loc && Timediff <= 2 && Timediff > 0 && typeP == type)
                    {
                        isRepeated.Add(1);
                    }
                    else
                    {
                        isRepeated.Add(0);
                    }


                    AmtP = Amt;
                    LocP = Loc;
                    TimediffP = Timediff;
                    typeP = type;
                    j++;
                    //card = cardNumbersWithAsterisks[j];

                }

                else
                {

                    isRepeated.Add(0);
                    i++;
                    AmtP = Amt;
                    LocP = Loc;
                    TimediffP = Timediff;
                    typeP = type;
                    card_ = uniqueValues[i]; // Update card_


                }

                //j++;
                count++;

            }

            var Time_diff = new DoubleDataFrameColumn("isRepeated", isRepeated);
            data.Columns.Add(Time_diff);
            return data;

        }







        public bool[] RepeatedSequencesPerAtm(DataFrame data, string AtmId)
        {
            var Cards = data.Columns["CardNumber"];

            List<string> cardperATM = new List<string>();
            int j = 0;
            foreach (var card in Cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (data.Columns["AtmID"][j].Equals(AtmId))
                {
                    cardperATM.Add(card1.ToString());
                }
                j++;

                card1 = null;
            }


            Dictionary<List<string>, int> SeenOrder = new Dictionary<List<string>, int>();
            int windowSize = 10;
            if (cardperATM.Count < 10) windowSize = cardperATM.Count / 2;

            for (int i = 0; i <= cardperATM.Count - windowSize; i++)
            {
                // Extract subsequence of length windowSize starting at index i
                List<string> window = cardperATM.GetRange(i, windowSize);

                // Check if window exists in SeenOrder
                bool foundSimilar = false;
                for (int index = 0; index < SeenOrder.Count; index++)
                {
                    var kvp = SeenOrder.ElementAt(index);
                    List<string> key = kvp.Key;
                    double similarityPercentage = CalculateSimilarityPercentage(key, window);
                    if (similarityPercentage >= 0.5)
                    {
                        SeenOrder[key]++;
                        foundSimilar = true;
                    }
                }

                if (!foundSimilar)
                {
                    SeenOrder.Add(window, 1);
                }


            }
            bool[] belongsToOrder = new bool[cardperATM.Count];
            for (int i = 0; i <= cardperATM.Count - windowSize; i++)
            {
                // Extract subsequence of length windowSize starting at index i
                List<string> window = cardperATM.GetRange(i, windowSize);

                if (SeenOrder.ContainsKey(window))
                {
                    if (SeenOrder[window] >= 2)
                    {
                        for (int d = i; d < window.Count; d++)
                        {
                            belongsToOrder[d] = true;
                        }
                    }
                    else
                    {
                        for (int d = i; d < window.Count; d++)
                        {
                            belongsToOrder[d] = false;
                        }
                    }

                }

                else
                {
                    for (int d = i; d < window.Count; d++)
                    {
                        belongsToOrder[d] = false;
                    }
                }

            }



            return belongsToOrder;

        }



        public double[] RepeatedSequencesPerAtmNew(DataFrame data, string AtmId)
        {
            var Cards = data.Columns["CardNumber"];

            List<string> cardperATM = new List<string>();
            int j = 0;
            foreach (var card in Cards)
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (data.Columns["AtmID"][j].Equals(AtmId))
                {
                    cardperATM.Add(card1.ToString());
                }
                j++;


                card1 = null;
            }


            Dictionary<List<string>, int> SeenOrder = new Dictionary<List<string>, int>();
            int windowSize = 10;
            if (cardperATM.Count < 10) windowSize = cardperATM.Count / 2;

            for (int i = 0; i <= cardperATM.Count - windowSize; i++)
            {
                // Extract subsequence of length windowSize starting at index i
                List<string> window = cardperATM.GetRange(i, windowSize);

                // Check if window exists in SeenOrder
                bool foundSimilar = false;
                for (int index = 0; index < SeenOrder.Count; index++)
                {
                    var kvp = SeenOrder.ElementAt(index);
                    List<string> key = kvp.Key;
                    double similarityPercentage = CalculateSimilarityPercentage(key, window);
                    if (similarityPercentage >= 0.5)
                    {
                        SeenOrder[key]++;
                        foundSimilar = true;
                    }
                }

                if (!foundSimilar)
                {
                    SeenOrder.Add(window, 1);
                }


            }
            double[] belongsToOrder = new double[cardperATM.Count];
            for (int i = 0; i <= cardperATM.Count - windowSize; i++)
            {
                // Extract subsequence of length windowSize starting at index i
                List<string> window = cardperATM.GetRange(i, windowSize);

                if (SeenOrder.ContainsKey(window))
                {
                    if (SeenOrder[window] >= 2)
                    {
                        for (int d = i; d < window.Count; d++)
                        {
                            belongsToOrder[d] = 1;
                        }
                    }
                    else
                    {
                        for (int d = i; d < window.Count; d++)
                        {
                            belongsToOrder[d] = 0;
                        }
                    }

                }

                else
                {
                    for (int d = i; d < window.Count; d++)
                    {
                        belongsToOrder[d] = 0;
                    }
                }

            }



            return belongsToOrder;

        }





        public DataFrame belongsToOrder(DataFrame data)
        {
            bool[] belongsToOrder = new bool[data.Rows.Count];



            List<string> Atms = new List<string>();
            foreach (var atm in data.Columns["AtmID"])
            {
                Atms.Add((string)atm);
            }
            List<string> distinctAtms = Atms.Distinct().ToList();

            Dictionary<string, bool[]> perAtm = new Dictionary<string, bool[]>();
            foreach (var atm in distinctAtms)
            {
                perAtm[(string)atm] = RepeatedSequencesPerAtm(data, (string)atm);
            }



            foreach (var AtmId in data.Columns["AtmID"])
            {
                int i = 0;
                if (perAtm.ContainsKey((string)AtmId))
                {
                    belongsToOrder[i] = perAtm[(string)AtmId].Skip(i).Take(1).FirstOrDefault();

                }
            }


            var col = new BooleanDataFrameColumn("is_belongsToOrder", belongsToOrder);
            data.Columns.Add(col);
            return data;
        }




        public DataFrame belongsToOrderNew(DataFrame data)
        {
            double[] belongsToOrder = new double[data.Rows.Count];



            List<string> Atms = new List<string>();
            foreach (var atm in data.Columns["AtmID"])
            {
                Atms.Add((string)atm);
            }
            List<string> distinctAtms = Atms.Distinct().ToList();

            Dictionary<string, double[]> perAtm = new Dictionary<string, double[]>();
            foreach (var atm in distinctAtms)
            {
                perAtm[(string)atm] = RepeatedSequencesPerAtmNew(data, (string)atm);
            }



            foreach (var AtmId in data.Columns["AtmID"])
            {
                int i = 0;
                if (perAtm.ContainsKey((string)AtmId))
                {
                    belongsToOrder[i] = perAtm[(string)AtmId].Skip(i).Take(1).FirstOrDefault();

                }
            }


            var col = new DoubleDataFrameColumn("is_belongsToOrder", belongsToOrder);
            data.Columns.Add(col);
            return data;
        }



        private double CalculateSimilarityPercentage(List<string> list1, List<string> list2)
        {
            var set1 = new HashSet<string>(list1);
            var set2 = new HashSet<string>(list2);

            int intersectionCount = set1.Intersect(set2).Count();
            int unionCount = set1.Union(set2).Count();

            return (double)intersectionCount / unionCount;
        }

        public DataFrame Data_scalingI(DataFrame data, string col)
        {
            var column = data.Columns[col];
            double mean = column.Mean();
            //double std = 0;
            List<double> scaled_values = new List<double>();
            foreach (int v in column)
            {
                scaled_values.Add(v);
                //double vv = (double)v;
                //std += (vv - mean) * (vv - mean);
            }
            /*std = Math.Sqrt(std / (column.Length - 1));

            foreach (int col_ in column)
            {
                if (std != 0)
                {
                    double col__ = (double)col_;
                    double scaledValue = (col__ - mean) / std;
                    scaled_values.Add(scaledValue);
                }
                else
                {
                    scaled_values.Add(0);
                }

            }*/
            var arrColumn = new DoubleDataFrameColumn(col + "_scaled", scaled_values);
            data.Columns.Add(arrColumn);
            data.Columns.Remove(col);

            return data;
        }



        public DataFrame Data_scalingD(DataFrame data, string col)
        {
            var column = data.Columns[col];
            double mean = column.Mean();
            //double std = 0;
            List<double> scaled_values = new List<double>();
            foreach (double v in column)
            {
                scaled_values.Add(v);
                //double vv = (double)v;
                //std += (vv - mean) * (vv - mean);
            }
            /*
            std = Math.Sqrt(std / (column.Length - 1));

            foreach (double col_ in column)
            {
                double col__ = (double)col_;
                double scaledValue = (col__ - mean) / std;
                scaled_values.Add(scaledValue);
            }*/
            var arrColumn = new DoubleDataFrameColumn(col + "_scaled", scaled_values);
            data.Columns.Add(arrColumn);
            data.Columns.Remove(col);

            return data;
        }


        public DataFrame Convert_boolToInt(DataFrame data, string col)
        {
            var column = data.Columns[col];
            List<int> boolvalues = new List<int>();
            foreach (var item in column)
            {
                var boolValue = (bool)item;
                int intValue = boolValue ? 1 : 0;
                boolvalues.Add(intValue);
            }



            var arrColumn = new Int32DataFrameColumn(col + "_toInt", boolvalues);
            data.Columns.Add(arrColumn);
            data.Columns.Remove(col);

            return data;
        }

        public DataFrame Convert_boolTofloat(DataFrame data, string col)
        {
            var column = data.Columns[col];
            List<double> boolvalues = new List<double>();
            foreach (var item in column)
            {
                var boolValue = (bool)item;
                double intValue = boolValue ? 1 : 0;
                boolvalues.Add(intValue);
            }



            var arrColumn = new DoubleDataFrameColumn(col + "_tofloat", boolvalues);
            data.Columns.Add(arrColumn);
            data.Columns.Remove(col);

            return data;
        }

        //########################Fraud_cases##################################################################


        public DataFrame is_PIN_Fraud(DataFrame data)
        {
            // Set the 'is_PIN_fraud' column to false for all rows
            List<bool> is_PIN_fraud = new List<bool>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var n_PIN_attemptsColumn = data.Columns["n_PIN_attempts"];
            foreach (int pin_attem in n_PIN_attemptsColumn)
            {
                //Console.WriteLine(pin_attem);
                if (pin_attem != 1)
                {

                    is_PIN_fraud.Add(true);
                }
                else
                {
                    is_PIN_fraud.Add(false);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_PIN_Fraud", is_PIN_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_PIN_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }



        public DataFrame is_RC_Fraud(DataFrame data)
        {
            // Set the 'is_PIN_fraud' column to false for all rows
            List<bool> is_RC_fraud = new List<bool>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var status = data.Columns["Statut_scaled"];
            foreach (double stat in status)
            {
                //Console.WriteLine(stat);
                if (stat == 1)
                {

                    is_RC_fraud.Add(true);
                }
                else
                {
                    is_RC_fraud.Add(false);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_RC_Fraud", is_RC_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_RC_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }

        public DataFrame is_RC_FraudNew(DataFrame data)
        {
            // Set the 'is_PIN_fraud' column to false for all rows
            List<double> is_RC_fraud = new List<double>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var status = data.Columns["Statut_scaled"];
            foreach (double stat in status)
            {
                //Console.WriteLine(stat);
                if (stat == 1)
                {

                    is_RC_fraud.Add(1);
                }
                else
                {
                    is_RC_fraud.Add(0);
                }
            }
            var arrColumn = new DoubleDataFrameColumn("is_RC_Fraud", is_RC_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_RC_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }

        public DataFrame is_IF_Fraud(DataFrame data)
        {

            List<bool> is_IF_fraud = new List<bool>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var column = data.Columns["isCashoutError_tofloat"];
            foreach (double col in column)
            {
                //Console.WriteLine(col);
                if (col == 1)
                {

                    is_IF_fraud.Add(true);
                }
                else
                {
                    is_IF_fraud.Add(false);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_IF_Fraud", is_IF_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_IF_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }


        public DataFrame is_IF_FraudNew(DataFrame data)
        {

            List<double> is_IF_fraud = new List<double>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var column = data.Columns["isCashoutError_tofloat"];
            foreach (double col in column)
            {
                //Console.WriteLine(col);
                if (col == 1)
                {

                    is_IF_fraud.Add(1);
                }
                else
                {
                    is_IF_fraud.Add(0);
                }
            }
            var arrColumn = new DoubleDataFrameColumn("is_IF_Fraud", is_IF_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_IF_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }


        public DataFrame is_CVM_Fraud(DataFrame data)
        {
            List<bool> is_CVM_fraud = new List<bool>();
            // Set 'is_PIN_fraud' to true where 'n_PIN_attempts' is not equal to 1
            var column = data.Columns["cvm_methods"];
            foreach (int col in column)
            {
                //Console.WriteLine(col);
                if (col == 3)
                {
                    is_CVM_fraud.Add(false);
                }
                else
                {
                    is_CVM_fraud.Add(true);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_CVM_Fraud", is_CVM_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_CVM_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }

        public DataFrame is_expDate_Fraud(DataFrame data)
        {
            List<bool> is_expDate_fraud = new List<bool>();
            var column = data.Columns["ExpiryDifference"];
            foreach (double col in column)
            {
                //Console.WriteLine(col);
                if (col >= 0)
                {

                    is_expDate_fraud.Add(false);
                }
                else
                {
                    is_expDate_fraud.Add(true);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_expDate_Fraud", is_expDate_fraud);
            data.Columns.Add(arrColumn);

            /*foreach (var m in data.Columns["is_expDate_fraud"])
            {
                Console.WriteLine(m);
            }*/
            return data;
        }

        public (List<double>, List<double>) calcule_diffOLD(DataFrame data)
        {
            DataFrame dataframe = data.GroupBy("CardNumber").Mean("Amount");
            DataFrameColumn meanData = dataframe.Columns[1];
            DataFrameColumn uniquecards = dataframe.Columns[0];
            var cardColumn = data.Columns["CardNumber"];
            var Amounts = data.Columns["Amount"];
            List<double> Amount_diff = new List<double>();
            List<double> Amount_moy = new List<double>();
            int i = 0;
            int j = 0;
            var card_ = uniquecards[i];


            foreach (var card in cardColumn)
            {

                int amount = (int)Amounts[j];
                double meanAm;
                double diff;
                if (card.Equals(card_))
                {
                    meanAm = (double)meanData[i];
                    diff = (amount - meanAm);
                    Amount_diff.Add(diff);

                }

                else
                {
                    i++;
                    card_ = uniquecards[i]; // Update card_
                    meanAm = (double)meanData[i];
                    diff = (amount - meanAm);
                    Amount_diff.Add(diff);


                }
                Amount_moy.Add(meanAm);
                j++;

            }
            /*Console.WriteLine("!!!!!!!!!!!!!!!!!!!!meandata");
            Console.WriteLine(data.Head(30));
            for (i = 0; i < 30; i++)
            {
                Console.WriteLine(Amount_diff[i]);
                //Console.WriteLine(Amounts[i]);
                Console.WriteLine("\n");
            }

            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");*/
            return (Amount_diff, Amount_moy);
        }

        public (List<double>, List<double>) calcule_diff(DataFrame data)
        {
            DataFrame dataframe = data.GroupBy("CardNumber").Mean("Amount_scaled");
            DataFrameColumn meanData = dataframe.Columns[1];
            DataFrameColumn uniquecards = dataframe.Columns[0];

            var Amounts = data.Columns["Amount_scaled"];
            List<double> Amount_diff = new List<double>();
            List<double> Amount_moy = new List<double>();

            var cardColumn = data.Columns["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }

            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();



            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();



            List<string> cardNumbersWithAsterisks = sortedList.Select(car => car.Length >= 4 ? $"{car.Substring(0, 4)}******{car.Substring(4)}" : car).ToList();


            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];

            while (count < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    double meanAm = (double)meanData[i];
                    double amount = (double)Amounts[j];
                    double diff = amount - meanAm;
                    Amount_diff.Add(diff);
                    Amount_moy.Add(meanAm);
                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {
                    Amount_diff.Add(0);
                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;
            }

            return (Amount_diff, Amount_moy);
        }

        //call before scaling amount and ecode cardnumber
        public DataFrame is_HM_Fraud(DataFrame data)
        {
            var results = calcule_diff(data);
            List<double> Amount_diff = results.Item1;
            //List<double> Amount_moy=results.Item2;
            var Amounts = data.Columns["Amount_scaled"];
            List<bool> is_HM_fraud = new List<bool>();


            for (int i = 0; i < Amounts.Length; i++)
            {

                double cdt = Amount_diff[i] / 2;
                if (Math.Abs(Amount_diff[i]) > 2000) is_HM_fraud.Add(true);
                else is_HM_fraud.Add(false);

            }
            var arrColumn = new BooleanDataFrameColumn("is_HM_Fraud", is_HM_fraud);
            data.Columns.Add(arrColumn);

            return data;

        }

        public DataFrame is_HM_FraudNew(DataFrame data)
        {
            var results = calcule_diff(data);
            List<double> Amount_diff = results.Item1;
            //List<double> Amount_moy=results.Item2;
            var Amounts = data.Columns["Amount_scaled"];
            List<double> is_HM_fraud = new List<double>();


            for (int i = 0; i < Amounts.Length; i++)
            {

                double cdt = Amount_diff[i] / 2;
                if (Math.Abs(Amount_diff[i]) > 2000) is_HM_fraud.Add(1);
                else is_HM_fraud.Add(0);

            }
            var arrColumn = new DoubleDataFrameColumn("is_HM_Fraud", is_HM_fraud);
            data.Columns.Add(arrColumn);

            return data;

        }

        public DataFrame is_TimeDiff_Fraud(DataFrame data)
        {
            var time_fiff = data.Columns["time_between_transactions_scaled"];
            List<bool> is_TD_Fraud = new List<bool>();

            for (int i = 0; i < time_fiff.Length; i++)
            {
                double timeDiff = (double)time_fiff[i];
                if (timeDiff < 0.5 && timeDiff > 0)
                {
                    is_TD_Fraud.Add(true);

                }
                else
                {
                    is_TD_Fraud.Add(false);
                }
            }
            var arrColumn = new BooleanDataFrameColumn("is_TimeDiff_Fraud", is_TD_Fraud);
            data.Columns.Add(arrColumn);

            return data;

        }


        public DataFrame is_TimeDiff_FraudNew(DataFrame data)
        {
            var time_fiff = data.Columns["time_between_transactions_scaled"];
            List<double> is_TD_Fraud = new List<double>();

            for (int i = 0; i < time_fiff.Length; i++)
            {
                double timeDiff = (double)time_fiff[i];
                if (timeDiff < 0.5 && timeDiff > 0)
                {
                    is_TD_Fraud.Add(1);

                }
                else
                {
                    is_TD_Fraud.Add(0);
                }
            }
            var arrColumn = new DoubleDataFrameColumn("is_TimeDiff_Fraud", is_TD_Fraud);
            data.Columns.Add(arrColumn);

            return data;

        }


        public Dictionary<string, List<double>> aux(DataFrame data)
        {

            //var cardColumn = data.GetColumn<string>("CardNumber");
            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)} ****** {car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            var Transactiondates = data.Columns["TransactionDate"];
            DateTime previousTransactionDate = (DateTime)Transactiondates[0];
            List<bool> is_UH_Fraud = new List<bool>();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];
            DateTime transactionDate = (DateTime)Transactiondates[j];

            Dictionary<string, List<double>> transactionDictionary = new Dictionary<string, List<double>>();



            while (count < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    if (transactionDictionary.ContainsKey(card))
                    {
                        // Add the transaction date to the existing list
                        transactionDictionary[card].Add(transactionDate.Hour);
                    }
                    else
                    {
                        // Create a new list with the transaction date for the card number
                        List<double> datesList = new List<double> { transactionDate.Hour };
                        transactionDictionary.Add(card, datesList);
                    }

                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;



            }

            return transactionDictionary;
        }

        public Dictionary<string, List<double>> auxNew(DataFrame data)
        {
            var Transactiondates = data.Columns["TransactionDate"];
            List<bool> is_UH_Fraud = new List<bool>();

            int i = 0;
            Dictionary<string, List<double>> transactionDictionary = new Dictionary<string, List<double>>();


            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (transactionDictionary.ContainsKey(card1.ToString()))
                {
                    transactionDictionary[card1.ToString()].Add(((DateTime)Transactiondates[i]).Hour);
                }
                else
                {
                    List<double> datesList = new List<double> { ((DateTime)Transactiondates[i]).Hour };
                    transactionDictionary.Add(card1.ToString(), datesList);
                }

                i++;

                card1 = null;
            }

            return transactionDictionary;
        }


        public DataFrame is_UnusualHour_Fraud(DataFrame data)
        {
            // Call the aux function to create the transaction dictionary
            Dictionary<string, List<double>> transactionDictionary = auxNew(data);

            // Assuming "CardNumber" and "TransactionDate" are column names
            var cardColumn = data.Columns["CardNumber"];
            var transactionDates = data.Columns["TransactionDate"];

            List<bool> isUnusualHourFraud = new List<bool>();

            for (int i = 0; i < data.Rows.Count; i++)
            {

                StringBuilder cardNumber = new  StringBuilder((string)cardColumn[i]);


                DateTime currentTransactionDate = (DateTime)transactionDates[i];
                int currentTransactionHour = currentTransactionDate.Hour;

                // Check if the card number is in the transaction dictionary
                if (transactionDictionary.TryGetValue(cardNumber.ToString(), out List<double> historicalTransactionHours))
                {
                    // Calculate the mean and standard deviation of historical transaction hours
                    double meanHour = historicalTransactionHours.Average();
                    double stdDevHour = CalculateStandardDeviation(historicalTransactionHours);


                    double threshold = 2 * stdDevHour;


                    bool isUnusualHour = Math.Abs(currentTransactionHour - meanHour) > threshold;
                    isUnusualHourFraud.Add(isUnusualHour);
                }
                else
                {
                    // If there is no historical data for the card, consider it not unusual
                    isUnusualHourFraud.Add(false);
                }

                cardNumber = null;
            }

            // Add a new column to the DataFrame indicating if the transaction hour is unusual
            var arrColumn = new BooleanDataFrameColumn("is_UnusualHour_Fraud", isUnusualHourFraud);
            data.Columns.Add(arrColumn);

            return data;
        }


        public DataFrame is_UnusualHour_FraudNew(DataFrame data)
        {
            // Call the aux function to create the transaction dictionary
            Dictionary<string, List<double>> transactionDictionary = auxNew(data);

            // Assuming "CardNumber" and "TransactionDate" are column names
            var cardColumn = data.Columns["CardNumber"];
            var transactionDates = data.Columns["TransactionDate"];

            List<double> isUnusualHourFraud = new List<double>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                StringBuilder cardNumber = new StringBuilder((string)cardColumn[i]);
                DateTime currentTransactionDate = (DateTime)transactionDates[i];
                int currentTransactionHour = currentTransactionDate.Hour;

                // Check if the card number is in the transaction dictionary
                if (transactionDictionary.TryGetValue(cardNumber.ToString(), out List<double> historicalTransactionHours))
                {
                    // Calculate the mean and standard deviation of historical transaction hours
                    double meanHour = historicalTransactionHours.Average();
                    double stdDevHour = CalculateStandardDeviation(historicalTransactionHours);


                    double threshold = 2 * stdDevHour;


                    bool isUnusualHour = Math.Abs(currentTransactionHour - meanHour) > threshold;
                    if (isUnusualHour)
                        isUnusualHourFraud.Add(1);
                    else
                        isUnusualHourFraud.Add(0);
                }
                else
                {
                    // If there is no historical data for the card, consider it not unusual
                    isUnusualHourFraud.Add(0);
                }

                cardNumber = null;
            }

            // Add a new column to the DataFrame indicating if the transaction hour is unusual
            var arrColumn = new DoubleDataFrameColumn("is_UnusualHour_Fraud", isUnusualHourFraud);
            data.Columns.Add(arrColumn);

            return data;
        }




        // Helper method to calculate standard deviation
        private double CalculateStandardDeviation(List<double> values)
        {
            double mean = values.Average();
            double variance = values.Sum(v => Math.Pow(v - mean, 2)) / (values.Count - 1);
            return Math.Sqrt(variance);
        }




        public Dictionary<string, List<string>> aux1(DataFrame data)
        {

            //var cardColumn = data.GetColumn<string>("CardNumber");
            var cardColumn = data["CardNumber"];
            List<string> cards = new List<string>();
            foreach (var c in cardColumn)
            {
                cards.Add((string)c);
            }
            List<string> cleanedCardNumbers = cards.Select(selector => cleanCardNumber(selector)).ToList();
            List<string> numericStrings = cleanedCardNumbers.Where(s => long.TryParse(s, out _)).ToList();

            // Sort the cleaned card numbers
            List<string> sortedList = numericStrings.OrderBy(ca => Int64.Parse(ca)).ToList();

            // Add asterisks to the sorted card numbers
            List<string> cardNumbersWithAsterisks = sortedList.Select(car => $"{car.Substring(0, 4)}******{car.Substring(4)}").ToList();
            List<string> uniqueValues = cardNumbersWithAsterisks.Distinct().ToList();

            var Atms = data.Columns["AtmID"];

            List<bool> is_Loc_Fraud = new List<bool>();

            int i = 0;
            int j = 0;
            int count = 0;
            var card_ = uniqueValues[i];
            var card = cardNumbersWithAsterisks[j];
            String AtmID = (String)Atms[j];

            Dictionary<string, List<string>> transactionDictionary = new Dictionary<string, List<string>>();



            while (count < cardNumbersWithAsterisks.Count)
            {

                //Console.WriteLine(card + " and " + card_);

                card = cardNumbersWithAsterisks[j];
                if (card != null && card_.Equals(card))
                {
                    if (transactionDictionary.ContainsKey(card))
                    {
                        // Add the transaction date to the existing list
                        transactionDictionary[card].Add(AtmID);
                    }
                    else
                    {
                        // Create a new list with the transaction date for the card number
                        List<string> datesList = new List<string> { AtmID };
                        transactionDictionary.Add(card, datesList);
                    }

                    j++;
                    //Console.WriteLine("yes");
                }
                else
                {

                    i++;
                    card_ = uniqueValues[i]; // Update card_
                    //Console.WriteLine("no");
                }

                count++;



            }

            return transactionDictionary;
        }

        public Dictionary<string, List<string>> aux2(DataFrame data)
        {
            var Atms = data.Columns["AtmID"];

            List<bool> is_Loc_Fraud = new List<bool>();



            Dictionary<string, List<string>> transactionDictionary = new Dictionary<string, List<string>>();

            int j = 0;

            foreach (var card in data.Columns["CardNumber"])
            {
                StringBuilder card1 = new StringBuilder((string)card);
                if (transactionDictionary.ContainsKey(card1.ToString()))
                {
                    // Add the transaction date to the existing list
                    transactionDictionary[card1.ToString()].Add((string)Atms[j]);
                }
                else
                {
                    // Create a new list with the transaction date for the card number
                    List<string> datesList = new List<string> { (string)Atms[j] };
                    transactionDictionary.Add(card1.ToString(), datesList);
                }
                card1 = null;
                j++;
            }

            return transactionDictionary;
        }
        public DataFrame is_Location_Fraud(DataFrame data)
        {

            // Call the aux function to create the transaction dictionary
            Dictionary<string, List<string>> transactionDictionary = aux2(data);

            // Assuming "CardNumber" and "TransactionDate" are column names
            var cardColumn = data.Columns["CardNumber"];
            var Atms = data.Columns["AtmID"];

            List<bool> isLocationFraud = new List<bool>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                StringBuilder cardNumber = new StringBuilder((string)cardColumn[i]);
                
                string currentAtm = (string)Atms[i];
                //int currentTransactionHour = currentTransactionDate.Hour;


                if (transactionDictionary.TryGetValue(cardNumber.ToString(), out List<string> historicalTransactionAtms))
                {



                    bool isUnusualLocation = !historicalTransactionAtms.Contains(currentAtm);
                    isLocationFraud.Add(isUnusualLocation);
                }
                else
                {
                    // If there is no historical data for the card, consider it not unusual
                    isLocationFraud.Add(false);
                }
                cardNumber = null;
            }

            // Add a new column to the DataFrame indicating if the transaction hour is unusual
            var arrColumn = new BooleanDataFrameColumn("is_Location_Fraud", isLocationFraud);
            data.Columns.Add(arrColumn);

            return data;
        }

        public DataFrame is_Location_FraudNew(DataFrame data)
        {

            // Call the aux function to create the transaction dictionary
            Dictionary<string, List<string>> transactionDictionary = aux2(data);

            // Assuming "CardNumber" and "TransactionDate" are column names
            var cardColumn = data.Columns["CardNumber"];
            var Atms = data.Columns["AtmID"];

            List<double> isLocationFraud = new List<double>();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                StringBuilder cardNumber = new StringBuilder((string)cardColumn[i]);
                string currentAtm = (string)Atms[i];
                //int currentTransactionHour = currentTransactionDate.Hour;


                if (transactionDictionary.TryGetValue(cardNumber.ToString(), out List<string> historicalTransactionAtms))
                {



                    bool isUnusualLocation = !historicalTransactionAtms.Contains(currentAtm);
                    if (isUnusualLocation)
                        isLocationFraud.Add(1);
                    else
                        isLocationFraud.Add(0);
                }
                else
                {
                    // If there is no historical data for the card, consider it not unusual
                    isLocationFraud.Add(0);
                }
                cardNumber = null;
            }

            // Add a new column to the DataFrame indicating if the transaction hour is unusual
            var arrColumn = new DoubleDataFrameColumn("is_Location_Fraud", isLocationFraud);
            data.Columns.Add(arrColumn);

            return data;
        }



        public DataFrame is_ExpCardFraud(DataFrame data)
        {
            var status = data.Columns["Statut"];
            List<bool> is_ExpCard = new List<bool>();
            foreach (var stat in status)
            {
                if ((double)stat == 101) is_ExpCard.Add(true);
                else is_ExpCard.Add(false);
            }
            var arrColumn = new BooleanDataFrameColumn("is_ExpCardFraud", is_ExpCard);
            data.Columns.Add(arrColumn);

            return data;
        }




        public DataFrame IsFraud(DataFrame data)
        {
            DataFrame train_data = data.Clone();


            train_data = is_HM_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : is_HM_Fraud is done");
            //train_data = is_expDate_Fraud(train_data);
            //train_data = is_CVM_Fraud(train_data);
            train_data = is_IF_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures :is_IF_Fraud is done");
            //train_data = is_PIN_Fraud(train_data);
            train_data = is_RC_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures :is_RC_Fraud is done");
            train_data = is_TimeDiff_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : is_TimeDiff_Fraud is done");
            train_data = is_UnusualHour_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures :is_UnusualHour_Fraud is done");
            train_data = is_Location_FraudNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : is_Location_Fraud is done");

            //train_data = is_ExpCardFraud(train_data);
            train_data = PIN_ErrorNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : PIN_Error is done");
            train_data = TimeDistanceConsistencyNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : TimeDistanceConsistency is done");
            train_data = ExpiredCardNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : ExpiredCard is done");
            train_data = NbrDeclinedTransNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : NbrDeclinedTrans is done");
            train_data = MultiplePINEntryNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures :MultiplePINEntry is done");
            train_data = IsLateNightOrEarlyMorningNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures :IsLateNightOrEarlyMorning  is done");
            train_data = FrequencyChangeNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : FrequencyChange is done");
            train_data = RepeatedTransactionsNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : RepeatedTransactions is done");
            train_data = belongsToOrderNew(train_data);
            //ExceptionLogger.Info("function CalculeFeatures : belongsToOrder is done");


            string[] subTargets = { "is_HM_Fraud" ,
                                    "is_RC_Fraud" ,
                                    "is_IF_Fraud" ,
                                    "PINError",
                                    "is_TimeDiff_Fraud",
                                    "is_UnusualHour_Fraud",
                                    "is_Location_Fraud",
                                    "isnot_Consistent",
                                    "ExpiredCard",
                                    "ManyDeclinedTransactions",
                                    "isMultiplePINEntry",
                                    "IsLateNightOrEarlyMorning",
                                    "FrequencyChange",
                                    "isRepeated",
                                    "is_belongsToOrder",};



            List<bool> is_Fraud = new List<bool>();

            for (int i = 0; i < train_data.Rows.Count; i++)
            {
                int count = 0;

                foreach (string subTarget in subTargets)
                {
                    double aux = (double)train_data.Columns[subTarget][i];
                    if (aux == 1)
                    {
                        count++;
                        if (count > 2)
                        {
                            is_Fraud.Add(true);
                            break;
                        }

                    }
                }

                if (count <= 2)
                {
                    is_Fraud.Add(false);
                }
            }

            var arrColumn = new BooleanDataFrameColumn("is_Fraud", is_Fraud);
            train_data.Columns.Add(arrColumn);

            return train_data;
        }
        /*

        public DataFrame IsFraud(DataFrame data)
        {
            DataFrame train_data = data.Clone();
            var subTargets = new List<Func<DataFrame, DataFrame>>()
            {
                is_HM_Fraud,
                is_expDate_Fraud,
                is_CVM_Fraud,
                is_IF_Fraud,
                is_PIN_Fraud,
                is_RC_Fraud
            };

            var is_Fraud = train_data.Rows
                .Select(row => subTargets.Select(subTarget => (bool)row[subTarget.Method.]))
                .Select(rowResults => rowResults.Count(result => result) > 2)
                .ToList();

            var arrColumn = new BooleanDataFrameColumn("is_Fraud", is_Fraud);
            train_data.Columns.Add(arrColumn);

            return train_data;
        }

        */


        public DataFrame CalculeFeatures(DataFrame training_data)
        {
            //DataFrame training_data = GetData();
            /*training_data = DataSorting(training_data);
            ExceptionLogger.Info("function CalculeFeatures :DataSorting is done");
            //DataFrame id_data = new DataFrame(training_data.Columns.Where(col => Id_columns.Contains(col.Name)).ToList());

            training_data = Time_between_trans(training_data);
            ExceptionLogger.Info("function CalculeFeatures :Time_between_trans is done");


            training_data = Velocity_per_hour(training_data);
            ExceptionLogger.Info("function CalculeFeatures :Velocity_per_hour is done");*/
            //training_data = AddRandomPINAttempts(training_data);

            //training_data = Cvm_methods(training_data);
            //training_data = IssuerCity(training_data);

            //training_data = Experation_date(training_data);
            //training_data = Scale_exp(training_data);


            try
            {
                training_data = DataSorting(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : DataSorting is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in DataSorting: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = Time_between_trans(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : Time_between_trans is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in Time_between_trans: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = Velocity_per_hour(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : Velocity_per_hour is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in Velocity_per_hour: " + ex.Message + ex.InnerException.Message);
            }


            string[] columnNamesB = new string[]
            {
                    "isCashPresented",
                    "isCashTaken",
                    "isCashRetracted",
                    "isCashoutError",
                    "ExistInHost"
            };

            foreach (string column in columnNamesB)
            {


                try
                {
                    training_data = Convert_boolTofloat(training_data, column);
                    //ExceptionLogger.Info("function CalculeFeatures : Convert_boolTofloat" + column + " is done");
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Info("Exeption infunction CalculeFeatures : Convert_boolTofloat" + column + ex.Message + ex.InnerException.Message);
                }


            }


            //training_data.Columns.Remove("ExpiryDifference");
            // Label Encoding
            //training_data = Label_Encoding(training_data, "Issuer_cities");


            try
            {
                training_data = Label_Encoding(training_data, "CardNumber");
                //ExceptionLogger.Info("function CalculeFeatures : Label_Encoding(CardNumber is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exeption in Label_Encoding(CardNumber)" + ex.Message + ex.InnerException.Message);
            }


            try
            {
                training_data = Label_Encoding(training_data, "Type");
                //ExceptionLogger.Info("function CalculeFeatures :Label_Encoding(Type is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exeption in Label_Encoding(Type)" + ex.Message + ex.InnerException.Message);
            }




            // Scaling the Data
            string[] columnNamesI = new string[]
            {
                 //"TransactionNumber",
                 "Type_encoded",
                 "Amount",
                 "Statut",
                 "CardNumber_encoded"
                 /*"n_PIN_attempts",
                 "cvm_methods",
                 "Issuer_cities_encoded"*/
            };

            string[] columnNamesD = new string[]
            {
                 "time_between_transactions",
                 "frequecy_per_hour"
            };

            foreach (string column in columnNamesI)
            {


                try
                {
                    training_data = Data_scalingI(training_data, column);
                    //ExceptionLogger.Info("function CalculeFeatures : Data_scalingI(" + column + " is done");
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Info("Exeption in Data_scalingI(" + column + ex.Message + ex.InnerException.Message);
                }
            }

            foreach (string column in columnNamesD)
            {


                try
                {
                    training_data = Data_scalingD(training_data, column);
                    //ExceptionLogger.Info("function CalculeFeatures : Data_scalingD(" + column + " is done");
                }
                catch (Exception ex)
                {
                    ExceptionLogger.Info("Exeption in Data_scalingD(" + column + ex.Message + ex.InnerException.Message);
                }
            }

            // Remove Columns
            //training_data.Columns.Remove("TransactionNumber_scaled");
            //training_data.Columns.Remove("TransactionDate");
            //training_data.Columns.Remove("ExpiryDate");


            try
            {
                training_data.Columns.Remove("IsRejected");
                //ExceptionLogger.Info("function CalculeFeatures : Remove(IsRejected) is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exeption inRemove(IsRejected)" + ex.Message + ex.InnerException.Message);
            }




            try
            {
                training_data = averageAmountNew(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : averageAmountNew is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in averageAmountNew: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = AccumulatedAmountNew(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : AccumulatedAmountNew is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in AccumulatedAmountNew: " + ex.Message + ex.InnerException.Message);
            }




            try
            {
                training_data = MinMaxNew(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : MinMaxNew is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in MinMaxNew: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = AtmLocation(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : AtmLocation is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in AtmLocation: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = Dst_between_Trans(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : Dst_between_Trans is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in Dst_between_Trans: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = Speed(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : Speed is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in Speed: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = AverageSpeedNew(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : AverageSpeedNew is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in AverageSpeedNew: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = VelocityPerLocationNew(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : VelocityPerLocationNew is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in VelocityPerLocationNew: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = STD(training_data);
                //ExceptionLogger.Info("function CalculeFeatures : STD is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in STD: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = LocationNameEncoder(training_data);
                ExceptionLogger.Info("function CalculeFeatures : LocationNameEncoder is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in LocationNameEncoder: " + ex.Message + ex.InnerException.Message);
            }

            try
            {
                training_data = IsFraud(training_data);
                ExceptionLogger.Info("function CalculeFeatures : IsFraud is done");
            }
            catch (Exception ex)
            {
                ExceptionLogger.Info("Exception in IsFraud: " + ex.Message + ex.InnerException.Message);
            }



            /*
            //training_data = ExpiredCard(training_data);
            //training_data = PIN_Error(training_data);
            //training_data = averageAmount(training_data);
            training_data = averageAmountNew(training_data);
            ExceptionLogger.Info("function CalculeFeatures : averageAmountNew is done");
            //training_data = AccumulatedAmount(training_data);
            training_data = AccumulatedAmountNew(training_data);
            ExceptionLogger.Info("function CalculeFeatures : AccumulatedAmountNew is done");
            //training_data = MinMax(training_data);
            training_data = MinMaxNew(training_data);
            ExceptionLogger.Info("function CalculeFeatures : MinMaxNew is done");
            training_data = AtmLocation(training_data);
            ExceptionLogger.Info("function CalculeFeatures : AtmLocation is done");
            training_data = Dst_between_Trans(training_data);
            ExceptionLogger.Info("function CalculeFeatures :Dst_between_Trans is done");
            training_data = Speed(training_data);
            ExceptionLogger.Info("function CalculeFeatures : Speed is done");
            //training_data = AverageSpeed(training_data);
            training_data = AverageSpeedNew(training_data);
            ExceptionLogger.Info("function CalculeFeatures :AverageSpeedNew is done");


            training_data = VelocityPerLocationNew(training_data);
            ExceptionLogger.Info("function CalculeFeatures : VelocityPerLocationNew is done");


            training_data = STD(training_data);
            ExceptionLogger.Info("function CalculeFeatures : STD is done");
            //training_data = TimeDistanceConsistency(training_data);
            //training_data = NbrDeclinedTrans(training_data);
            //training_data = MultiplePINEntry(training_data);
            //training_data = IsLateNightOrEarlyMorning(training_data);

            //training_data = FrequencyChange(training_data);
            //training_data = RepeatedTransactions(training_data);
            //training_data = belongsToOrder(training_data);

            training_data = LocationNameEncoder(training_data);
            ExceptionLogger.Info("function CalculeFeatures :LocationNameEncoder is done");

            training_data = IsFraud(training_data);
            ExceptionLogger.Info("function CalculeFeatures : IsFraud is done");*/
            return training_data;
        }
    }
}
