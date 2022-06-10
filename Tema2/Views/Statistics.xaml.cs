using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Tema2.Views
{
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            view.Visibility = Visibility.Hidden;
            txt1.Visibility = Visibility.Visible;
            txt2.Visibility = Visibility.Visible;
            txt3.Visibility = Visibility.Visible;

            string directory = @"C:\Users\User\Desktop\Tema2\Tema2\Users";
            string[] subdirectoryEntries = Directory.GetFiles(directory);
            foreach (string file in subdirectoryEntries)
            {
                string text = File.ReadAllText(file);
                string[] lines = text.Split(Environment.NewLine.ToCharArray());

                int nrLine = 0;
                string str1="", str2="", str3 = "";
                foreach (string line in lines)
                {
                    if (nrLine == 0) str1 = str1 + "   " + line;
                    if (nrLine==4) str2 = str2 + "   " + line;
                    if (nrLine==6) str3 = str3 + "   " + line;
                    nrLine++;
                } 
                txtBlock1.Text += str1 + Environment.NewLine;
                txtBlock2.Text += str2 + Environment.NewLine;
                txtBlock3.Text += str3 + Environment.NewLine;
            }
        }
    }
}
