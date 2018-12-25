using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BasicCalculator
{
    public partial class Form1 : Form
    {
        bool isNewEntry = true;
        bool callFromEqual = false;//used to identify that the user wants arithmatic operations with result
        bool callFromValue = false;
        Queue<string> operators = new Queue<string>();
        Queue<double> values = new Queue<double>();
        Stack<double> results = new Stack<double>();
        double firstValue = 0;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button0_9(object sender, EventArgs e)
        {
            int result;
            Button myButton = (Button)sender;
            string btnValue = myButton.Text;
            labelBox.Text += btnValue;

            //special handling for decimal point
            if (btnValue.Equals("."))
            {
                if (outputBox.Text == "") //trying to display 0.somevalue
                {
                    outputBox.Text += 0 + btnValue;
                    isNewEntry = false;
                }
                else if (outputBox.Text == "0")
                {
                    outputBox.Text += btnValue;
                    isNewEntry = false;
                }

                if (isNewEntry)
                {
                    
                    return;
                }
                if (!outputBox.Text.Contains("."))
                {
                    outputBox.Text += btnValue;


                    isNewEntry = false;
                }
                return;
            }


            if (Int32.TryParse(btnValue, out result))
            {
                if (isNewEntry || outputBox.Text.Equals("0"))
                {
                    outputBox.Text = "";
                }
                outputBox.Text += btnValue;
                isNewEntry = false;
                callFromEqual = false;
                callFromValue = true; //indicate that value is taken from user 
            }
        }



        private void AddButton_Click(object sender, EventArgs e)
        {
            if (callFromValue)
            {
                Button opAdd = (Button)sender;
                if (!callFromEqual)
                {
                    double value = double.Parse(outputBox.Text);
                    values.Enqueue(value);
                }

                operators.Enqueue(opAdd.Text);
                labelBox.Text += " + ";
                outputBox.Text = "";
                isNewEntry = true;
                callFromValue = false;
            }  
        }


        private void SubButton_Click(object sender, EventArgs e)
        {
            if (callFromValue)
            {
                Button opSub = (Button)sender;
                if (!callFromEqual)
                {
                    double value = double.Parse(outputBox.Text);
                    values.Enqueue(value);
                }

                operators.Enqueue(opSub.Text);
                labelBox.Text += " - ";
                isNewEntry = true;
                callFromValue = false;
            }  
        }

       
        private void MulButtonClick(object sender, EventArgs e)
        {
            if (callFromValue)
            {
                Button opMul = (Button)sender;
                if (!callFromEqual)
                {
                    double value = double.Parse(outputBox.Text);
                    values.Enqueue(value);
                }

                operators.Enqueue(opMul.Text);
                labelBox.Text += " * ";
                isNewEntry = true;
                callFromValue = false;
            }
        }

        private void DivButtonClick(object sender, EventArgs e)
        {
            if (callFromValue)
            {
                Button opDiv = (Button)sender;
                if (!callFromEqual)
                {
                    double value = double.Parse(outputBox.Text);
                    values.Enqueue(value);
                }

                operators.Enqueue(opDiv.Text);
                labelBox.Text += " / ";
                isNewEntry = true;
                callFromValue = false;
            }
        }

        private void EqualButtonClick(object sender, EventArgs e)
        {
            if (callFromValue)
            {

                double value = double.Parse(outputBox.Text);
                values.Enqueue(value);
                //labelBox.Text += "last value is" + value + "values " + values.Count+ "op "+ operators.Count ;

                firstValue = values.Dequeue();

                foreach (var item in values)
                {
                    if (operators.Count != 0)
                    {
                        string op = operators.Dequeue();
                        if (op == "+")
                        {
                            // labelBox.Text += "V1 " + firstValue + " item " + item;
                            firstValue = firstValue + item;

                        }
                        else if (op == "-")
                        {
                            firstValue = firstValue - item;
                        }
                        else if (op == "*")
                        {
                            firstValue = firstValue * item;
                        }
                        else if (op == "/")
                        {
                            firstValue = firstValue / item;
                        }
                    }
                }

                outputBox.Text = firstValue.ToString();
                results.Push(firstValue);
                labelBox.Text += " = " + firstValue.ToString();
                isNewEntry = true;
                if (operators.Count == 0)
                {
                    values.Clear();
                    values.Enqueue(firstValue);
                    callFromEqual = true;
                }
                callFromValue = true;
            }

        }
        
        private void ACButton_Click(object sender, EventArgs e)
        {
            outputBox.Text = "0";
            labelBox.Text = "";
            isNewEntry = true;
            callFromEqual = false;
            values.Clear();
            operators.Clear();
            firstValue = 0;
        }

        private void ResultButtonClick(object sender, EventArgs e)
        {
            Button RButton = (Button)sender;
            int i = 0;
            labelBox.Text = "";
            foreach (var item in results)
            {
                labelBox.Text += "R" + (results.Count + i) + ":"+item +" .";
                i = i - 1; 
            }

        }

    }
}

// Author:@ Md.Ekhlasur Rahman
