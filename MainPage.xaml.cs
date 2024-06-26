

namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Settings(object sender, System.EventArgs e)
        {

            App.Current.MainPage = new NavigationPage(new Settings());
        }
        private void Accounting(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Accounting());
        }
        private void Plans(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Plans());
        }
    }

}
