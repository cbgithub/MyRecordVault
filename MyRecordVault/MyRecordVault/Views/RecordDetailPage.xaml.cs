using MyRecordVault.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRecordVault.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordDetailPage : ContentPage
    {
        public RecordDetailPage(RecordItemDetailPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
