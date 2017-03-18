using System.ComponentModel;

namespace MyRecordVault.Models
{
    public class Password : INotifyPropertyChanged
    {

        public string text;

        
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }


        public double strength;


        public double Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                OnPropertyChanged("Strength");
            }
        }


        public int length;


        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
                OnPropertyChanged("Length");
            }
        }

        public bool casesensitive;


        public bool CaseSensitive
        {
            get
            {
                return casesensitive;
            }
            set
            {
                casesensitive = value;
                OnPropertyChanged("CaseSensitive");
            }
        }

        public bool digits;


        public bool Digits
        {
            get
            {
                return digits;
            }
            set
            {
                digits = value;
                OnPropertyChanged("Digits");
            }
        }


        public bool specialcharacters;


        public bool SpecialCharacters
        {
            get
            {
                return specialcharacters;
            }
            set
            {
                specialcharacters = value;
                OnPropertyChanged("SpecialCharacters");
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;

        // <summary>
        /// Use to notify the view when one of the Fields changes.
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
