using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MedicalHeaderBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Values values;

        public MainWindow()
        {
            InitializeComponent();
            values = new Values();
            this.DataContext = values;
        }


        private void Clear_Fields_Click(object sender, RoutedEventArgs e)
        {

            foreach (UIElement element in Grid1.Children)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)element;
                    textBox.Text = "";
                }

            }
            values.result = "";
            this.result.Text = "";

        }

        private void Generate_Header_Click(object sender, RoutedEventArgs e)
        {
            values.result = "";
            foreach (UIElement element in Grid1.Children)
            {
                if (element.GetType() == typeof(TextBox))
                {
                    TextBox textBox = (TextBox)element;
                    foreach (UIElement element2 in Grid1.Children)
                    {
                        if (element2.GetType() == typeof(Label))
                        {
                            Label label = (Label)element2;
                            if (label.Name.Replace("Label", "") == textBox.Name)
                            {
                                checkAndAppend(textBox, label);
                            }
                        }
                    }
                }
            }

            char[] charsToTrim = { ',', ' ' };
            if (values.result != "" && values.result != null)
            {
                values.result = values.result.TrimEnd(charsToTrim) + ".";
            }


           
            this.result.Text = values.result;
            if (values.result != null)
            {
                System.Windows.Clipboard.SetText(values.result);
            }

        }

        public bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public void checkAndAppend(TextBox field, Label label)
        {
            if (field.GetLineText(0) != "")
            {
                if (label.Name == "plateletsLabel")
                {
                    field.Text = field.Text.Replace(",000", "");

                    field.Text += "000";

                    field.Text = Regex.Replace(field.Text, "[^0-9]", "");

                    field.Text = string.Format("{0:n0}", Convert.ToInt64(field.Text));
                }
                if (values.result == null || values.result == "")
                {
                    values.result += label.Content.ToString() + " " + field.GetLineText(0) + ", ";
                }
                else
                {
                    if (IsAllUpper(label.Content.ToString()))
                    {
                        values.result += label.Content.ToString() + " " + field.GetLineText(0) + ", ";
                    }
                    else
                    {
                        values.result += label.Content.ToString().ToLower() + " " + field.GetLineText(0) + ", ";
                    }
                }
            }
        }
    }
}
