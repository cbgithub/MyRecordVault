using SQLite.Net.Attributes;
using System;
using System.ComponentModel;

namespace MyRecordVault.Models
{
    public class Record : INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int ParentID { get; set; } = 0;

        public string title;

        [NotNull]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string username;

        [NotNull]
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }


        public string password;

        [NotNull]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }


        public string note;

        
        public string Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        




        public bool delete;


        public bool Delete
        {
            get
            {
                return delete;
            }
            set
            {
                delete = value;
                OnPropertyChanged("Delete");
            }
        }

        public DateTime createdAt;

        
        public DateTime CreatedAt
        {
            get
            {
                return createdAt;
            }
            set
            {
                createdAt = value;
                OnPropertyChanged("CreatedAt");
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
