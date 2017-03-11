using MyRecordVault.Helpers;
using MyRecordVault.Models;
using MyRecordVault.Services;
using Reactive.Bindings;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyRecordVault.ViewModels
{
    public class RecordItemDetailPageViewModel : INotifyPropertyChanged
    {

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


        private readonly RecordRepository _recordRepository = new RecordRepository();

        public ReactiveProperty<Record> Record { get; } = new ReactiveProperty<Record>();

        public ReactiveCommand GeneratePassword { get; } = new ReactiveCommand();

        public ReactiveProperty<string> NewRecordPassword { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<int> NewRecordPasswordLength { get; } = new ReactiveProperty<int>();

        public ReactiveProperty<bool> IsCaseSensitive { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsDigit { get; } = new ReactiveProperty<bool>();

        public ReactiveProperty<bool> IsSpecialCharacter { get; } = new ReactiveProperty<bool>();


        public ReactiveCommand Save { get; } = new ReactiveCommand();

        public ReactiveCommand Delete { get; } = new ReactiveCommand();

        public ReactiveCollection<Record> Records { get; } = new ReactiveCollection<Record>();



        public RecordItemDetailPageViewModel(int id)
        {
            OnNavigatedTo(id);

            


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
                    this.Record.Value.Password = _generatePassword._password;


                });



            this.Save
                .Subscribe(async _ => {
                    var item = this.Record.Value;
                    await this._recordRepository.SaveItemAsync(item);
                     await App.Current.MainPage.Navigation.PopAsync();
                });


            this.Delete
                .Subscribe(async _ => {
                    var item = this.Record.Value;
                    var ans = await Application.Current.MainPage.DisplayAlert("Are you sure you want to Delete", item.Title, "Yes", "No");
                    if (ans)
                    {
                        item.Delete = true;
                        await this._recordRepository.SaveItemAsync(item);
                        await App.Current.MainPage.Navigation.PopAsync();
                    }
                });

            
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





        public async void OnNavigatedTo(int id)
        {
           
            this.Record.Value = await this._recordRepository.FindFirstAsync(id);
           
           
            // SQLite
            this.Records.ClearOnScheduler();
            this.Records.AddRangeOnScheduler(await this._recordRepository.GetItemsAsync(id));
        }
    }



    
}
