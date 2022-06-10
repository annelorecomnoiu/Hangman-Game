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
using System.Windows.Shapes;

namespace Tema2.Views
{
    /// <summary>
    /// Interaction logic for NewUsername.xaml
    /// </summary>
    public partial class NewUsername : Window
    {
        public NewUsername(string defaultAnswer = "")
        {
            InitializeComponent();
            usernameTxt.Text = defaultAnswer;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Answer
        {
            get { return usernameTxt.Text; }
        }
    }
}
