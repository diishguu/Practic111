namespace Pra;

using System.Collections.ObjectModel;
using static WordItem;

public partial class Slovar : ContentPage
{
    public static ObservableCollection<WordItem> Words { get; set; } = new();


    public ObservableCollection<WordItem> FilteredWords { get; set; } = new();

    public Slovar()
	{
		InitializeComponent();

        if (Words.Count == 0)
        {
            Words.Add(new WordItem
            {
                Word = "Apple",
                Translation = "Яблоко",
                Transcription = "[ˈæpl]",
                Example = "I eat an apple every day.",
                Category = "Еда"
            });

            Words.Add(new WordItem
            {
                Word = "Airport",
                Translation = "Аэропорт",
                Transcription = "[ˈeəpɔːt]",
                Example = "The airport is very busy.",
                Category = "Путешествия"
            });

            OnSearchTextChanged(
                WordSearchBar,
                new TextChangedEventArgs(
                    WordSearchBar.Text,
                    WordSearchBar.Text));
        }

        WordsCollection.ItemsSource = Words;
    }
    private async void OnAddWordClicked(object sender, EventArgs e)
    {
        string word = await DisplayPromptAsync(
            "Новое слово",
            "Введите слово:");

        if (string.IsNullOrWhiteSpace(word))
            return;

        string translation = await DisplayPromptAsync(
            "Перевод",
            "Введите перевод:");

        if (string.IsNullOrWhiteSpace(translation))
            return;

        string transcription = await DisplayPromptAsync(
            "Транскрипция",
            "Введите транскрипцию:");

        string example = await DisplayPromptAsync(
            "Пример",
            "Введите пример использования:");

        string category = await DisplayPromptAsync(
            "Категория",
            "Введите категорию:");

        Words.Add(new WordItem
        {
            Word = word,
            Translation = translation,
            Transcription = transcription,
            Example = example,
            Category = string.IsNullOrWhiteSpace(category)
                ? "Без категории"
                : category
        });
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";

        FilteredWords.Clear();

        var result = Words.Where(w =>
    w.Word.ToLower().Contains(searchText) ||
    w.Translation.ToLower().Contains(searchText));

        WordsCollection.ItemsSource = result;
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