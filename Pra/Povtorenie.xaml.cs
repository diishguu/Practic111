namespace Pra;

using System.Linq;
using Microsoft.Maui.Storage;

public partial class Povtorenie : ContentPage
{
    private List<WordItem> _deck = new(); // колода карточек на эту сессию
    private int _index = 0; // номер текущей карточки
    private int _known = 0; // сколько слов отметили как "знаю"
    private int _unknown = 0; // сколько "не знаю"
    private bool _answerShown = false; // показан ли перевод текущей карточки

    public Povtorenie()
	{
		InitializeComponent();
        StartSession(); // готовит колоду и показывает первую карточку
    }
    private void StartSession()
    {

        // слава берутся из словаря, если их нет то есть запасной набор слов
        var source = Slovar.Words.Count > 0 ? Slovar.Words.ToList() : FallbackWords();

        // тут перемешиваются, каждой карточке присваивается код 
        // список сортируется по этому коду
        _deck = source.OrderBy(_ => Guid.NewGuid()).ToList();

        // сбрасываются счётчики
        _index = 0;
        _known = 0;
        _unknown = 0; 

        SummaryPanel.IsVisible = false; // прячем панель итогов
        CardBorder.IsVisible = true; // показываем карточку
        ProgressPanel.IsVisible = true; // показываем прогресс

        ShowCurrentCard(); // выводит первое слово
    }

    private void ShowCurrentCard()
    {
        _answerShown = false; // новый показ -> перевод скрыт
        var word = _deck[_index]; // берем слово по текущему индексу

        WordLabel.Text = word.Word;
        TranscriptionLabel.Text = word.Transcription ?? ""; // если транскрипция пустая, то подставит пустую строку
        TranslationLabel.Text = word.Translation ?? "";
        ExampleLabel.Text = string.IsNullOrWhiteSpace(word.Example) ? "" : $"«{word.Example}»"; // если примера нет, то пусто, если есть то кавычки

        AnswerPanel.IsVisible = false; // прячем перевод
        HintLabel.IsVisible = true; // подсказка
        ShowAnswerButton.IsVisible = true; // кнопка показать перевод видна
        KnowOrDont.IsVisible = false; // кнопки знажю/не знаю скртыты

        ProgressLabel.Text = $"Карточка {_index + 1} / {_deck.Count}"; // это типа насчет заголовка полоски, например "карточки 3/8"
        ScoreLabel.Text = $"✅ {_known}    🔁 {_unknown}";
        SessionProgress.Progress = (double)_index / _deck.Count; // заполнение полоски
    }

    


    private void ShowAnswerButton_Clicked(object sender, EventArgs e) // показать перевод
    {
        if (_answerShown) return; // если перевод уже показан то ничего
        _answerShown = true; 

        AnswerPanel.IsVisible = true; // показывем перевод
        HintLabel.IsVisible = false; // убираем подсказку
        ShowAnswerButton.IsVisible = false; // прячем покзать перевод
        KnowOrDont.IsVisible = true; // показываем знаю/не знаю
    }

    private void OnKnowClicked(object sender, EventArgs e)
    {
        // увеличить счётчик на 1 потом следующая карточка
        _known++;
        NextCard();
    }

    private void OnDontKnowClicked(object sender, EventArgs e)
    {
        // увеличить счётчик на 1 потом следующая карточка
        _unknown++;
        NextCard();
    }

    private void NextCard()
    {
        _index++; //следующий индекс
        if (_index >= _deck.Count) // если картчоки кончились
            ShowSummary(); // показать итог
        else
            ShowCurrentCard(); // иначе другое слово
    }

    private void ShowSummary()
    {
        SessionProgress.Progress = 1; // полоска заполнена
        ProgressLabel.Text = $"Карточка {_deck.Count} / {_deck.Count}";
        ScoreLabel.Text = $"✅ {_known}    🔁 {_unknown}";

        CardBorder.IsVisible = false; // прячем карточку и кнопки
        ShowAnswerButton.IsVisible = false;
        KnowOrDont.IsVisible = false;

        SummaryLabel.Text = $"Знаешь: {_known}    ·    Повторить: {_unknown}";
        SummaryPanel.IsVisible = true; // показываем панель итогов

        // сохраняем прогресс 
        int totalReviewed = Preferences.Get("WordsReviewed", 0) + _known;
        Preferences.Set("WordsReviewed", totalReviewed);
        Preferences.Set("LastReviewKnown", _known);
        Preferences.Set("LastReviewTotal", _deck.Count);
    }

    private void OnRestartClicked(object sender, EventArgs e) //начать заново
    {
        StartSession();
    }

    private List<WordItem> FallbackWords() => new() // словарь запасных слов
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

    // нижнее меню
    private async void OnUrokTapped(object sender, EventArgs e) => await Navigation.PushAsync(new NewPage1());
    private async void OnProfileTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Profile());
    private async void OnStaticTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Stats());
    private async void OnSlovarTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new Slovar());
    private async void OnDostigTapped(object sender, TappedEventArgs e) => await Navigation.PushAsync(new NewPage2());
}