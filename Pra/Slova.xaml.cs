namespace Pra;

using System.Collections.ObjectModel;

public partial class Slova : ContentPage
{
    public ObservableCollection<WordItem> Words { get; set; } = new();

    private List<WordItem> wordsList;
    private int currentIndex = 0;

    public Slova()
    {
        InitializeComponent();

        wordsList = new List<WordItem>();
        LoadWords();

        if (wordsList.Count > 0)
        {
            LoadWord();
        }
    }

    private void LoadWords()
    {
        wordsList.Add(new WordItem
        {
            Word = "Apple",
            Translation = "Яблоко",
            Transcription = "[ˈæpl]",
            Example = "I eat an apple every day.",
            Category = "Еда"
        });

        wordsList.Add(new WordItem
        {
            Word = "Airport",
            Translation = "Аэропорт",
            Transcription = "[ˈeəpɔːt]",
            Example = "The airport is very busy.",
            Category = "Путешествия"
        });

        wordsList.Add(new WordItem
        {
            Word = "Book",
            Translation = "Книга",
            Transcription = "[bʊk]",
            Example = "I read a book every evening.",
            Category = "Учеба"
        }); wordsList.Add(new WordItem
        {
            Word = "Beautiful",
            Translation = "Красивый",
            Transcription = "[ˈbjuːtɪfl]",
            Example = "She has a beautiful smile.",
            Category = "Прилагательные"
        });

        wordsList.Add(new WordItem
        {
            Word = "Computer",
            Translation = "Компьютер",
            Transcription = "[kəmˈpjuːtə]",
            Example = "I work on my computer every day.",
            Category = "Техника"
        });

        wordsList.Add(new WordItem
        {
            Word = "Delicious",
            Translation = "Вкусный",
            Transcription = "[dɪˈlɪʃəs]",
            Example = "This pizza is delicious!",
            Category = "Еда"
        });

        wordsList.Add(new WordItem
        {
            Word = "Experience",
            Translation = "Опыт",
            Transcription = "[ɪkˈspɪəriəns]",
            Example = "I have experience in programming.",
            Category = "Работа"
        });

        wordsList.Add(new WordItem
        {
            Word = "Friend",
            Translation = "Друг",
            Transcription = "[frend]",
            Example = "He is my best friend.",
            Category = "Люди"
        });

        wordsList.Add(new WordItem
        {
            Word = "Garden",
            Translation = "Сад",
            Transcription = "[ˈɡɑːdn]",
            Example = "There are roses in the garden.",
            Category = "Природа"
        });

        wordsList.Add(new WordItem
        {
            Word = "Happy",
            Translation = "Счастливый",
            Transcription = "[ˈhæpi]",
            Example = "I am happy to see you!",
            Category = "Эмоции"
        });



        foreach (var word in wordsList)
        {
            Words.Add(word);
        }
    }

    private void LoadWord()
    {
        if (wordsList.Count == 0 || currentIndex >= wordsList.Count)
            return;

        var currentWord = wordsList[currentIndex];

     
        WordLabel.Text = currentWord.Word;
        TranslationLabel.Text = currentWord.Translation;
        TranscriptionLabel.Text = currentWord.Transcription;
        ExampleLabel.Text = currentWord.Example;
        

        
        TranslationLabel.IsVisible = false;
        ExampleLabel.IsVisible = false;
        ShowButton.IsVisible = true;

        
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        if (wordsList.Count == 0)
            return;

        
        ProgressLabel.Text = $"{currentIndex + 1} из {wordsList.Count}";

        
        LessonProgressBar.Progress = (double)(currentIndex + 1) / wordsList.Count;
    }

    private void OnShowClicked(object sender, EventArgs e)
    {
        TranslationLabel.IsVisible = true;
        ExampleLabel.IsVisible = true;
        ShowButton.IsVisible = false;
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        if (currentIndex < wordsList.Count - 1)
        {
            currentIndex++;
            LoadWord();
        }
        else
        {
            DisplayAlert("Конец", "Вы просмотрели все слова", "OK");
        }
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            LoadWord();
        }
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