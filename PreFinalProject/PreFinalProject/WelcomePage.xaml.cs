using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
        public string selectedCoin;
        public string selectedCurrency;
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

            string coinResponse = await client.GetStringAsync(endpoint + "coins/list");
            List<Coins> deserializedCoinResponse = JsonConvert.DeserializeObject<List<Coins>>(coinResponse);
            foreach (Coins c in deserializedCoinResponse)
            {
                pickerCoin.Items.Add(c.id);
            }

            string currencyResponse = await client.GetStringAsync(endpoint + "simple/supported_vs_currencies");
            List<string> deserializedCurrencyResponse = JsonConvert.DeserializeObject<List<string>>(currencyResponse);
            pickerCurrency.ItemsSource = deserializedCurrencyResponse;
        }

        private void pickerCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCurrency = pickerCurrency.SelectedItem.ToString();
        }

        private void pickerCoin_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCoin = pickerCoin.SelectedItem.ToString();
        }

        private async void btnCheckPrice_Clicked(object sender, EventArgs e)
        {
            string response = await client.GetStringAsync(endpoint + "simple/price?ids=" + selectedCoin + "&vs_currencies=" + selectedCurrency);
            // response format is { coin : { currency : price } }
            // example: { "bitcoin" : { "usd" : 48910 } }
            // we use dictionary to model the same structure of key-value pairs
            Dictionary<string, Dictionary<string, int>> deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(response);


            Dictionary<string, int> currencyPairPrice;
            bool a = deserializedResponse.TryGetValue(selectedCoin, out currencyPairPrice);
            int price;
            bool b = currencyPairPrice.TryGetValue(selectedCurrency, out price);
            lblResponse.Text = "The price is " + price;

        }
    }

    
}