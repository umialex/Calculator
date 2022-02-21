using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculate
{
    public partial class Form1 : Form
    {
        Double resultValue = 0; //wartość wyniku 
        String operationPerformed = ""; //wartość wpisywana przez kalkulator
        bool isOperationPerformed = false;  // zmienna przechowująca czy operacja jest wykonana
        String functionClick = ""; //zmienna przechowująca wykonywane polecenie np sin cos tg ctg 
        String isOperation = ""; // zmienna przechowująca jaką konwersję wykonujemy na z F na stopnie C

        //początkowa wielkość naszego okna Form1
        public Form1()
        {
            InitializeComponent();
            Width = 326;
        }

        //funkcja przechwytująca liczby (0-9 ,) wpisywane w klawisze kalkulatora 
        private void button_click(object sender, EventArgs e)
        { //czyszczenie pamięci kalkulatora 
            if (textBox_Result.Text == "0" || (isOperationPerformed))
            {
                textBox_Result.Clear();
            }
            isOperationPerformed = false;
            Button button = (Button)sender;
            
            //Kontrola nad liczbami zmiennoprzecinkowymi (aby wystąpił tylko raz przecinek w linii)
            if (button.Text == ",") 
            {
                if (!textBox_Result.Text.Contains(","))
                    textBox_Result.Text = textBox_Result.Text + button.Text;
            }
            else
                textBox_Result.Text = textBox_Result.Text + button.Text;
            //dla liczby pi 

            if (textBox_Result.Text == "pi")
            {
                textBox_Result.Text = (Math.PI).ToString();
            }
            labelHistory.Text = labelHistory.Text+button.Text;
        }

        //funkcja przechwytująca znaki (+ - * /) które po kliknięciu są przechowywane w pamięci (lebel) kalkulatora 
        private void operation_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            labelHistory.Text = labelHistory.Text + button.Text;
            if (resultValue != 0)
            {
                button13.PerformClick();
                operationPerformed = button.Text;
              labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {
                operationPerformed = button.Text;
                resultValue = System.Convert.ToDouble(textBox_Result.Text);
                labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
         
        }
        //czyszczenie wiersza wpisywania kalkulatora CE
        private void button4_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            labelHistory.Text = "";
            resultValue = 0;
            labelCurrentOperation.Text = "";


        }
        //czyszczenie wiersza i wyniku kalkucatora C
        private void button5_Click(object sender, EventArgs e)
        {
            //historia
            textBox_Result.Text = "0";
            resultValue = 0;
            labelHistory.Text = "";

        }

        // wykonywanie obliczeń arytmetyki podstawowej po kliknięciu "="
        private void button13_Click(object sender, EventArgs e)
        {

            switch (operationPerformed)
           {
                case "+":
                    textBox_Result.Text = (resultValue + Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "-":
                    textBox_Result.Text = (resultValue - Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "*":
                    textBox_Result.Text = (resultValue * Double.Parse(textBox_Result.Text)).ToString();
                    break;
                case "/":
                    textBox_Result.Text = (resultValue / Double.Parse(textBox_Result.Text)).ToString();
                    break;

            }
        
            resultValue = Double.Parse(textBox_Result.Text);
            labelCurrentOperation.Text = "";
            labelHistory.Text = labelHistory.Text + "=" + textBox_Result.Text;
            resultValue = 0;
            historydrop();  
        }

        // wykonywanie obliczeń zaawansowanych
        private void function_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            functionClick = button.Text;
            Double liczba = Double.Parse(textBox_Result.Text);
            switch (functionClick)
            {
                case "sqrt":
                    if (resultValue == 0)
                        textBox_Result.Text = (Math.Sqrt(Double.Parse(textBox_Result.Text))).ToString();
                    else
                        textBox_Result.Text = Math.Sqrt(resultValue).ToString();

                  
                    labelHistory.Text = "sqrt(" +liczba.ToString()+")"+ "=" + textBox_Result.Text;
                    resultValue = Double.Parse(textBox_Result.Text);
                    historydrop();
                    break;
                case "sin()":
                    if (resultValue == 0)
                        textBox_Result.Text = (Math.Sin(Double.Parse(textBox_Result.Text))).ToString();
                    else
                        textBox_Result.Text = Math.Sin(resultValue).ToString();
                    labelHistory.Text = "sin(" + liczba.ToString() + ")" + "=" + textBox_Result.Text;
                    resultValue = Double.Parse(textBox_Result.Text);
                    historydrop();
                    break;
                case "cos()":
                    if (resultValue == 0)
                        textBox_Result.Text = (Math.Cos(Double.Parse(textBox_Result.Text))).ToString();
                    else
                        textBox_Result.Text = Math.Cos(resultValue).ToString();
                    labelHistory.Text = "cos(" + liczba.ToString() + ")" + "=" + textBox_Result.Text;
                    resultValue = Double.Parse(textBox_Result.Text);
                    historydrop();
                    break;
                case "tg()":
                    if (resultValue == 0)
                        textBox_Result.Text = (Math.Tan(Double.Parse(textBox_Result.Text))).ToString();
                    else
                        textBox_Result.Text = Math.Tan(resultValue).ToString();
                    labelHistory.Text = "tg(" + liczba.ToString() + ")" + "=" + textBox_Result.Text;
                    resultValue = Double.Parse(textBox_Result.Text);
                    historydrop();
                    break;

            }

        }
// szerokość wzglendem menu:
        private void standartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 326;
           
        }

        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 818;
            groupBox1.Visible = true;
            groupBox3.Visible = false;
            // ustawienie lokalizacji dla convertera 
            groupBox1.Location = new Point(310, 52);
        }
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 818;
            groupBox1.Visible = false;
            groupBox3.Visible = true;
            groupBox3.Location = new Point(310, 52);
        }
        //menu temperatura
        private void CtoF_CheckedChanged(object sender, EventArgs e)
        {
            isOperation = "C";
        }

        private void FtoC_CheckedChanged(object sender, EventArgs e)
        {
            isOperation = "F";
        }

        private void K_CheckedChanged(object sender, EventArgs e)
        {
            isOperation = "K";
        }
        private void KFCheckedChanged(object sender, EventArgs e)
        {
            isOperation = "KF";
        }

        private void DR_CheckedChanged(object sender, EventArgs e)
        {
            isOperation = "DR";
        }

        private void RD_CheckedChanged(object sender, EventArgs e)
        {
            isOperation = "RD";
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            Double chwilowa = 0;

            switch (isOperation)
            {
                case "C":
                    chwilowa= double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round((((9 * chwilowa ) / 5) + 32),3).ToString();
                    break;
                case "F":
                    chwilowa = double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round((((chwilowa-32) * 5) / 9),3).ToString();
                    break;
                case "K":
                    chwilowa = double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round(((((9 * chwilowa) / 5) + 32)+273.15),3).ToString();
                    break;
                case "KF":
                    chwilowa = double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round((((chwilowa-273.15) *1.8) + 32), 3).ToString();
                    break;
                case "DR":
                    chwilowa = double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round(((Math.PI * chwilowa) / 180), 3).ToString();
                    break;
                case "RD":
                    chwilowa = double.Parse(textBox_Result.Text);
                    textConvert.Text = Math.Round(((180 * chwilowa) / Math.PI) , 3).ToString();
                    break;

            }
        }
        //motyw ciemny
        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(0, 0, 20);
            groupBox1.BackColor = Color.FromArgb(0, 0, 20);
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox2.BackColor = Color.FromArgb(0, 0, 20);
            groupBox2.ForeColor = Color.WhiteSmoke;
            groupBox3.BackColor = Color.FromArgb(0, 0, 20);
            groupBox3.ForeColor = Color.WhiteSmoke;
            textBox_Result.BackColor = Color.FromArgb(0, 0, 40);
            textBox_Result.ForeColor = Color.WhiteSmoke;
            menuStrip1.BackColor = Color.FromArgb(0, 0, 40);
            menuStrip1.ForeColor = Color.WhiteSmoke;
            Convert.BackColor = Color.FromArgb(0, 0, 40);
            Convert.ForeColor = Color.WhiteSmoke;
            button1.BackColor = Color.FromArgb(0, 0, 40);
            button1.ForeColor = Color.WhiteSmoke;
            button2.BackColor = Color.FromArgb(0, 0, 40);
            button2.ForeColor = Color.WhiteSmoke;
            button3.BackColor = Color.FromArgb(0, 0, 40);
            button3.ForeColor = Color.WhiteSmoke;
            button4.BackColor = Color.FromArgb(0, 0, 40);
            button4.ForeColor = Color.WhiteSmoke;
            button5.BackColor = Color.FromArgb(0, 0, 40);
            button5.ForeColor = Color.WhiteSmoke;
            button6.BackColor = Color.FromArgb(0, 0, 40);
            button6.ForeColor = Color.WhiteSmoke;
            button7.BackColor = Color.FromArgb(0, 0, 40);
            button7.ForeColor = Color.WhiteSmoke;
            button8.BackColor = Color.FromArgb(0, 0, 40);
            button8.ForeColor = Color.WhiteSmoke;
            button9.BackColor = Color.FromArgb(0, 0, 40);
            button9.ForeColor = Color.WhiteSmoke;
            button10.BackColor = Color.FromArgb(0, 0, 40);
            button10.ForeColor = Color.WhiteSmoke;
            button11.BackColor = Color.FromArgb(0, 0, 40);
            button11.ForeColor = Color.WhiteSmoke;
            button12.BackColor = Color.FromArgb(0, 0, 40);
            button12.ForeColor = Color.WhiteSmoke;
            button13.BackColor = Color.FromArgb(0, 0, 40);
            button13.ForeColor = Color.WhiteSmoke;
            button14.BackColor = Color.FromArgb(0, 0, 40);
            button14.ForeColor = Color.WhiteSmoke;
            button15.BackColor = Color.FromArgb(0, 0, 40);
            button15.ForeColor = Color.WhiteSmoke;
            button16.BackColor = Color.FromArgb(0, 0, 40);
            button16.ForeColor = Color.WhiteSmoke;
            button17.BackColor = Color.FromArgb(0, 0, 40);
            button17.ForeColor = Color.WhiteSmoke;
            button18.BackColor = Color.FromArgb(0, 0, 40);
            button18.ForeColor = Color.WhiteSmoke;
            button19.BackColor = Color.FromArgb(0, 0, 40);
            button19.ForeColor = Color.WhiteSmoke;
            button20.BackColor = Color.FromArgb(0, 0, 40);
            button20.ForeColor = Color.WhiteSmoke;
            button21.BackColor = Color.FromArgb(0, 0, 40);
            button21.ForeColor = Color.WhiteSmoke;
            button22.BackColor = Color.FromArgb(0, 0, 40);
            button22.ForeColor = Color.WhiteSmoke;
            pi.BackColor = Color.FromArgb(0, 0, 40);
            pi.ForeColor = Color.WhiteSmoke;
            fileToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            fileToolStripMenuItem.ForeColor = Color.WhiteSmoke; 
            standartToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            standartToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            temperatureToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            temperatureToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            historyToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            historyToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            viewToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            viewToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            darkToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            darkToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            whiteToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            whiteToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            editToolStripMenuItem.BackColor = Color.FromArgb(0, 0, 40);
            editToolStripMenuItem.ForeColor = Color.WhiteSmoke;

        }

        //motyw jasny

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackColor = Color.WhiteSmoke;
            groupBox1.BackColor = Color.WhiteSmoke;
            groupBox1.ForeColor = Color.Black;
            groupBox2.BackColor = Color.WhiteSmoke;
            groupBox2.ForeColor = Color.Black;
            groupBox3.BackColor = Color.WhiteSmoke;
            groupBox3.ForeColor = Color.Black;
            textBox_Result.BackColor = Color.WhiteSmoke;
            textBox_Result.ForeColor = Color.Black;
            menuStrip1.BackColor = Color.WhiteSmoke;
            menuStrip1.ForeColor = Color.Black;
            Convert.BackColor = Color.WhiteSmoke;
            Convert.ForeColor = Color.Black;
            button1.BackColor = Color.WhiteSmoke;
            button1.ForeColor = Color.Black;
            button2.BackColor = Color.WhiteSmoke;
            button2.ForeColor = Color.Black;
            button3.BackColor = Color.WhiteSmoke;
            button3.ForeColor = Color.Black;
            button4.BackColor = Color.WhiteSmoke;
            button4.ForeColor = Color.Black;
            button5.BackColor = Color.WhiteSmoke;
            button5.ForeColor = Color.Black;
            button6.BackColor = Color.WhiteSmoke;
            button6.ForeColor = Color.Black;
            button7.BackColor = Color.WhiteSmoke;
            button7.ForeColor = Color.Black;
            button8.BackColor = Color.WhiteSmoke;
            button8.ForeColor = Color.Black;
            button9.BackColor = Color.WhiteSmoke;
            button9.ForeColor = Color.Black;
            button10.BackColor = Color.WhiteSmoke;
            button10.ForeColor = Color.Black;
            button11.BackColor = Color.WhiteSmoke;
            button11.ForeColor = Color.Black;
            button12.BackColor = Color.WhiteSmoke;
            button12.ForeColor = Color.Black;
            button13.BackColor = Color.WhiteSmoke;
            button13.ForeColor = Color.Black;
            button14.BackColor = Color.WhiteSmoke;
            button14.ForeColor = Color.Black;
            button15.BackColor = Color.WhiteSmoke;
            button15.ForeColor = Color.Black;
            button16.BackColor = Color.WhiteSmoke;
            button16.ForeColor = Color.Black;
            button17.BackColor = Color.WhiteSmoke;
            button17.ForeColor = Color.Black;
            button18.BackColor = Color.WhiteSmoke;
            button18.ForeColor = Color.Black;
            button19.BackColor = Color.WhiteSmoke;
            button19.ForeColor = Color.Black;
            button20.BackColor = Color.WhiteSmoke;
            button20.ForeColor = Color.Black;
            button21.BackColor = Color.WhiteSmoke;
            button21.ForeColor = Color.Black;
            button22.BackColor = Color.WhiteSmoke;
            button22.ForeColor = Color.Black;
            pi.BackColor = Color.WhiteSmoke;
            pi.ForeColor = Color.Black;
            fileToolStripMenuItem.BackColor = Color.WhiteSmoke;
            fileToolStripMenuItem.ForeColor = Color.Black;
            standartToolStripMenuItem.BackColor = Color.WhiteSmoke;
            standartToolStripMenuItem.ForeColor = Color.Black;
            temperatureToolStripMenuItem.BackColor = Color.WhiteSmoke;
            temperatureToolStripMenuItem.ForeColor = Color.Black;
            historyToolStripMenuItem.BackColor = Color.WhiteSmoke;
            historyToolStripMenuItem.ForeColor = Color.Black;
            viewToolStripMenuItem.BackColor = Color.WhiteSmoke;
            viewToolStripMenuItem.ForeColor = Color.Black;
            darkToolStripMenuItem.BackColor = Color.WhiteSmoke;
            darkToolStripMenuItem.ForeColor = Color.Black;
            whiteToolStripMenuItem.BackColor = Color.WhiteSmoke;
            whiteToolStripMenuItem.ForeColor = Color.Black;
            editToolStripMenuItem.BackColor = Color.WhiteSmoke;
            editToolStripMenuItem.ForeColor = Color.Black;
        }

        // przesuwanie histori do następnego wiersza
        private void historydrop()
        {
            labelCurrentOperation.Text = "";
            labelHistory9.Text = labelHistory8.Text;
            labelHistory8.Text = labelHistory7.Text;
            labelHistory7.Text = labelHistory6.Text;
            labelHistory6.Text = labelHistory5.Text;
            labelHistory5.Text = labelHistory4.Text;
            labelHistory4.Text = labelHistory3.Text;
            labelHistory3.Text = labelHistory2.Text;
            labelHistory2.Text = labelHistory1.Text;
            labelHistory1.Text = labelHistory.Text;
            //labelHistory.Text = "";

            if (textBox_Result.Text == "0")
            {
              
                labelHistory.Text = "";

            }
            else
            {
                labelHistory.Text = textBox_Result.Text;
            }
            resultValue = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    

}


