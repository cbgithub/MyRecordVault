using MyRecordVault.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRecordVault.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordListPage : ContentPage
    {
        RecordListPageViewModel vm = new RecordListPageViewModel();

        public RecordListPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
