namespace Pra;

using Microsoft.Maui.Storage;

public partial class Stats : ContentPage
{
	public Stats()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing(); // вызов базовой версии
        LoadStats(); // подгрузка свежих чисел
    }

    private void LoadStats()
    {
        int reviewed = Preferences.Get("WordsReviewed", 0); // читаем сколько изучено слов
        WordsLearnedLabel.Text = reviewed.ToString(); // превращение числа в текст

        int lastKnown = Preferences.Get("LastReviewKnown", 0); // читаем сколько слов отметили как "знаю"
        int lastTotal = Preferences.Get("LastReviewTotal", 0); // сколько всего было карточек
        int percent = lastTotal > 0 ? (int)Math.Round(lastKnown * 100.0 / lastTotal) : 0; // считаем процент
        AccuracyLabel.Text = $"{percent}%"; // выводим процент
    }
    // нижнее меню
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