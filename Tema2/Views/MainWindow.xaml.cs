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
using Tema2.Models;
using Tema2.ViewModels;
using Tema2.Views;

namespace Tema2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            int index = (int)(indexGame.Text[0] - '0');
            User currentUser = new User(username.Text, image.Source.ToString(), index, nr_Games.Text, nr_Wons.Text);
            Game game = new Game(currentUser, image);
            game.Show();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
