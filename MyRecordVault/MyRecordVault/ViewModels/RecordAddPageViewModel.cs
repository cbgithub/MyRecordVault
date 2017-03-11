using MyRecordVault.Helpers;
using MyRecordVault.Models;
using MyRecordVault.Services;
using Reactive.Bindings;
using System;
using System.ComponentModel;

namespace MyRecordVault.ViewModels
{
    public class RecordAddPageViewModel : INotifyPropertyChanged
    {

        private readonly RecordRepository _recordRepository = new RecordRepository();


        public string title;


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

        public ReactiveProperty<string> NewRecordTitle { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> NewRecordUserName { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> NewRecordPassword { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<int> NewRecordPasswordLength { get; } = new ReactiveProperty<int>();

        public ReactiveProperty<bool> IsCaseSensitive { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsDigit { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsSpecialCharacter { get; } = new ReactiveProperty<bool>();

        public ReactiveCommand GeneratePassword { get; } = new ReactiveCommand();

        public ReactiveCommand Save { get; } = new ReactiveCommand();

        public ReactiveCollection<Record> RecordCollection { get; } = new ReactiveCollection<Record>();


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


        public RecordAddPageViewModel()
        {


            this.GeneratePassword
                .Subscribe(_ =>
                {
                    var password = new Password
                    {
                        Text = this.NewRecordPassword.Value,
                        Length = this.NewRecordPasswordLength.Value,
                        CaseSensitive = this.IsCaseSensitive.Value,
                        Digits = this.IsDigit.Value,
                        SpecialCharacters = this.IsSpecialCharacter.Value

                    };
                    GeneratePassword _generatePassword = new GeneratePassword(password);
                    this.NewRecordPassword.Value = _generatePassword._password;


                });


            this.Save
                .Subscribe(async _ =>
                {
                    var record = new Record
                    {
                        Title = this.NewRecordTitle.Value,
                        UserName = this.NewRecordUserName.Value,
                        Password = this.NewRecordPassword.Value,
                        CreatedAt = DateTime.Now,
                        Delete = false
                    };
                    this.RecordCollection.AddOnScheduler(record);
                    await this._recordRepository.SaveItemAsync(record);
                    await App.Current.MainPage.Navigation.PopAsync();


                });
            }


        public async void OnNavigatedTo()
        {
            // SQLite
            this.RecordCollection.ClearOnScheduler();
            this.RecordCollection.AddRangeOnScheduler(await this._recordRepository.GetItemsAsync());
        }
    }

}
