namespace Pra;


public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();

       
    }
   

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

 
    }

    private async void OnWordsTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Slova());
    }
    private async void OnRulesTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Rules());
    }
    private async void OnPronunciationTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pronuciation());
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

    private async void OnExercisesTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new exercises());
    }
}