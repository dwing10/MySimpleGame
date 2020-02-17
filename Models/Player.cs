using Demo_Wpf_TheSimpleGame.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Demo_Wpf_TheSimpleGame.Models
{
    public class Player : ObservableObject, IDataErrorInfo
    {
        private string _name;
        private int _wins;
        private int _Losses;
        private int _ties;
        
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Wins
        {
            get { return _wins; }
            set
            {
                _wins = value;
                OnPropertyChanged(nameof(Wins));
            }
        }

        public int Losses
        {
            get { return _Losses; }
            set
            {
                _Losses = value;
                OnPropertyChanged(nameof(Losses));
            }
        }

        public int Ties
        {
            get { return _ties; }
            set
            {
                _ties = value;
                OnPropertyChanged(nameof(Ties));
            }
        }

        // IDataErrorInfo implementation
        public string Error => null;

        public string this[string name]
        {
            get
            {
                switch (name)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                        {
                            return "Enter Name";
                        }
                        break;
                }

                return null;
            }
        }
    }
}
