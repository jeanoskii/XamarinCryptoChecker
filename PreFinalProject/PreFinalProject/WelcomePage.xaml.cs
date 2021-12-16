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
        // CoinGecko API documentation
        // https://www.coingecko.com/en/api/documentation

        // create a new HttpClient for our asnyc calls
        // to the API endpoint
        public HttpClient client = new HttpClient();

        // the endpoint URI of the API. we will concatenate
        // the API calls to this URL.
        public string endpoint = "https://api.coingecko.com/api/v3/";
        
        // these two string variables will hold the values selected
        // from the picker/dropdown menu
        // these are used in the selectedIndexChanged events
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
            // using the HttpClient object called 'client',
            // we will get a string response from the endpoint
            // the endpoint will be appended with 'ping' API call
            // whatever the response from the API server
            // will be stored in a string called response
            // kindly refer to API documentation for 'ping' command
            string response = await client.GetStringAsync(endpoint + "ping");
            // we will put the response to the label
            // called lblResponse to be shown to the UI
            lblResponse.Text = response;


            // CURRENCIES
            // create another API call to the command simple/supported_vs_currency
            // put the reponse in a string variable called currencyResponse
            // kindly refer to API documentation for 'simple/supported_vs_currency' command
            string currencyResponse = await client.GetStringAsync(endpoint + "simple/supported_vs_currencies");

            // API response will return a LIST of supported currency in the format
            // [ "--currency--", "--currency--", ... ]
            // which we need to deserialize or convert to a compatible
            // .NET object which is List<string>
            // using the NewtonSoft.JsonConvert.DeserializeObject method
            List<string> deserializedCurrencyResponse = JsonConvert.DeserializeObject<List<string>>(currencyResponse);
            // put the list of currencies to the picker/dropdown in the UI
            pickerCurrency.ItemsSource = deserializedCurrencyResponse;

            // COINS
            // create another API call to the command coins/list
            // put the reponse in a string variable called coinResponse
            // kindly refer to API documentation for 'coins/list' command
            string coinResponse = await client.GetStringAsync(endpoint + "coins/list");
            // API response will return a LIST of coins in the format
            // [ { "id" : "--value--","symbol" : "--value--","name" : "--value--" }, ... ]
            // which we need to deserialize or convert to a compatible
            // .NET object which is List<Coins*>
            // using the NewtonSoft.JsonConvert.DeserializeObject method
            // * Coins is a custom class which we created
            // that will serve as the model or data structure
            // for the API response
            List<Coins> deserializedCoinResponse = JsonConvert.DeserializeObject<List<Coins>>(coinResponse);
            // iterate over the List of Coins and
            // and add the 'id' to the picker for the UI
            foreach (Coins c in deserializedCoinResponse)
            {
                pickerCoin.Items.Add(c.id);
            }
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
            Dictionary<string, Dictionary<string, double>> deserializedResponse = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(response);


            Dictionary<string, double> currencyPairPrice;
            // we will try to get the value of the Dictionary
            // using the selected coin ID.
            // this will return yet another Dictionary value that
            // we will put in the currencyPairPrice variable
            bool a = deserializedResponse.TryGetValue(selectedCoin, out currencyPairPrice);
            
            double price;
            // we will try to get the value of the Dictionary
            // using the selected currency.
            // this will return a decimal number value that
            // we will put in the price variable
            bool b = currencyPairPrice.TryGetValue(selectedCurrency, out price);
            
            // output the price to the lblResponse
            lblResponse.Text = "The price is " + price;

        }
    }

    
}