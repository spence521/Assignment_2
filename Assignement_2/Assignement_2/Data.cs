using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assignement_2
{
    public class Data
    {
        public List<Entry> Training_Data { get; private set; }
        public List<Entry> Test_Data { get; private set; }
        public Dictionary<int, AccuracyWB> AccuracyWeightB { get; private set; }
        public Perceptron perceptron { get; set; }
        public double Accuracy { get; set; }
        public double Learning_Rate { get; set; }
        public double Margin { get; set; }
        public double Majority { get; set; }
        public WeightBias BestWeightBias { get; set; }
        public Data(StreamReader r1, StreamReader r2, StreamReader r3, StreamReader r4, StreamReader r5, int epochs, 
            double learning_rate, Random r, bool DymanicLearningRate, double margin, bool Average, bool Aggressive)
        {
            double[] w_average = new double[68];
            double b_average;
            WeightBias wb_average = null;
            if (Average)
            {
                for (int i = 0; i < 68; i++)
                {
                    double randomNumber = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                    w_average[i] = randomNumber;
                }
                b_average = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                wb_average = new WeightBias(w_average, b_average, 0);
            }

            double temp_accuracy1;
            double temp_accuracy2;
            double temp_accuracy3;
            double temp_accuracy4;
            double temp_accuracy5;
            Learning_Rate = learning_rate;
            Margin = margin;
            #region First Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r5);
            SetData(r2);
            SetData(r3);
            SetData(r4);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            double[] w = new double[68];
            double b = (r.NextDouble() * (0.01 + 0.01) - 0.01);
            for (int i = 0; i < 68; i++)
            {
                double randomNumber = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                w[i] = randomNumber;
            }
            WeightBias wb = new WeightBias(w, b, 0);            
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy1 = perceptron.GetAccuracy(Test_Data, wb);
            if(Average) { temp_accuracy1 = perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average); }
            #endregion

            #region Second Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r4);
            SetData(r2);
            SetData(r3);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy2 = perceptron.GetAccuracy(Test_Data, wb);
            if (Average) { temp_accuracy2 = perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average); }
            #endregion

            #region Third Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r3);
            SetData(r2);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy3 = perceptron.GetAccuracy(Test_Data, wb);
            if (Average) { temp_accuracy3 = perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average); }
            #endregion

            #region Fourth Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r2);
            SetData(r3);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy4 = perceptron.GetAccuracy(Test_Data, wb);
            if (Average) { temp_accuracy4 = perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average); }
            #endregion

            #region Fifth Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r2, r1);
            SetData(r3);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy5 = perceptron.GetAccuracy(Test_Data, wb);
            if (Average) { temp_accuracy5 = perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average); }
            #endregion


            Accuracy = (temp_accuracy1 + temp_accuracy2 + temp_accuracy3 + temp_accuracy4 + temp_accuracy5) / 5;
        }
        public Data(StreamReader r1, StreamReader r2, int epochs, double learning_rate, Random r, bool DymanicLearningRate, double margin, bool Average, bool Aggressive)
        {
            double[] w_average = new double[68];
            double b_average;
            WeightBias wb_average = null;
            if (Average)
            {
                for (int i = 0; i < 68; i++)
                {
                    double randomNumber = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                    w_average[i] = randomNumber;
                }
                b_average = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                wb_average = new WeightBias(w_average, b_average, 0);
            }


            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();
            AccuracyWeightB = new Dictionary<int, AccuracyWB>();

            SetData(r1, r2);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin, wb_average, Aggressive);
            double[] w = new double[68];
            double b = (r.NextDouble() * (0.01 + 0.01) - 0.01);
            for (int i = 0; i < 68; i++)
            {
                double randomNumber = (r.NextDouble() * (0.01 + 0.01) - 0.01);
                w[i] = randomNumber;
            }
            WeightBias wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                if (Average)
                {
                    perceptron.WeightBias_Average.Updates = wb.Updates;
                    AccuracyWeightB.Add(i+1, new AccuracyWB(perceptron.GetAccuracy(Test_Data, perceptron.WeightBias_Average), perceptron.WeightBias_Average));
                    
                }
                else
                {
                    AccuracyWeightB.Add(i + 1, new AccuracyWB (perceptron.GetAccuracy(Test_Data, wb), wb));
                }
                perceptron.ShuffleTraining_Data(r);
            }
            //foreach (var item in AccuracyWeightB)
            //{
            //    Console.WriteLine(item.Value.Accuracy);
            //}
            AccuracyWB bestAccuracy = AccuracyWeightB.OrderByDescending(x => x.Value.Accuracy).ThenByDescending(y => y.Key).Select(z => z.Value).First();


            Accuracy = bestAccuracy.Accuracy;
            BestWeightBias = bestAccuracy.Weight_Bias;
            Learning_Rate = learning_rate;
            //Console.WriteLine("\n" + Accuracy); 
        }
        public Data(StreamReader r1, StreamReader r2, double learning_rate, WeightBias bestWB)
        {
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();
            AccuracyWeightB = new Dictionary<int, AccuracyWB>();

            SetData(r1, r2);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, false, 0, null, false);
            Accuracy = perceptron.GetAccuracy(Test_Data, bestWB);
        }
        public Data(StreamReader r1)
        {
            Training_Data = new List<Entry>();
            SetData(r1);
            int count = 0;
            foreach (var item in Training_Data)
            {
                if(item.Sign == 1)
                {
                    count++;
                }
            }
            double majority = (Convert.ToDouble(count) / Training_Data.Count) * 100;
            if(majority < 50)
            {
                majority = 100 - majority;
            }
            Majority = majority;
        }
        public void SetData(StreamReader reader, StreamReader reader_2 = null)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int Sign;
                double[] Vector = new double[68];
                string[] splitstring = line.Split();
                if(splitstring.First().First() == '+') { Sign = +1; }
                else { Sign = -1; }
                foreach (var item in splitstring)
                {
                    if (item.Contains(":"))
                    {
                        string[] s = item.Split(':');
                        Vector[Convert.ToInt32(s[0]) - 1] = Convert.ToDouble(s[1]);
                    }
                }
                Training_Data.Add(new Entry(Sign, Vector));
            }
            if (reader_2 != null)
            {
                reader_2.DiscardBufferedData();
                reader_2.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                string line2;
                while ((line2 = reader_2.ReadLine()) != null)
                {
                    int Sign;
                    double[] Vector = new double[68];
                    string[] splitstring = line2.Split();
                    if (splitstring.First().First() == '+') { Sign = +1; }
                    else { Sign = -1; }
                    foreach (var item in splitstring)
                    {
                        if (item.Contains(":"))
                        {
                            string[] s = item.Split(':');
                            Vector[Convert.ToInt32(s[0]) - 1] = Convert.ToDouble(s[1]);
                        }
                    }
                    Test_Data.Add(new Entry(Sign, Vector));
                }
            }
        }
    }
}
