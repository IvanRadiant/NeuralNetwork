using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NeuralNetwork
{
    public partial class MainForm : Form
    {

        NeuralNetwork nn;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ImgLoadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (!String.IsNullOrEmpty(openFileDialog1.FileName))
                    {
                        Bitmap bitmap = new Bitmap(openFileDialog1.OpenFile());
                        InputPictureBox.Image = bitmap;

                    }
                }
                else
                {

                }
            }
            catch (Exception error)
            {

            }

        }

        public double[] GetArrayOfPixels(Image image)
        {
            Bitmap bitmap = image as Bitmap;
            if (bitmap != null)
            {
                double[] input = new double[bitmap.Height * bitmap.Width];
                int i = 0;

                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        byte color = bitmap.GetPixel(x, y).R;
                        if (color >= 255)
                            color = 0;
                        else
                            color = 1;
                        input[i] = (double)color;
                        i++;
                    }
                }

                return input;
            }

            return null;
        }

        private Dictionary<int, double[]> GetTrainSet()
        {
            Dictionary<int, double[]> trainSet = new Dictionary<int, double[]>();

            trainSet.Add(0, GetArrayOfPixels(Properties.Resources._0));
            trainSet.Add(1, GetArrayOfPixels(Properties.Resources._1));
            trainSet.Add(2, GetArrayOfPixels(Properties.Resources._2));
            trainSet.Add(3, GetArrayOfPixels(Properties.Resources._3));
            trainSet.Add(4, GetArrayOfPixels(Properties.Resources._4));
            trainSet.Add(5, GetArrayOfPixels(Properties.Resources._5));
            trainSet.Add(6, GetArrayOfPixels(Properties.Resources._6));
            trainSet.Add(7, GetArrayOfPixels(Properties.Resources._7));
            trainSet.Add(8, GetArrayOfPixels(Properties.Resources._8));
            trainSet.Add(9, GetArrayOfPixels(Properties.Resources._9));

            return trainSet;
        }

        private void DisplayResults(Prediction[] predictions)
        {
            string line = new string('*', 30);

            mainTextBox.Text += Environment.NewLine + line + Environment.NewLine;
            for (int i = 0; i < predictions.Length; i++)
                mainTextBox.Text += predictions[i].ToString() + Environment.NewLine;
            mainTextBox.Text += Environment.NewLine + line + Environment.NewLine;
        }

        private void GuessBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (InputPictureBox.Image != null)
                {
                    double[] input = GetArrayOfPixels(InputPictureBox.Image);

                    if(nn == null)
                        nn = new NeuralNetwork(15, 2, 2);
                    nn.Run(input);

                    DisplayResults(nn.Output);

                }
            }
            catch (Exception error)
            {

            }

        }



    }
}
