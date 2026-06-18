namespace Pra;

public partial class Rules : ContentPage
{
    private int currentIndex = 0;
    private readonly List<GrammarRule> rules = new()
    {
        new()
        {
            Title = "Артикль a / an",
            Description = "Артикли a и an используются перед исчисляемыми существительными в единственном числе. An используется перед гласным звуком.",
            Example = "a book, a car, an apple, an orange"
        },

        new()
        {
            Title = "Глагол to be",
            Description = "Формы am, is, are используются для описания состояния, возраста, профессии и местоположения.",
            Example = "I am a student. She is happy. They are at home."
        },

        new()
        {
            Title = "Present Simple",
            Description = "Используется для регулярных действий, привычек и общеизвестных фактов.",
            Example = "She goes to school every day."
        },

        new()
        {
            Title = "Present Continuous",
            Description = "Используется для действий, происходящих прямо сейчас.",
            Example = "I am reading a book now."
        },

        new()
        {
            Title = "Past Simple",
            Description = "Используется для действий, завершившихся в прошлом.",
            Example = "We visited London last year."
        }
    };
    public Rules()
	{
		InitializeComponent();
        LoadRule();
    }
    private void LoadRule()
    {
        var rule = rules[currentIndex];

        TitleLabel.Text = rule.Title;
        DescriptionLabel.Text = rule.Description;
        ExampleLabel.Text = rule.Example;

        ProgressLabel.Text = $"{currentIndex + 1} из {rules.Count}";
        RuleProgressBar.Progress = (double)(currentIndex + 1) / rules.Count;
    }

    private void OnNextClicked(object sender, EventArgs e)
    {
        if (currentIndex >= rules.Count - 1)
            return;

        currentIndex++;
        LoadRule();
    }

    private void OnPreviousClicked(object sender, EventArgs e)
    {
        if (currentIndex <= 0)
            return;

        currentIndex--;
        LoadRule();
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