using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PreFinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }


        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            if (entUsername.Text.Equals(null) || entPassword.Text.Equals(null))
            {
                await DisplayAlert("No Information", "Please input username and password", "Ok");
            }
            else
            {
                await UsersDAO.AddUser(entUsername.Text, entPassword.Text);
                await Navigation.PopToRootAsync();
            }
        }
    }
}