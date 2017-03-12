using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyRecordVault.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthenticationPage : ContentPage
    {
        private CancellationTokenSource _cancel;

        public AuthenticationPage()
        {
            InitializeComponent();
        }

        private async void OnAuthenticate(object sender, EventArgs e)
        {
            _cancel = new CancellationTokenSource();
            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!", _cancel.Token);

            await SetResultAsync(result);
        }

        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
            {


                await Navigation.PushAsync(new RecordListPage());
                    
            }
           
        }
    }

}
