using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Assignement_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Data data1;
            Data data2;
            Data data3;
            Data data4;
            Data data5;
            Data data6;
            Data data7;
            Data data8;
            Data data9;
            #region Arguments
            StreamReader reader;
            StreamReader reader2;
            StreamReader reader3;
            StreamReader reader4;
            StreamReader reader5;
            StreamReader dev;
            StreamReader test;
            StreamReader train;
            Random r = new Random(2);
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a file argument.");
                return;
            }
            else if (args.Length > 7)
            {
                reader = File.OpenText(args[0]);
                reader2 = File.OpenText(args[1]);
                reader3 = File.OpenText(args[2]);
                reader4 = File.OpenText(args[3]);
                reader5 = File.OpenText(args[4]);
                train = File.OpenText(args[5]);
                test = File.OpenText(args[6]);
                dev = File.OpenText(args[7]);

                #region Part 1
                Data dataTest1;
                Data dataDev1;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, false, 0);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, false, 0);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, false, 0);
                double learning_rate = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n*******Simple Perceptron (Part 1)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + learning_rate);
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)));
                dataDev1 = new Data(train, dev, 20, learning_rate, r, false, 0);
                Console.WriteLine("The total number of updates: \n\t" + dataDev1.BestWeightBias.Updates);
                Console.WriteLine("The Best Developement Set Accuracy \n\t" + dataDev1.Accuracy);
                dataTest1 = new Data(train, test, learning_rate, dataDev1.BestWeightBias);
                Console.WriteLine("Test Set accuracy using the Best Weight and Bias: \n\t" + dataTest1.Accuracy);
                Console.WriteLine("The following are the accuracies from the learning curve part 1: \n\t");
                foreach (var item in dataDev1.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + item.Key);
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 2
                Data dataTest2;
                Data dataDev2;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0);
                learning_rate = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n\n\n*******Perceptron with Dynamic Learning Rate (Part 2)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + learning_rate);
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)));
                dataDev2 = new Data(train, dev, 20, learning_rate, r, true, 0);
                Console.WriteLine("The total number of updates: \n\t" + dataDev2.BestWeightBias.Updates);
                Console.WriteLine("The Best Developement Set Accuracy \n\t" + dataDev2.Accuracy);
                dataTest2 = new Data(train, test, learning_rate, dataDev2.BestWeightBias);
                Console.WriteLine("Test Set accuracy using the Best Weight and Bias: \n\t" + dataTest2.Accuracy);
                Console.WriteLine("The following are the accuracies from the learning curve part 2: \n\t");
                foreach (var item in dataDev2.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + item.Key);
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 3
                Data dataTest3;
                Data dataDev3;
                double margin;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 1);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 1);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 1);
                data4 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0.1);
                data5 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0.1);
                data6 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0.1);
                data7 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0.01);
                data8 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0.01);
                data9 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0.01);
                List<Data> ListOfDatas = new List<Data>() { data1, data2, data3, data4, data5, data6, data7, data8, data9 };
                Data LargestData = ListOfDatas.OrderByDescending(w => w.Accuracy).First();
                learning_rate = LargestData.Learning_Rate;
                margin = LargestData.Margin;
                Console.WriteLine("\n\n\n*******Margin Perceptron (Part 3)*******");
                Console.WriteLine("The best hyperparameters are: \n\t" + "Learning Rate:\t" + learning_rate + "\n\tMargin:\t" + margin);
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + LargestData.Accuracy);
                dataDev3 = new Data(train, dev, 20, learning_rate, r, true, margin);
                Console.WriteLine("The total number of updates: \n\t" + dataDev3.BestWeightBias.Updates);
                Console.WriteLine("The Best Developement Set Accuracy \n\t" + dataDev3.Accuracy);
                dataTest3 = new Data(train, test, learning_rate, dataDev3.BestWeightBias);
                Console.WriteLine("Test Set accuracy using the Best Weight and Bias: \n\t" + dataTest3.Accuracy);
                Console.WriteLine("The following are the accuracies from the learning curve part 3: \n\t");
                foreach (var item in dataDev3.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + item.Key);
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                Chart chart = new Chart();
                chart.Series.Clear();
                chart.Size = new System.Drawing.Size(640, 320);
                chart.ChartAreas.Add("ChartedAreas");
                chart.Legends.Add("Legend1");
                chart = GenerateChart(chart, dataDev1, "Part 1");
                chart = GenerateChart(chart, dataDev2, "Part 2");
                chart = GenerateChart(chart, dataDev3, "Part 3");
                chart.SaveImage("Chart.png", ChartImageFormat.Png);
            }
            #endregion

            #region Non-Arguments
            //string startupPath = System.IO.Directory.GetCurrentDirectory();
            //StreamReader reader = File.OpenText(startupPath + @"\training00.data");
            //StreamReader reader2 = File.OpenText(startupPath + @"\training01.data");
            //StreamReader reader3 = File.OpenText(startupPath + @"\training02.data");
            //StreamReader reader4 = File.OpenText(startupPath + @"\training03.data");
            //StreamReader reader5 = File.OpenText(startupPath + @"\training04.data");
            //StreamReader train = File.OpenText(startupPath + @"\phishing.train");
            //StreamReader test = File.OpenText(startupPath + @"\phishing.test");
            //StreamReader dev = File.OpenText(startupPath + @"\phishing.dev");
            //data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1);
            //data01 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1);
            //data001 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01);
            //double learning_rate;
            //if (data1.Accuracy > data01.Accuracy)
            //{
            //    if (data1.Accuracy > data001.Accuracy) { learning_rate = 1; }
            //    else { learning_rate = 0.01; }
            //}
            //else if (data01.Accuracy > data001.Accuracy) { learning_rate = 0.1; }
            //else { learning_rate = 0.01; }
            //Console.WriteLine("The best hyperparameter is: \n\t" + learning_rate);
            //Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Max(data1.Accuracy, Math.Max(data01.Accuracy, data001.Accuracy)));
            //dataTest = new Data(train, dev, 20, learning_rate);
            //Console.WriteLine("Trained on Training Data, Tested with Test Data: \n\t" + dataTest.Accuracy);
            //dataDev = new Data(train, test, 20, learning_rate);
            //Console.WriteLine("Trained on Training Data, Tested with Dev Data: \n\t" + dataDev.Accuracy);
            //Console.ReadKey(false);
            #endregion

        }
        static Chart GenerateChart(Chart chart, Data part1, string seriesName)
        {
            chart.Series.Add(seriesName);
            //chart.ChartAreas.Add(seriesName);
            //chart.ChartAreas[seriesName].AxisY.Minimum = 70;
            //chart.ChartAreas[seriesName].AxisY.Minimum = 100;
            chart.Series[seriesName].ChartType = SeriesChartType.Spline;
            chart.Series[seriesName].XValueType = ChartValueType.Int32; //the epoch ID
            chart.Series[seriesName].YValueType = ChartValueType.Int32; //Development set Acuracy
            int i = 1;
            foreach (var item in part1.AccuracyWeightB)
            {
                chart.Series[seriesName].Points.AddXY(i, item.Key);
                i++;
            }
            chart.Series[seriesName].BorderWidth = 2;
            return chart;
        }
        static double DetermineLargest(double Accuracy1, double Accuracy2, double Accuracy3)
        {
            if (Accuracy1 > Accuracy2)
            {
                if (Accuracy1 > Accuracy3) { return 1; }
                else { return 0.01; }
            }
            else if (Accuracy2 > Accuracy3) { return 0.1; }
            else { return 0.01; }
        }
    }
}
