using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema2.Models;
using Tema2.Views;

namespace Tema2.ViewModels
{
    public class UserViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public UserViewModel()
        {
            Users = new ObservableCollection<User>();
            CurrentUser = new User();

            Users = new ObservableCollection<User>();
            if (Users != null)
            {
                foreach (User user in Users)
                {
                    Console.WriteLine(user.ImagePath);
                }
            }
        }

        public UserViewModel(int index)
        {
            ShowUsers();
        }
        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value) return;
                currentUser = value;
                NotifyPropertyChanged("CurrentUser");
            }
        }

        private void Add()
        {
            CurrentUser = new User();

            ChooseAvatar();
            NewUsername newUsername = new NewUsername();
            if (newUsername.ShowDialog() == true)
            {
                currentUser.UserName = newUsername.Answer;
            }
            CurrentUser.NrOfGames = "0";
            CurrentUser.NrOfGamesWonCategory="00000";
     
            Users.Add(CurrentUser);

            string[] userInfo = { CurrentUser.UserName, CurrentUser.ImagePath, CurrentUser.NrOfGames.ToString(), CurrentUser.NrOfGamesWonCategory };
            try
            {
                System.IO.File.WriteAllLines(@"C:\Users\User\Desktop\Tema2\Tema2\Users\" + "\\" + CurrentUser.UserName + ".txt", userInfo);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void Delete()
        {
            string filePath = "C:\\Users\\User\\Desktop\\Tema2\\Tema2\\Users\\" + CurrentUser.UserName + ".txt";
            File.Delete(filePath);

            Users.Remove(CurrentUser);
        }

        private void ChooseAvatar()
        {
            var openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                currentUser.ImagePath = openFileDialog.FileName;
            }
        }

        private void ShowUsers()
        {
            string directory = @"C:\Users\User\Desktop\Tema2\Tema2\Users";
            string[] subdirectoryEntries = Directory.GetFiles(directory);
            foreach (string file in subdirectoryEntries)
            {
                CurrentUser = new User();

                string text = File.ReadAllText(file);
                string[] lines = text.Split(Environment.NewLine.ToCharArray());

                int nrLine=0;
                foreach (string line in lines)
                {
                    if (nrLine == 0) CurrentUser.UserName = line;
                    if (nrLine == 2) CurrentUser.ImagePath = line;
                    if (nrLine == 4) CurrentUser.NrOfGames = line;
                    if (nrLine == 6) CurrentUser.NrOfGamesWonCategory = line; 
                    nrLine++;
                }
                
                int nrLine1 = 0; int ok = 0;
                foreach (string line in lines)
                {
                    if (nrLine1 == nrLine - 19)
                    {
                        ok = 1;
                        if (line.Length > 1 && line[line.Length - 2] == ' ')
                            CurrentUser.IndexGames = (int)(line[line.Length - 1] - '0');
                        else
                            CurrentUser.IndexGames = 0;
                    }
                       
                    nrLine1++;
                }
                if(ok==0) CurrentUser.IndexGames = 0;
                Users.Add(CurrentUser);
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(Add);
                }
                return addCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(Delete);
                }
                return deleteCommand;
            }
        }

        private ICommand showUsersCommand;
        public ICommand ShowUsersCommand
        {
            get
            {
                if (showUsersCommand == null)
                {
                    showUsersCommand = new RelayCommand(ShowUsers);
                }
                return showUsersCommand;
            }
        }

        private ICommand avatarCommand;

        public ICommand AvatarCommand
        {
            get
            {
                if (avatarCommand == null)
                {
                    avatarCommand = new RelayCommand(ChooseAvatar);
                }
                return avatarCommand;
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
