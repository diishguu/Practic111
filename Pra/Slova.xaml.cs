namespace Pra;

using static WordItem;

public partial class Slova : ContentPage
{
    private int currentIndex = 0;
    private List<WordItem> words;

    public Slova()
	{
		InitializeComponent();

        words = Slovar.AllWords.ToList();

        LoadWord();
    }
    private void LoadWord()
    {
        if (words.Count == 0)
            return;

        var word = words[currentIndex];

        WordLabel.Text = word.Word;
        TranslationLabel.Text = word.Translation;
        TranscriptionLabel.Text = word.Transcription;
        ExampleLabel.Text = word.Example;

        TranslationLabel.IsVisible = false;
        ExampleLabel.IsVisible = false;

        ShowButton.IsVisible = true;

        ProgressLabel.Text = $"{currentIndex + 1} из {words.Count}";
        LessonProgressBar.Progress = (double)(currentIndex + 1) / words.Count;
    }

    private void OnShowClicked(object sender, EventArgs e)
    {
        TranslationLabel.IsVisible = true;
        ExampleLabel.IsVisible = true;
        ShowButton.IsVisible = false;
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        if (currentIndex >= words.Count - 1)
            return;

        currentIndex++;
        LoadWord();
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        if (currentIndex <= 0)
            return;

        currentIndex--;
        LoadWord();
    }
}
