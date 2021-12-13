using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PreFinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {

        public HttpClient client = new HttpClient();
        public string endpoint = "https://api.coingecko.com/api/v3/";
        public WelcomePage()
        {
            InitializeComponent();
           
        }

        

        private void btnNextPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private async void btnCheckConnection_Clicked(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(endpoint + "ping");
            lblResponse.Text = response;



            string currencyResponse = await client.GetStringAsync(endpoint + "simple/supported_vs_currencies");
            List<string> deserializedCurrencyResponse = JsonConvert.DeserializeObject<List<string>>(currencyResponse);
            pickerCurrency.ItemsSource = deserializedCurrencyResponse;
        }

        /*
        private async void btnBitcoinBuyPrice_Clicked(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(endpoint + "simple/price?ids=bitcoin&vs_currencies=php");
            lblResponse.Text = response;
        }

        private async void btnSLPBuyPrice_Clicked(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(endpoint + "simple/price?ids=smooth-love-potion&vs_currencies=php");
            lblResponse.Text = response;
        }

        private async void btnShibaInuBuyPrice_Clicked(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(endpoint + "simple/price?ids=shiba-inu&vs_currencies=php");
            lblResponse.Text = response;
        }
        */

        private void pickerCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pickerCoin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCheckPrice_Clicked(object sender, EventArgs e)
        {

        }
    }

    
}