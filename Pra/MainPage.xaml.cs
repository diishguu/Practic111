namespace Pra
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnUrokTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewPage1());
        }

        private async void OnProfileTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Profile());
        }

        private async void OnStaticTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Stats());
        }

        private async void OnSlovarTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Slovar());
        }
        private async void OnDostigTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new NewPage2());
        }
        private async void OnPovtorTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new Povtorenie());
        }

    }
}
