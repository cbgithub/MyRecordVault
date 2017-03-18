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
    public partial class RecordAddPage : ContentPage
    {
        RecordAddPageViewModel vm = new RecordAddPageViewModel();
        public RecordAddPage()
        {
            InitializeComponent();
            BindingContext = vm;
           
        }
    }
}
