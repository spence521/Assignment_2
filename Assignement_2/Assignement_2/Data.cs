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
        public Dictionary<double, WeightBias> AccuracyWeightB { get; private set; }
        public Perceptron perceptron { get; set; }
        public double Accuracy { get; set; }
        public double Learning_Rate { get; set; }
        public double Margin { get; set; }
        public WeightBias BestWeightBias { get; set; }
        public Data(StreamReader r1, StreamReader r2, StreamReader r3, StreamReader r4, StreamReader r5, int epochs, double learning_rate, Random r, bool DymanicLearningRate, double margin)
        {
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
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
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


            #endregion

            #region Second Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r4);
            SetData(r2);
            SetData(r3);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy2 = perceptron.GetAccuracy(Test_Data, wb);
            #endregion

            #region Third Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r3);
            SetData(r2);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy3 = perceptron.GetAccuracy(Test_Data, wb);

            #endregion

            #region Fourth Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r1, r2);
            SetData(r3);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy4 = perceptron.GetAccuracy(Test_Data, wb);
            #endregion

            #region Fifth Fold
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();

            SetData(r2, r1);
            SetData(r3);
            SetData(r4);
            SetData(r5);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
            wb = new WeightBias(w, b, 0);
            for (int i = 0; i < epochs; i++)
            {
                wb = perceptron.CalculateWB(wb);
                perceptron.ShuffleTraining_Data(r);
            }
            temp_accuracy5 = perceptron.GetAccuracy(Test_Data, wb);
            #endregion


            Accuracy = (temp_accuracy1 + temp_accuracy2 + temp_accuracy3 + temp_accuracy4 + temp_accuracy5) / 5;
        }
        public Data(StreamReader r1, StreamReader r2, int epochs, double learning_rate, Random r, bool DymanicLearningRate, double margin)
        {            
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();
            AccuracyWeightB = new Dictionary<double, WeightBias>();

            SetData(r1, r2);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, DymanicLearningRate, margin);
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
                if (AccuracyWeightB.ContainsKey(perceptron.GetAccuracy(Test_Data, wb))) { AccuracyWeightB[perceptron.GetAccuracy(Test_Data, wb)] = wb; }
                else { AccuracyWeightB.Add(perceptron.GetAccuracy(Test_Data, wb), wb); }
                perceptron.ShuffleTraining_Data(r);
            }
            //foreach (var item in AccuracyWeightB)
            //{
            //    Console.WriteLine(item.Key);
            //}

            Accuracy = AccuracyWeightB.Aggregate((p, q) => p.Key > q.Key ? p : q).Key;//perceptron.GetAccuracy(Test_Data, wb); 
            BestWeightBias = AccuracyWeightB[Accuracy];
            Learning_Rate = learning_rate;
            //Console.WriteLine("\n" + Accuracy); 
        }
        public Data(StreamReader r1, StreamReader r2, double learning_rate, WeightBias bestWB)
        {
            Training_Data = new List<Entry>();
            Test_Data = new List<Entry>();
            AccuracyWeightB = new Dictionary<double, WeightBias>();

            SetData(r1, r2);
            perceptron = new Perceptron(Training_Data, Test_Data, learning_rate, false, 0);
            Accuracy = perceptron.GetAccuracy(Test_Data, bestWB);
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
