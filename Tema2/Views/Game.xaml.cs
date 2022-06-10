using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using Tema2.Models;
using Tema2.ViewModels;
using Microsoft.VisualBasic;
namespace Tema2.Views
{

    public partial class Game : Window
    {
        private int index_game;
        private User currentUser;
        private int nrGamesWon = 0;
        private int nrGames = 0;
        int[] nrGamesCategory = new int[] { 0, 0, 0, 0, 0 };
        string correctLetters = "";
        string incorrectLetters = "";
        private int increment = 60;
        DispatcherTimer dt = new DispatcherTimer();
        public string CurrentWord { get; set; }
        public Game(User user, Image image)
        {
            InitializeComponent();

            currentUser = user;
            nrGames = Convert.ToInt32(currentUser.NrOfGames);
            Console.WriteLine("nrGames: " + nrGames);
            Console.WriteLine("Won:");

            string aux = currentUser.NrOfGamesWonCategory;
            int index = 0;
            for (int i = 0; i < aux.Length; i++)
                if (aux[i] != ' ')
                {
                    nrGamesCategory[index] = (int)(aux[i] - '0');
                    index++;
                }

            userImage.Source = image.Source;
            username.Text = currentUser.UserName;
            index_game = currentUser.IndexGames;
            off();
        }

        private string createString(int[] array)
        {
            string _string = "";
            for (int i = 0; i < array.Length; i++)
                _string = _string + array[i].ToString();
            return _string;
        }
        void off()
        {
            a.IsEnabled = false;
            b.IsEnabled = false;
            c.IsEnabled = false;
            d.IsEnabled = false;
            e.IsEnabled = false;
            f.IsEnabled = false;
            g.IsEnabled = false;
            h.IsEnabled = false;
            i.IsEnabled = false;
            j.IsEnabled = false;
            k.IsEnabled = false;
            l.IsEnabled = false;
            m.IsEnabled = false;
            n.IsEnabled = false;
            o.IsEnabled = false;
            p.IsEnabled = false;
            q.IsEnabled = false;
            r.IsEnabled = false;
            s.IsEnabled = false;
            t.IsEnabled = false;
            u.IsEnabled = false;
            v.IsEnabled = false;
            x.IsEnabled = false;
            y.IsEnabled = false;
            w.IsEnabled = false;
            z.IsEnabled = false;

            word.Visibility = Visibility.Hidden;
        }
        void Reset()
        {
            hangman_image1.Visibility = Visibility.Visible;
            hangman_image2.Visibility = Visibility.Hidden;
            hangman_image3.Visibility = Visibility.Hidden;
            hangman_image4.Visibility = Visibility.Hidden;
            hangman_image5.Visibility = Visibility.Hidden;
            hangman_image6.Visibility = Visibility.Hidden;
            hangman_image7.Visibility = Visibility.Hidden;

            x1.Text = ""; x2.Text = ""; x3.Text = ""; x4.Text = ""; x5.Text = ""; x6.Text = "";

            SetLettersVisible();

            start.Visibility = Visibility.Visible;
            word.Visibility = Visibility.Hidden;

            TimerLabel.Content = "60";
            increment = 60;
            TimerLabel.Visibility = Visibility.Visible;

        }
        private bool won()
        {
            foreach (char c in word.Text)
                if (c == '_')
                    return false;
            return true;
        }
        private void LetterExistsInWord(char letter)
        {
            int nr = 1;
            char[] ch = word.Text.ToCharArray();
            bool exist = false;
            for (int i = 0; i < CurrentWord.Length; i++)
            {
                if (letter == CurrentWord[i])
                {
                    ch[i + nr] = letter;
                    exist = true;
                    correctLetters = correctLetters + letter + " ";
                }
                nr++;
            }
            if (exist == false)
            {
                incorrectLetters = incorrectLetters + letter + " ";
                Mistakes();
            }

            word.Text = new string(ch);
            if (won() == true)
            {
                dt.Stop(); TimerLabel.IsEnabled = false;
                dt = new DispatcherTimer();
                MessageBox.Show("Correct!");
                nrGamesWon++;
                _level.Text = nrGamesWon.ToString();
                if (nrGamesWon == 5)
                {
                    MessageBox.Show("You won! :)");
                    _nr_won.Text = addGamesToCategory().ToString();
                    nrGamesWon = 0;
                    _level.Text = nrGamesWon.ToString();
                    nrGames++;

                    currentUser.NrOfGames = nrGames.ToString();
                    currentUser.NrOfGamesWonCategory = createString(nrGamesCategory);
                    updateFile();
                }
                off();
            }
        }
        private void Generate_Word(string path)
        {
            correctLetters = "";
            incorrectLetters = "";
            Random rand = new Random();
            string[] lines = File.ReadAllLines(path);
            CurrentWord = lines[rand.Next(lines.Length)];
            string text = "";
            for (int i = 0; i < CurrentWord.Length; i++)
            {
                if (CurrentWord[i] == ' ')
                    text = text + "  ";
                else
                    text = text + " _";
            }
            word.Text = text;
        }
        private int addGamesToCategory()
        {
            for (int i = 0; i < 5; i++)
                if (i == IndexOfSelectedCategory())
                    return ++nrGamesCategory[i];
            return -1;
        }
        private string pathOfSelectedCategory()
        {
            if (all_categories.IsChecked) return "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\all.txt";
            if (cars.IsChecked) return "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\cars.txt";
            if (movies.IsChecked) return "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\movies.txt";
            if (cartoons.IsChecked) return "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\cartoons.txt";
            return "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\flowers.txt";
        }
        private int IndexOfSelectedCategory()
        {
            if (all_categories.IsChecked) return 0;
            if (cars.IsChecked) return 1;
            if (flowers.IsChecked) return 2;
            if (movies.IsChecked) return 3;
            return 4;
        }
        private void updateFile()
        {
            string fileName = @"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt";
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[3 - 1] = currentUser.NrOfGames.ToString();
            arrLine[4 - 1] = currentUser.NrOfGamesWonCategory;

            File.WriteAllLines(fileName, arrLine);
        }
        private void DtTicker(object sender, EventArgs e)
        {
            increment--;
            if (increment == -1)
            {
                MessageBox.Show("Time is over!" + "The word was " + CurrentWord);
                nrGames++;
                currentUser.NrOfGames = nrGames.ToString();
                updateFile();
                off();
                dt.Stop();
            }
            else if (increment > -1)
                TimerLabel.Content = increment.ToString();
        }
        private string StringOfSelectedCategory()
        {
            int index = IndexOfSelectedCategory();
            string selected_category = "";
            if (index == 0) selected_category = "All categories";
            else if (index == 1) selected_category = "Cars";
            else if (index == 2) selected_category = "Flowers";
            else if (index == 3) selected_category = "Movies";
            else selected_category = "Cartoons";
            return selected_category;
        }
        private int NrOfMistakes()
        {
            int nr = 0;
            if (x1.Text == "X") nr++;
            if (x2.Text == "X") nr++;
            if (x3.Text == "X") nr++;
            if (x4.Text == "X") nr++;
            if (x5.Text == "X") nr++;
            if (x6.Text == "X") nr++;
            return nr;
        }
        private bool ExistLetter(char c, string str, int index)
        {
            for (int i = index + 1; i < str.Length; i++)
                if (c == str[i])
                    return true;
            return false;
        }
        private string UniqueLetters(string str)
        {
            string new_str = "";
            for (int i = 0; i < str.Length; i++)
                if (str[i] != ' ' && ExistLetter(str[i], str, i) == false)
                    new_str = new_str + str[i];
            str = "";
            for (int i = 0; i < new_str.Length; i++)
                str = str + new_str[i] + " ";
            return str;
        }
        private int findStringInFile(string str)
        {
            string file = @"C:\Users\User\Desktop\Tema2\Tema2\Users\" + currentUser.UserName + ".txt";
            string text = File.ReadAllText(file);
            string[] lines = text.Split(Environment.NewLine.ToCharArray());

            int nrLine = 0, count = 0;
            foreach (string line in lines)
            {
                int position = line.IndexOf("Game");
                nrLine++;
                if (position != -1) count++;
                    
            }
            return count;
        }
        private int NrLine(string input)
        {
            string file = @"C:\Users\User\Desktop\Tema2\Tema2\Users\" + currentUser.UserName + ".txt";
            string text = File.ReadAllText(file);
            string[] lines = text.Split(Environment.NewLine.ToCharArray());

            int nrLine = 0;
            foreach (string line in lines)
            {
                int position = line.IndexOf("Game " + input.ToString());
                nrLine++;
                if (position != -1)
                    return nrLine;
            }
            return 0;
        }
        private void Mistakes()
        {
            if (x1.Text != "X") { x1.Text = "X"; hangman_image2.Visibility = Visibility.Visible; hangman_image1.Visibility = Visibility.Hidden; }
            else if (x2.Text != "X") { x2.Text = "X"; hangman_image3.Visibility = Visibility.Visible; hangman_image2.Visibility = Visibility.Hidden; }
            else if (x3.Text != "X") { x3.Text = "X"; hangman_image4.Visibility = Visibility.Visible; hangman_image3.Visibility = Visibility.Hidden; }
            else if (x4.Text != "X") { x4.Text = "X"; hangman_image5.Visibility = Visibility.Visible; hangman_image4.Visibility = Visibility.Hidden; }
            else if (x5.Text != "X") { x5.Text = "X"; hangman_image6.Visibility = Visibility.Visible; hangman_image5.Visibility = Visibility.Hidden; }
            else if (x6.Text != "X")
            {
                x6.Text = "X";
                hangman_image7.Visibility = Visibility.Visible;
                hangman_image6.Visibility = Visibility.Hidden;
                dt.Stop(); TimerLabel.IsEnabled = false; dt = new DispatcherTimer();
                MessageBox.Show("You failed :( The word was " + CurrentWord);
                nrGamesWon = 0; nrGames++; _level.Text = nrGamesWon.ToString();
                currentUser.NrOfGames = nrGames.ToString();
                updateFile();
                off();
            }
        }
        private void SetLettersVisible()
        {
            a.Visibility = Visibility.Visible; a.IsEnabled = true;
            b.Visibility = Visibility.Visible; b.IsEnabled = true;
            c.Visibility = Visibility.Visible; c.IsEnabled = true;
            d.Visibility = Visibility.Visible; d.IsEnabled = true;
            e.Visibility = Visibility.Visible; e.IsEnabled = true;
            f.Visibility = Visibility.Visible; f.IsEnabled = true;
            g.Visibility = Visibility.Visible; g.IsEnabled = true;
            h.Visibility = Visibility.Visible; h.IsEnabled = true;
            i.Visibility = Visibility.Visible; i.IsEnabled = true;
            j.Visibility = Visibility.Visible; j.IsEnabled = true;
            k.Visibility = Visibility.Visible; k.IsEnabled = true;
            l.Visibility = Visibility.Visible; l.IsEnabled = true;
            m.Visibility = Visibility.Visible; m.IsEnabled = true;
            n.Visibility = Visibility.Visible; n.IsEnabled = true;
            o.Visibility = Visibility.Visible; o.IsEnabled = true;
            p.Visibility = Visibility.Visible; p.IsEnabled = true;
            q.Visibility = Visibility.Visible; q.IsEnabled = true;
            r.Visibility = Visibility.Visible; r.IsEnabled = true;
            s.Visibility = Visibility.Visible; s.IsEnabled = true;
            t.Visibility = Visibility.Visible; t.IsEnabled = true;
            u.Visibility = Visibility.Visible; u.IsEnabled = true;
            v.Visibility = Visibility.Visible; v.IsEnabled = true;
            x.Visibility = Visibility.Visible; x.IsEnabled = true;
            y.Visibility = Visibility.Visible; y.IsEnabled = true;
            w.Visibility = Visibility.Visible; w.IsEnabled = true;
            z.Visibility = Visibility.Visible; z.IsEnabled = true;
        }
        private void SetLetterHidden(char letter)
        {
            if (letter == a.Content.ToString()[0]) a.Visibility = Visibility.Hidden;
            if (letter == b.Content.ToString()[0]) b.Visibility = Visibility.Hidden;
            if (letter == c.Content.ToString()[0]) c.Visibility = Visibility.Hidden;
            if (letter == d.Content.ToString()[0]) d.Visibility = Visibility.Hidden;
            if (letter == e.Content.ToString()[0]) e.Visibility = Visibility.Hidden;
            if (letter == f.Content.ToString()[0]) f.Visibility = Visibility.Hidden;
            if (letter == g.Content.ToString()[0]) g.Visibility = Visibility.Hidden;
            if (letter == h.Content.ToString()[0]) h.Visibility = Visibility.Hidden;
            if (letter == i.Content.ToString()[0]) i.Visibility = Visibility.Hidden;
            if (letter == j.Content.ToString()[0]) j.Visibility = Visibility.Hidden;
            if (letter == k.Content.ToString()[0]) k.Visibility = Visibility.Hidden;
            if (letter == l.Content.ToString()[0]) l.Visibility = Visibility.Hidden;
            if (letter == m.Content.ToString()[0]) m.Visibility = Visibility.Hidden;
            if (letter == n.Content.ToString()[0]) n.Visibility = Visibility.Hidden;
            if (letter == o.Content.ToString()[0]) o.Visibility = Visibility.Hidden;
            if (letter == p.Content.ToString()[0]) p.Visibility = Visibility.Hidden;
            if (letter == q.Content.ToString()[0]) q.Visibility = Visibility.Hidden;
            if (letter == r.Content.ToString()[0]) r.Visibility = Visibility.Hidden;
            if (letter == s.Content.ToString()[0]) s.Visibility = Visibility.Hidden;
            if (letter == t.Content.ToString()[0]) t.Visibility = Visibility.Hidden;
            if (letter == u.Content.ToString()[0]) u.Visibility = Visibility.Hidden;
            if (letter == v.Content.ToString()[0]) v.Visibility = Visibility.Hidden;
            if (letter == w.Content.ToString()[0]) w.Visibility = Visibility.Hidden;
            if (letter == x.Content.ToString()[0]) x.Visibility = Visibility.Hidden;
            if (letter == y.Content.ToString()[0]) y.Visibility = Visibility.Hidden;
            if (letter == z.Content.ToString()[0]) z.Visibility = Visibility.Hidden;
        }
        private void CheckCategory(string _category)
        {
            if (all_categories.Header.ToString() == _category)
            {
                all_categories.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
            }
            if (cars.Header.ToString() == _category)
            {
                cars.IsChecked = true;
                all_categories.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
            }
            if (movies.Header.ToString() == _category)
            {
                movies.IsChecked = true;
                cars.IsChecked = false;
                all_categories.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
            }
            if (flowers.Header.ToString() == _category)
            {
                flowers.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                all_categories.IsChecked = false;
                cartoons.IsChecked = false;
            }
            if (cartoons.Header.ToString() == _category)
            {
                cartoons.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                all_categories.IsChecked = false;
            }
        }
        private void SelectedGame(string input, object sender, RoutedEventArgs e)
        {
            int nrLineGame = NrLine(input);
            string str = "";

            SetLettersVisible();
            string file = @"C:\Users\User\Desktop\Tema2\Tema2\Users\" + currentUser.UserName + ".txt";
            string text = File.ReadAllText(file);
            string[] lines = text.Split(Environment.NewLine.ToCharArray());

            int nrLine = 0;
            foreach (string line in lines)
            {
                if (nrLine == (nrLineGame + 1)) { str = line.Replace("Level: ", string.Empty); _level.Text = str; nrGamesWon = Int32.Parse(str); }//level
                if (nrLine == (nrLineGame + 3)) { str = line.Replace("Word1: ", string.Empty); CurrentWord = str; }//word1
                if (nrLine == (nrLineGame + 5)) { str = line.Replace("Word2: ", string.Empty); word.Text = str; word.Visibility = Visibility.Visible; } //word2
                if (nrLine == (nrLineGame + 7)) { str = line.Replace("Category: ", string.Empty); category.Text = str + ": "; CheckCategory(str); }//category
                if (nrLine == (nrLineGame + 9))
                {   //correct
                    str = line.Replace("Correct: ", string.Empty);
                    for (int i = 0; i < str.Length; i++)
                        if (str[i] != ' ')
                            SetLetterHidden(str[i]);
                }
                if (nrLine == (nrLineGame + 11))
                {   //incorect
                    str = line.Replace("Incorrect: ", string.Empty);
                    for (int i = 0; i < str.Length; i++)
                        if (str[i] != ' ')
                            SetLetterHidden(str[i]);
                }
                if (nrLine == (nrLineGame + 13))
                {//mistakes
                    str = line.Replace("Mistakes: ", string.Empty);
                    int mistakes = Int32.Parse(str);
                    for(int i=0; i<mistakes; i++)
                        Mistakes();
                }
                if (nrLine == (nrLineGame + 15))
                { //timer
                    str = line.Replace("Time Left: ", string.Empty);
                    TimerLabel.Content = str; 
                    increment = Int32.Parse(str);
                    TimerLabel.IsEnabled = true; 
                }
                nrLine++; 
            }
            start.Visibility = Visibility.Hidden;
            Window_Loaded(sender, e);

        }
        private void open_game_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            int count = findStringInFile("Games");
            if (count == 0)
                MessageBox.Show("You don't have saved games! ");
            else
            {
                string games = "";
                for (int i = 1; i <= count; i++)
                    games = games + "Game " + i + " ";
                
                string input = Interaction.InputBox(games, "Choose a game to open!", "...", 10, 10);
                SelectedGame(input,sender,e);
            }
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void about_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }
        private void save_game_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop(); TimerLabel.IsEnabled = false;
            index_game++;

            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", " " + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Game " + index_game + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Level: " + _level.Text + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Word1: " + CurrentWord + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Word2: " + word.Text + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Category: " + StringOfSelectedCategory() + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Correct: " + UniqueLetters(correctLetters) + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Incorrect: " + incorrectLetters + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Mistakes: " + NrOfMistakes() + Environment.NewLine);
            File.AppendAllText(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + currentUser.UserName + ".txt", "Time Left: " + TimerLabel.Content.ToString() + Environment.NewLine);

            MessageBox.Show("Game saved!");
            Reset();
            off();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            nrGamesWon = 0;
            _level.Text = nrGamesWon.ToString();

            if (e.Source == all_categories)
            {
                all_categories.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
                category.Text = "All categories: ";
                Generate_Word("C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\all.txt");
            }
            else if (e.Source == cars)
            {
                cars.IsChecked = true;
                all_categories.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
                category.Text = "Cars: ";
                Generate_Word("C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\cars.txt");
            }
            else if (e.Source == cartoons)
            {
                cartoons.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                flowers.IsChecked = false;
                all_categories.IsChecked = false;
                category.Text = "Cartoons: ";
                Generate_Word("C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\cartoons.txt");
            }
            else if (e.Source == movies)
            {
                movies.IsChecked = true;
                cars.IsChecked = false;
                all_categories.IsChecked = false;
                flowers.IsChecked = false;
                cartoons.IsChecked = false;
                category.Text = "Movies: ";
                Generate_Word("C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\movies.txt");
            }
            else
            {
                flowers.IsChecked = true;
                cars.IsChecked = false;
                movies.IsChecked = false;
                all_categories.IsChecked = false;
                cartoons.IsChecked = false;
                category.Text = "Flowers: ";
                Generate_Word("C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Categories\\flowers.txt");
            }
            _nr_won.Text = nrGamesCategory[IndexOfSelectedCategory()].ToString();
        }
        private void start_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            Reset();
            Window_Loaded(sender, e);
            TimerLabel.IsEnabled = true;
            start.Visibility = Visibility.Hidden;
            Generate_Word(pathOfSelectedCategory());

            word.Visibility = Visibility.Visible;
        }
        private void KeyBoard_Click(object sender, RoutedEventArgs e)
        {
            string str = sender.ToString();
            char current_letter = str[str.Length - 1];
            Console.WriteLine(current_letter);
            Button button = (Button)sender;
            button.Visibility = Visibility.Hidden;
            LetterExistsInWord(current_letter);
        }
        private void new_game_Click(object sender, RoutedEventArgs e)
        {
            dt.Stop(); TimerLabel.IsEnabled = false; dt = new DispatcherTimer();
            Reset();
            off();
            start.Visibility = Visibility.Visible;
            word.Text = "";
        }
        private void statistics_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Nr of games: " + nrGames);
            for (int i = 0; i < 5; i++)
                Console.WriteLine("Category " + i + ": " + nrGamesCategory[i]);
            Statistics statistics = new Statistics();
            statistics.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += DtTicker;
            dt.Start();
        }

    }
}
