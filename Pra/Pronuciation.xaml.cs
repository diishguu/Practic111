namespace Pra;
using static PronItem;

public partial class Pronuciation : ContentPage
{
    private int currentIndex = 0;
    private readonly List<PronItem> sounds = new()
    {
        new()
        {
            Sound = "/æ/",
            Description = "Краткий звук, похожий на русское «э», но рот открывается шире.",
            Example = "cat, apple, black"
        },

        new()
        {
            Sound = "/ɪ/",
            Description = "Краткий звук между русскими «и» и «ы».",
            Example = "sit, big, milk"
        },

        new()
        {
            Sound = "/iː/",
            Description = "Долгий звук «ии».",
            Example = "see, green, teacher"
        },

        new()
        {
            Sound = "/ʌ/",
            Description = "Краткий звук, похожий на русское «а».",
            Example = "cup, bus, sun"
        },

        new()
        {
            Sound = "/θ/",
            Description = "Кончик языка между зубами, глухой звук.",
            Example = "think, thank, three"
        }
    };
    public Pronuciation()
	{
		InitializeComponent();
        LoadSound();
    }
    private void LoadSound()
    {
        var sound = sounds[currentIndex];

        SoundLabel.Text = sound.Sound;
        DescriptionLabel.Text = sound.Description;
        ExampleLabel.Text = sound.Example;

        ProgressLabel.Text = $"{currentIndex + 1} из {sounds.Count}";
        PronunciationProgressBar.Progress =
            (double)(currentIndex + 1) / sounds.Count;
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        if (currentIndex >= sounds.Count - 1)
            return;

        currentIndex++;
        LoadSound();
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        if (currentIndex <= 0)
            return;

        currentIndex--;
        LoadSound();
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        await TextToSpeech.Default.SpeakAsync("apple");
        await DisplayAlert(
            "Прослушивание",
            "Воспроизведение аудио будет добавлено позже.",
            "OK");
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

}
