using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignement_2
{
    public class Perceptron
    {
        public List<Entry> Training_Data { get; private set; }
        public List<Entry> Test_Data { get; private set; }
        public double Learning_Rate { get; set; }
        public double Initial_Learning_Rate { get; set; }
        public bool DymanicLearningRate { get; set; }
        public int T_Count { get; set; }
        public double Margin { get; set; }
        public Perceptron(List<Entry> train, List<Entry> test, double learning_rate, bool dymanicLearningRate, double margin)
        {
            Training_Data = train;
            Test_Data = test;
            Learning_Rate = learning_rate;
            Initial_Learning_Rate = learning_rate;
            DymanicLearningRate = dymanicLearningRate;
            if (DymanicLearningRate) { T_Count = 1; }
            Margin = margin;           
        }

        public WeightBias CalculateWB(WeightBias wb)
        {
            double[] w = wb.Weight;
            double b = wb.Bias;
            int updates = wb.Updates;
            foreach (var item in Training_Data)
            {
                if (DymanicLearningRate) { Learning_Rate = Initial_Learning_Rate / T_Count; }
                int y = item.Sign;
                int yguess;
                double[] x = item.Vector;
                double xw = 0;
                for (int i = 0; i < 68; i++)
                {
                    xw = xw + (x[i] * w[i]);
                }
                xw += b;
                if(xw >= Margin) { yguess = +1; }
                else { yguess = -1; }
                if(y != yguess)
                {
                    for (int i = 0; i < 68; i++)
                    {
                        w[i] = w[i] + (Learning_Rate * y * x[i]); 
                    }
                    b = b + (Learning_Rate * y);
                    updates++;
                    if (DymanicLearningRate) { T_Count++; }
                }
            }
            return new WeightBias(w, b, updates);
        }

        public double GetAccuracy(List<Entry> test_Data, WeightBias wb)
        {
            double[] w = wb.Weight;
            double b = wb.Bias;
            double TotalErrors = 0;
            foreach (var item in test_Data)
            {
                int y = item.Sign;
                int yguess;
                double[] x = item.Vector;
                double xw = 0;
                for (int i = 0; i < 68; i++)
                {
                    xw = xw + (x[i] * w[i]);
                }
                xw += b;
                if(xw >= 0) { yguess = +1; }
                else { yguess = -1; }
                if(y != yguess)
                {
                    TotalErrors++;
                }
            }
            return 100 - ((TotalErrors / Convert.ToDouble(test_Data.Count)) * 100);
        }
        public void ShuffleTraining_Data(Random rSeed)
        {
            Training_Data = Training_Data.OrderBy(i => rSeed.Next()).ToList();
        }
    }
}
