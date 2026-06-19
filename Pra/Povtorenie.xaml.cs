namespace Pra;

using System.Linq;
using Microsoft.Maui.Storage;

public partial class Povtorenie : ContentPage
{
    private List<WordItem> _deck = new(); 
    private int _index = 0; 
    private int _known = 0; 
    private int _unknown = 0; 
    private bool _answerShown = false; 

    public Povtorenie()
	{
		InitializeComponent();
        StartSession(); 
    }
    private void StartSession()
    {

        
        var source = Slovar.Words.Count > 0 ? Slovar.Words.ToList() : FallbackWords();

        
        _deck = source.OrderBy(_ => Guid.NewGuid()).ToList();

       
        _index = 0;
        _known = 0;
        _unknown = 0; 

        SummaryPanel.IsVisible = false; 
        CardBorder.IsVisible = true; 
        ProgressPanel.IsVisible = true; 
        ShowCurrentCard();
    }

    private void ShowCurrentCard()
    {
        _answerShown = false;
        var word = _deck[_index];

        WordLabel.Text = word.Word;
        TranscriptionLabel.Text = word.Transcription ?? ""; 
        TranslationLabel.Text = word.Translation ?? "";
        ExampleLabel.Text = string.IsNullOrWhiteSpace(word.Example) ? "" : $"«{word.Example}»"; 

        AnswerPanel.IsVisible = false; 
        HintLabel.IsVisible = true;
        ShowAnswerButton.IsVisible = true; 
        KnowOrDont.IsVisible = false; 

        ProgressLabel.Text = $"Карточка {_index + 1} / {_deck.Count}"; 
        ScoreLabel.Text = $"✅ {_known}    🔁 {_unknown}";
        SessionProgress.Progress = (double)_index / _deck.Count; 
    }

    


    private void ShowAnswerButton_Clicked(object sender, EventArgs e) 
    {
        if (_answerShown) return; 
        _answerShown = true; 

        AnswerPanel.IsVisible = true; 
        HintLabel.IsVisible = false;
        ShowAnswerButton.IsVisible = false; 
        KnowOrDont.IsVisible = true; 
    }

    private void OnKnowClicked(object sender, EventArgs e)
    {
     
        _known++;
        NextCard();
    }

    private void OnDontKnowClicked(object sender, EventArgs e)
    {
     
        _unknown++;
        NextCard();
    }

    private void NextCard()
    {
        _index++;
        if (_index >= _deck.Count) 
            ShowSummary(); 
        else
            ShowCurrentCard(); 
    }

    private void ShowSummary()
    {
        SessionProgress.Progress = 1; 
        ProgressLabel.Text = $"Карточка {_deck.Count} / {_deck.Count}";
        ScoreLabel.Text = $"✅ {_known}    🔁 {_unknown}";

        CardBorder.IsVisible = false; 
        ShowAnswerButton.IsVisible = false;
        KnowOrDont.IsVisible = false;

        SummaryLabel.Text = $"Знаешь: {_known}    ·    Повторить: {_unknown}";
        SummaryPanel.IsVisible = true; 

  
        int totalReviewed = Preferences.Get("WordsReviewed", 0) + _known;
        Preferences.Set("WordsReviewed", totalReviewed);
        Preferences.Set("LastReviewKnown", _known);
        Preferences.Set("LastReviewTotal", _deck.Count);
    }

    private void OnRestartClicked(object sender, EventArgs e) 
    {
        StartSession();
    }

    private List<WordItem> FallbackWords() => new() 
    {
        new WordItem { Word = "Apple",   Translation = "Яблоко",   Transcription = "[ˈæpl]",    Example = "I eat an apple every day." },
        new WordItem { Word = "House",   Translation = "Дом",      Transcription = "[haʊs]",    Example = "This is my house." },
        new WordItem { Word = "Water",   Translation = "Вода",     Transcription = "[ˈwɔːtə]",  Example = "I drink water." },
        new WordItem { Word = "Friend",  Translation = "Друг",     Transcription = "[frend]",   Example = "He is my friend." },
        new WordItem { Word = "Book",    Translation = "Книга",    Transcription = "[bʊk]",     Example = "I read a book." },
        new WordItem { Word = "City",    Translation = "Город",    Transcription = "[ˈsɪti]",   Example = "The city is big." },
        new WordItem { Word = "Morning", Translation = "Утро",     Transcription = "[ˈmɔːnɪŋ]", Example = "Good morning!" },
        new WordItem { Word = "Window",  Translation = "Окно",     Transcription = "[ˈwɪndəʊ]", Example = "Open the window." },
    };


    
    private async void OnUrokTapped(object sender, EventArgs e) => await Navigation.PushAsync(new NewPage1());
    private async void OnProfileTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Profile());
    private async void OnStaticTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Stats());
    private async void OnSlovarTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Slovar());
    private async void OnDostigTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new NewPage2());
    private async void OnPovtorTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Povtorenie());
    }
}