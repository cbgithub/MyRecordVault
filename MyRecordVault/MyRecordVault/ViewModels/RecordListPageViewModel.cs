using MyRecordVault.Commands;
using MyRecordVault.Models;
using MyRecordVault.Services;
using MyRecordVault.Views;
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
            get { return refreshCommand ?? (refreshCommand = new Command(Refresh, () =>
            {
                return !IsBusy;
            
                
            }));
        }
    }

        private async void Refresh()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            await OnNavigatedTo();

            //DoStuff

            IsBusy = false;
            await OnNavigatedTo();
        }

        #region Field
        private ICommand _navigateTo;
        #endregion Field


        /// <summary>
        ///
        /// </summary>
        public ICommand NavigateTo
        {
            get
            {
                if (_navigateTo == null)
                {
                    _navigateTo = new RelayCommand(
                    param => NavigateToAddPage(),
                    param => CanNavigateToAddPage()
                    );
                }
                return _navigateTo;
            }
        }
        private bool CanNavigateToAddPage()
        {
            // Verify command can be executed here
            return true;
        }
        private void NavigateToAddPage()
        {
             Application.Current.MainPage.Navigation.PushAsync(new RecordAddPage());

        }



        public ReactiveCommand NavigateToAddRecordPage { get; } = new ReactiveCommand();

        public ReactiveCollection<Record> Records { get; } = new ReactiveCollection<Record>();

        public ReactiveProperty<Record> SelectedItem { get; } = new ReactiveProperty<Record>();

        public RecordListPageViewModel()
        {

            this.NavigateToAddRecordPage
            .Subscribe(async _ =>
            {
                
                await Application.Current.MainPage.Navigation.PushAsync(new RecordAddPage());
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
