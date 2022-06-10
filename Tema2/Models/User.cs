using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2.Models
{
    public class User : ObservableObject
    {
        private string _userName;
        private string _imagePath;
        private int _indexGame=0;
        private string _nrGames;
        private string _nrGamesCategory;

        public User(string username, string imagePath, int indexGame, string nrGames, string nrGamesCategory)
        {
            _userName = username;
            _imagePath = imagePath;
            _indexGame = indexGame;
            _nrGames = nrGames;
            _nrGamesCategory = nrGamesCategory;
        }
        public User() { }
        public string UserName
        {
            get { return _userName; }
            set { OnPropertyChanged(ref _userName, value); }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { OnPropertyChanged(ref _imagePath, value); }
        }

        public int IndexGames
        {
            get { return _indexGame; }
            set { OnPropertyChanged(ref _indexGame, value); }
        }

        public string NrOfGames
        {
            get { return _nrGames; }
            set { OnPropertyChanged(ref _nrGames, value); }
        }

        public string NrOfGamesWonCategory
        {
            get { return _nrGamesCategory; }
            set { OnPropertyChanged(ref _nrGamesCategory, value); }
        }
    }
}
