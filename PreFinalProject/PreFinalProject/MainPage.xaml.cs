using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PreFinalProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var user = await UsersDAO.Login(entUsername.Text, entPassword.Text);
            if (user == null)
            {
                await DisplayAlert("Incorrect Credentials", "Your username/password is incorrect. Please try again", "Ok");
            }
            else
            {
                var welcomePage = new WelcomePage();
                welcomePage.BindingContext = user;
                await Navigation.PushAsync(welcomePage);
                await DisplayAlert("Login Successful", "Enjoy", "Ok");
            }
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
