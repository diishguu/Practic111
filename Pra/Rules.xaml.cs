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
        },
        new()
        {
            Title = "Артикль the",
            Description = "Артикль the используется, когда речь идёт о конкретном предмете, человеке или явлении, известном собеседникам.",
            Example = "the sun, the book on the table, the teacher"
        },
        new()
        {
            Title = "Множественное число существительных",
            Description = "Большинство существительных образуют множественное число с помощью окончания -s или -es.",
            Example = "book → books, box → boxes, bus → buses"
        },
        new()
        {
            Title = "Глагол to be",
            Description = "Глагол to be используется для описания состояния, профессии, возраста и местоположения. В настоящем времени имеет формы am, is и are.",
            Example = "I am a student. She is happy. They are at home."
        },
        new()
        {
            Title = "Местоимения",
            Description = "Личные местоимения заменяют существительные и помогают избежать повторений.",       
            Example = "I, you, he, she, it, we, they"
        },
        new()
        {
            Title = "Present Simple",
            Description = "Present Simple используется для описания привычек, регулярных действий и общеизвестных фактов.",
            Example = "I work every day. She likes coffee. The Earth goes around the Sun."
        },
        new()
        {
            Title = "Present Continuous",
            Description = "Present Continuous используется для действий, происходящих в момент речи или в текущий период времени.",
            Example = "I am reading a book. They are playing football."
        },
        new()
        {
            Title = "Притяжательные местоимения",
            Description = "Притяжательные местоимения показывают принадлежность предмета или человека.",
            Example = "my book, your phone, his car, our house"
        },
        new()
        {
            Title = "Предлоги места",
            Description = "Предлоги места помогают описать расположение предметов в пространстве.",
            Example = "on the table, under the chair, in the bag, next to the window"
        },
        new()
        {
            Title = "Предлоги времени",
            Description = "Предлоги in, on и at используются для обозначения времени.",
            Example = "in July, on Monday, at 7 o'clock"
        },
        new()
        {
            Title = "Модальный глагол can",
            Description = "Глагол can используется для выражения умения, возможности или просьбы.",
            Example = "I can swim. Can you help me?"
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
    private async void OnPovtorTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Povtorenie());
    }
}