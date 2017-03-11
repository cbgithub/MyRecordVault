using MyRecordVault.Models;
using MyRecordVault.Services;
using Reactive.Bindings;
using System;
using Xamarin.Forms;

namespace MyRecordVault.ViewModels
{
    public class RecordItemDetailPageViewModel
    {

        private readonly RecordRepository _recordRepository = new RecordRepository();

        public ReactiveProperty<Record> Record { get; } = new ReactiveProperty<Record>();

        public ReactiveProperty<string> EditRecordTitle { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> EditRecordUserName { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> EditRecordPassword { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<string> EditRecordNote { get; } = new ReactiveProperty<string>();

        public ReactiveCommand Save { get; } = new ReactiveCommand();

        public ReactiveCommand Delete { get; } = new ReactiveCommand();

        public ReactiveCollection<Record> Records { get; } = new ReactiveCollection<Record>();



        public RecordItemDetailPageViewModel(int id)
        {
            OnNavigatedTo(id);
            
            

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




        public async void OnNavigatedTo(int id)
        {
           
            this.Record.Value = await this._recordRepository.FindFirstAsync(id);
           
           
            // SQLite
            this.Records.ClearOnScheduler();
            this.Records.AddRangeOnScheduler(await this._recordRepository.GetItemsAsync(id));
        }
    }



    
}
