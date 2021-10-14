using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Vasilev4
{
    public partial class Form1 : Form
    {
        internal GraphPane pane;
        internal int graphcount = 0;
       
        public Form1()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pane = graph1.GraphPane;
            pictureBox1.BackColor = Color.White;
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось Y";
            pane.Title.Text = "y = ax^2 + bx + c";
            pane.YAxis.Scale.Min = -30;
            pane.YAxis.Scale.Max = 30;
            pane.XAxis.Scale.Min = -30;
            pane.XAxis.Scale.Max = 30;
            pane.XAxis.Cross = 0.0;
            pane.YAxis.Cross = 0.0;

            pane.XAxis.Scale.IsSkipFirstLabel = true;
            pane.XAxis.Scale.IsSkipLastLabel = true;
            pane.XAxis.Scale.IsSkipCrossLabel = true;
            pane.YAxis.Scale.IsSkipFirstLabel = true;

            pane.YAxis.Scale.IsSkipLastLabel = true;
            pane.YAxis.Scale.IsSkipCrossLabel = true;

            pane.XAxis.Title.IsVisible = false;
            pane.YAxis.Title.IsVisible = false;
            graph1.AxisChange();
        }

        private void FirstBut_Click(object sender, EventArgs e) //Задание 1
        {
            pictureBox1.CreateGraphics().Clear(Color.White);
            string hello = "Привет, " + textBox1.Text;
            if (hello.Length > 51)
            {
                for (int i = 0; i < hello.Length / 51; i++) //Добавляет абзац и перенос
                {
                    hello = hello.Insert(50 * (i + 1), "-\n-");

                    if (i == 14) //Максимум строк
                    {
                        hello = "Слишком длинное имя";
                        break;
                    }
                }
            }
            pictureBox1.CreateGraphics().DrawString(hello, new Font("Arial", 14), new SolidBrush(Color.Black), 1, 1); //Предел - 51 символ на строчку
        }

        private void button1_Click(object sender, EventArgs e) //Задание 2
        {
            bool digit_check = decimal.TryParse(textBox3.Text, out decimal left_dig);
            if (digit_check)
            {
                bool digit_check2 = decimal.TryParse(textBox4.Text, out decimal right_dig);
                if (digit_check2)
                {
                    if (right_dig != 0)
                        textBox5.Text = Math.Round(left_dig / right_dig, 3).ToString();
                    else
                        textBox5.Text = "Ошибка деления на 0";
                }
                else
                    textBox5.Text = "Введите цифры";
            }
            else
                textBox5.Text = "Введите цифры";
        }

        private void button2_Click(object sender, EventArgs e) //Задание 3
        {
            char next = tB3_1.Text[0];
            if (char.IsLetter(next) & next != char.Parse("я") & next != char.Parse("Я") & next != char.Parse("z") & next != char.Parse("Z")) //проверяет что буква + что не последняя
            {
                if (tB3_1.Text == "ё")
                    tB3_2.Text = "ж";
                else if (tB3_1.Text == "Ё")
                    tB3_2.Text = "Ж";
                else if (tB3_1.Text == "е")
                    tB3_2.Text = "ё";
                else if (tB3_1.Text == "Е")
                    tB3_2.Text = "Ё";
                else
                {
                    next++;
                    tB3_2.Text = Convert.ToString(next);
                }
            }
            else
                tB3_2.Text = "Error";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double x1, x2;
            bool check_A = double.TryParse(textBoxA.Text, out double per_a);
            if (check_A & per_a != 0)
            {
                try
                {
                    PointPairList list = new PointPairList();
                    double per_b = Convert.ToDouble(textBoxB.Text);
                    double per_c = Convert.ToDouble(textBoxC.Text);
                    double D = Math.Pow(per_b, 2) - 4 * per_a * per_c;
                    if (D >= 0)
                    {
                        labelX1.Text = "x1 = ";
                        labelX2.Text = "x2 = ";
                        x1 = (-per_b + Math.Sqrt(D)) / (2 * per_a);
                        x2 = (-per_b - Math.Sqrt(D)) / (2 * per_a);
                        labelX1.Text += x1.ToString();
                        labelX2.Text += x2.ToString();
                        if (Math.Abs(MinX.Value)+Math.Abs(MaxX.Value) < 100)
                        {
                            for (double i = (double)MinX.Value; i < (double)MaxX.Value; i += 0.001)
                            {
                                list.Add(i, y(i, per_b, per_c, per_a));
                            }
                        }
                        else
                        {
                            for (double i = (double)MinX.Value; i < (double)MaxX.Value; i += 0.1)
                            {
                                list.Add(i, y(i, per_b, per_c, per_a));
                            }
                        }
                        
                        graphcount++;
                        pane.AddCurve(graphcount.ToString(), list, Color.DarkBlue, SymbolType.None);
                        graph1.AxisChange();
                        graph1.Invalidate();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        protected double y(double x, double b, double c, double a)
        {
            double y = a * Math.Pow(x, 2) + b * x + c;
            return y;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            graphcount = 0;
            pane.CurveList.Clear();
            NewAxsisMax();
            graph1.Invalidate();
        }

        private void NewAxsisMax()
        {
            pane.YAxis.Scale.Min = (double)MinY.Value;
            pane.YAxis.Scale.Max = (double)MaxY.Value;
            pane.XAxis.Scale.Min = (double)MinX.Value;
            pane.XAxis.Scale.Max = (double)MaxX.Value;
            graph1.AxisChange();
            graph1.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewAxsisMax();
            
        }
    }
}
