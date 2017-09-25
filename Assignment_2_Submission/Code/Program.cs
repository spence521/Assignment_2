using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms.DataVisualization.Charting;

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

                #region Majority Baseline
                Data dataTest0 = new Data(test);
                Data dataDev0 = new Data(dev);
                Console.WriteLine("\nThe Majority Baseline Accuracy on the Test set is: \n\t" + Math.Round(dataTest0.Majority, 3));
                Console.WriteLine("The Majority Baseline Accuracy on the Development set is: \n\t" + Math.Round(dataDev0.Majority, 3));

                #endregion

                #region Part 1
                Data dataTest1;
                Data dataDev1;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, false, 0, false, false);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, false, 0, false, false);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, false, 0, false, false);
                double learning_rate = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n*******Simple Perceptron (Part 1)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + "Learning Rate:\t" + Math.Round(learning_rate, 3));
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Round(Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)), 3));
                dataDev1 = new Data(train, dev, 20, learning_rate, r, false, 0, false, false);
                Console.WriteLine("The total number of updates for the best Weight and Bias: \n\t" + dataDev1.BestWeightBias.Updates);
                Console.WriteLine("Developement Set Accuracy: \n\t" + Math.Round(dataDev1.Accuracy, 3));
                dataTest1 = new Data(train, test, learning_rate, dataDev1.BestWeightBias);
                Console.WriteLine("Test Set Accuracy: \n\t" + Math.Round(dataTest1.Accuracy, 3));
                Console.WriteLine("The following are the accuracies from the learning curve part 1: \n\t");
                foreach (var item in dataDev1.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + Math.Round(item.Value.Accuracy, 3));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 2
                Data dataTest2;
                Data dataDev2;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0, false, false);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0, false, false);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0, false, false);
                learning_rate = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n\n\n*******Perceptron with Dynamic Learning Rate (Part 2)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + "Learning Rate:\t" + Math.Round(learning_rate, 3));
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Round(Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)), 3));
                dataDev2 = new Data(train, dev, 20, learning_rate, r, true, 0, false, false);
                Console.WriteLine("The total number of updates for the best Weight and Bias: \n\t" + dataDev2.BestWeightBias.Updates);
                Console.WriteLine("Developement Set Accuracy: \n\t" + Math.Round(dataDev2.Accuracy, 3));
                dataTest2 = new Data(train, test, learning_rate, dataDev2.BestWeightBias);
                Console.WriteLine("Test Set Accuracy: \n\t" + Math.Round(dataTest2.Accuracy, 3));
                Console.WriteLine("The following are the accuracies from the learning curve part 2: \n\t");
                foreach (var item in dataDev2.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + Math.Round(item.Value.Accuracy, 3));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 3
                Data dataTest3;
                Data dataDev3;
                double margin;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 1, false, false);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 1, false, false);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 1, false, false);
                data4 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0.1, false, false);
                data5 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0.1, false, false);
                data6 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0.1, false, false);
                data7 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, true, 0.01, false, false);
                data8 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, true, 0.01, false, false);
                data9 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, true, 0.01, false, false);
                List<Data> ListOfDatas = new List<Data>() { data1, data2, data3, data4, data5, data6, data7, data8, data9 };
                Data LargestData = ListOfDatas.OrderByDescending(w => w.Accuracy).First();
                learning_rate = LargestData.Learning_Rate;
                margin = LargestData.Margin;
                Console.WriteLine("\n\n\n*******Margin Perceptron (Part 3)*******");
                Console.WriteLine("The best hyperparameters are: \n\t" + "Learning Rate:\t" + Math.Round(learning_rate, 3) + "\n\tMargin:\t" + Math.Round(margin, 3));
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Round(LargestData.Accuracy, 3));
                dataDev3 = new Data(train, dev, 20, learning_rate, r, true, margin, false, false);
                Console.WriteLine("The total number of updates for the best Weight and Bias: \n\t" + dataDev3.BestWeightBias.Updates);
                Console.WriteLine("Developement Set Accuracy: \n\t" + Math.Round(dataDev3.Accuracy, 3));
                dataTest3 = new Data(train, test, learning_rate, dataDev3.BestWeightBias);
                Console.WriteLine("Test Set Accuracy: \n\t" + Math.Round(dataTest3.Accuracy, 3));
                Console.WriteLine("The following are the accuracies from the learning curve part 3: \n\t");
                foreach (var item in dataDev3.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + Math.Round(item.Value.Accuracy, 3));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 4
                Data dataTest4;
                Data dataDev4;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, false, 0, true, false);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, false, 0, true, false);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, false, 0, true, false);
                learning_rate = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n*******Averaged Perceptron (Part 4)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + "Learning Rate:\t" + Math.Round(learning_rate, 3));
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Round(Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)), 3));
                dataDev4 = new Data(train, dev, 20, learning_rate, r, false, 0, true, false);
                Console.WriteLine("The total number of updates for the best Weight and Bias: \n\t" + dataDev4.BestWeightBias.Updates);
                Console.WriteLine("Developement Set Accuracy: \n\t" + Math.Round(dataDev4.Accuracy, 3));
                dataTest4 = new Data(train, test, learning_rate, dataDev4.BestWeightBias);
                Console.WriteLine("Test Set Accuracy: \n\t" + Math.Round(dataTest4.Accuracy, 3));
                Console.WriteLine("The following are the accuracies from the learning curve part 4: \n\t");
                foreach (var item in dataDev4.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + Math.Round(item.Value.Accuracy, 3));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Part 5
                Data dataTest5;
                Data dataDev5;
                data1 = new Data(reader, reader2, reader3, reader4, reader5, 10, 1, r, false, 1, false, true);
                data2 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.1, r, false, 0.1, false, true);
                data3 = new Data(reader, reader2, reader3, reader4, reader5, 10, 0.01, r, false, 0.01, false, true);
                margin = DetermineLargest(data1.Accuracy, data2.Accuracy, data3.Accuracy);
                Console.WriteLine("\n\n\n*******Aggressive Perceptron with Margin (Part 5)*******");
                Console.WriteLine("The best hyperparameter is: \n\t" + "Margin:\t" + Math.Round(margin, 3));
                Console.WriteLine("The cross validation accuracy for the best hyperparameter is: \n\t" + Math.Round(Math.Max(data1.Accuracy, Math.Max(data2.Accuracy, data3.Accuracy)), 3));
                dataDev5 = new Data(train, dev, 20, learning_rate, r, true, margin, false, true);
                Console.WriteLine("The total number of updates for the best Weight and Bias: \n\t" + dataDev5.BestWeightBias.Updates);
                Console.WriteLine("Developement Set Accuracy: \n\t" + Math.Round(dataDev5.Accuracy, 3));
                dataTest5 = new Data(train, test, learning_rate, dataDev5.BestWeightBias);
                Console.WriteLine("Test Set Accuracy: \n\t" + Math.Round(dataTest5.Accuracy, 3));
                Console.WriteLine("The following are the accuracies from the learning curve part 5: \n\t");
                foreach (var item in dataDev5.AccuracyWeightB)
                {
                    Console.WriteLine("\t" + Math.Round(item.Value.Accuracy, 3));
                }
                Console.WriteLine("---------------------------------------------------------------------------------------");
                #endregion

                #region Chart
                //Chart chart = new Chart();
                //chart.Series.Clear();
                //chart.Size = new System.Drawing.Size(960, 480);
                //chart.ChartAreas.Add("ChartedAreas");
                //chart.ChartAreas["ChartedAreas"].AxisX.Minimum = 0;
                //chart.ChartAreas["ChartedAreas"].AxisX.Maximum = 21;
                //chart.ChartAreas["ChartedAreas"].AxisY.Minimum = 70;
                //chart.ChartAreas["ChartedAreas"].AxisY.Maximum = 100;
                //chart.ChartAreas["ChartedAreas"].AxisX.Interval= 1;
                //chart.ChartAreas["ChartedAreas"].AxisY.Interval = 3;
                //chart.Legends.Add("Legend1");
                //chart = GenerateChart(chart, dataDev1, "Part 1");
                //chart = GenerateChart(chart, dataDev2, "Part 2");
                //chart = GenerateChart(chart, dataDev3, "Part 3");
                //chart = GenerateChart(chart, dataDev4, "Part 4");
                //chart = GenerateChart(chart, dataDev5, "Part 5");
                //chart.SaveImage("Chart.png", ChartImageFormat.Png);
                #endregion
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
        #region Chart
        //static Chart GenerateChart(Chart chart, Data part1, string seriesName)
        //{
        //    chart.Series.Add(seriesName);
        //    chart.Series[seriesName].ChartType = SeriesChartType.Spline;
        //    chart.Series[seriesName].XValueType = ChartValueType.Int32; //the epoch ID
        //    chart.Series[seriesName].YValueType = ChartValueType.Double; //Development set Acuracy
        //    int i = 1;
        //    foreach (var item in part1.AccuracyWeightB)
        //    {
        //        chart.Series[seriesName].Points.AddXY(i, item.Value.Accuracy);
        //        i++;
        //    }
        //    chart.Series[seriesName].BorderWidth = 2;
        //    return chart;
        //}
        #endregion
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
