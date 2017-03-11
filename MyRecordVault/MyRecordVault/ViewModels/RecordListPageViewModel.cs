using MyRecordVault.Models;
using MyRecordVault.Services;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyRecordVault.ViewModels
{
    public class RecordListPageViewModel : INotifyPropertyChanged
    {

        private readonly RecordRepository _recordItemRepository = new RecordRepository();

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


        bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (isBusy == value)
                    return;

                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        ICommand refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await OnNavigatedTo())); }
        }



        public ReactiveCommand NavigateToAddRecordPage { get; } = new ReactiveCommand();

        public ReactiveCollection<Record> Records { get; } = new ReactiveCollection<Record>();

        public ReactiveProperty<Record> SelectedItem { get; } = new ReactiveProperty<Record>();

        public RecordListPageViewModel()
        {

            this.NavigateToAddRecordPage
            .Subscribe(async _ =>
            {
                
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RecordAddPage());
            });



            SelectedItem
            .Where(item => item != null)
                .Subscribe(async item =>
                {
                    this.SelectedItem.Value = null;
                    RecordItemDetailPageViewModel vm = new RecordItemDetailPageViewModel(item.ID);
                    await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new RecordDetailPage(vm));
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


       

        public async Task OnNavigatedTo()
        {
            // SQLite
            this.Records.ClearOnScheduler();
            this.Records.AddRangeOnScheduler(await this._recordItemRepository.GetItemsAsync());
        }

    }
}
