using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Embedding
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("Live Player", "Thank you for watching me live", "Ok");
        }
    }
}
