namespace Pra;

using Microsoft.Maui.Controls.Shapes;

public partial class exercises : ContentPage
{
	public exercises()
	{
		InitializeComponent();

        _dark = (Color)Application.Current.Resources["Dark"];
        _lightgreen = (Color)Application.Current.Resources["LightGreen"];
        _acidgreen = (Color)Application.Current.Resources["AcidGreen"];
        _cream = (Color)Application.Current.Resources["Cream"];

        BuildQuestions();
        LoadQuestions();
	}

    // один вопрос это подсказка, верный ответяснения, слова ловушки
    class Question
    {
        public string Prompt { get; }
        public string Answer { get; }
        public string Explanation { get; }
        public List<string> Pool { get; }
        public Question(  string prompt, string answer, string explanation, params string[] distractors)
        {
                Prompt = prompt;
                Answer = answer;
                Explanation = explanation;
                var words = answer.Split(' ').ToList(); // слова правильного ответа
                words.AddRange(distractors); // + лишние слова ловушки
                Pool = words;
        }
        
    }
        readonly List<Question> _questions = new();
        int _index; // индекс текущего вопросв
        int _score; // счетчик верных ответов
        bool _answered; // проверка нажата ли кнопка ответа

    readonly Color _dark;
    readonly Color _lightgreen;
    readonly Color _acidgreen;
    readonly Color _cream;

    void BuildQuestions()
    {
        _questions.Add(new Question("Я люблю читать книги", "I like to read books",
            "После 'like' идёт инфинитив с 'to': like to read.", "want", "the"));
        _questions.Add(new Question("Она пьёт кофе утром", "She drinks coffee in the morning",
            "3-е лицо ед. числа: she drinks (+s). 'Утром' = in the morning.", "tea", "at"));
        _questions.Add(new Question("Мы живём в большом городе", "We live in a big city",
            "Перед 'прилагательное + существительное' нужен артикль: a big city.", "small", "house"));
        _questions.Add(new Question("Он играет в футбол по выходным", "He plays football on weekends",
            "Названия видов спорта без артикля: play football. 'По выходным' = on weekends.", "tennis", "in"));
        _questions.Add(new Question("Кошка спит на диване", "The cat sleeps on the sofa",
            "'На поверхности' = on. Конкретный предмет → the sofa.", "dog", "under"));
        _questions.Add(new Question("Я хочу выучить английский язык", "I want to learn English",
            "После 'want' — инфинитив с 'to'. Названия языков с большой буквы: English.", "teach", "French"));
        _questions.Add(new Question("Они смотрят фильм вечером", "They watch a movie in the evening",
            "Один фильм → a movie. 'Вечером' = in the evening.", "morning", "read"));
        _questions.Add(new Question("Моя сестра работает в больнице", "My sister works in a hospital",
            "3-е лицо ед. числа: works (+s). 'В больнице' = in a hospital.", "school", "brother"));
        _questions.Add(new Question("Дети играют в парке", "The children play in the park",
            "Children — мн. число от child, глагол без -s: children play.", "garden", "at"));
        _questions.Add(new Question("Я завтракаю в восемь часов", "I have breakfast at eight o'clock",
            "'Завтракать' = have breakfast (без артикля). Точное время → at: at eight.", "dinner", "nine"));
        _questions.Add(new Question("Он водит машину очень быстро", "He drives the car very fast",
            "Fast не меняется — это и прилагательное, и наречие. drives (+s).", "slow", "bike"));
        _questions.Add(new Question("Мы учим новые слова каждый день", "We learn new words every day",
            "'Каждый день' = every day (пишется раздельно).", "old", "week"));
        _questions.Add(new Question("Она поёт красивую песню", "She sings a beautiful song",
            "3-е лицо ед. числа: sings (+s). Артикль 'a' перед beautiful.", "ugly", "dances"));
        _questions.Add(new Question("Я пью воду после тренировки", "I drink water after training",
            "'После' = after. water и training неисчисляемые — без артикля.", "before", "juice"));
        _questions.Add(new Question("Птицы летают высоко в небе", "Birds fly high in the sky",
            "High = высоко (наречие). Небо всегда с the: the sky.", "low", "swim"));
        _questions.Add(new Question("Мой друг живёт рядом со мной", "My friend lives near me",
            "3-е лицо ед. числа: lives (+s). После предлога — местоимение me.", "far", "enemy"));
        _questions.Add(new Question("Учитель объясняет урок", "The teacher explains the lesson",
            "3-е лицо ед. числа: explains (+s). Конкретные предметы → the.", "student", "asks"));
        _questions.Add(new Question("Я люблю гулять по утрам", "I like to walk in the mornings",
            "'По утрам' (регулярно) = in the mornings, мн. число. После 'like' — to walk.", "run", "evenings"));
        _questions.Add(new Question("Мы готовим ужин вместе", "We cook dinner together",
            "'Ужин' = dinner без артикля. 'Вместе' = together.", "alone", "breakfast"));
        _questions.Add(new Question("Зимой часто идёт снег", "It often snows in winter",
            "Безличное it + snows (+s). 'Часто' (often) ставится перед глаголом.", "summer", "rains"));
    }

    void LoadQuestions()
    {
        var q = _questions[_index];
        PromptLabel.Text = q.Prompt;
        CounterLabel.Text = $"{_index + 1} / {_questions.Count}";
        TestProgress.Progress = (double)_index / _questions.Count;

        AnswerArea.Children.Clear();
        WordsKeep.Children.Clear();

        // слова в хранении в случайном порядке
        foreach (var w in q.Pool.OrderBy(_ => Guid.NewGuid()))
            WordsKeep.Children.Add(MakeChip(w));

        CheckButton.IsEnabled = false;
    }

    Border MakeChip(string word)
    {
        var label = new Label
        {
            Text = word,
            FontSize = 16,
            TextColor = _dark,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        var chip = new Border
        {
            Content = label,
            BackgroundColor = Colors.White,
            Stroke = Color.FromArgb("#33383939"),
            StrokeThickness = 1,
            StrokeShape = new RoundRectangle { CornerRadius = 12 },
            Padding = new Thickness(14, 10),
            Margin = new Thickness(4)
        };
        var tap = new TapGestureRecognizer();
        tap.Tapped += OnChipTapped;
        chip.GestureRecognizers.Add(tap);
        return chip;
    }

    // тап по слову: хранение слов <=> зона ответа
    void OnChipTapped(object sender, TappedEventArgs e)
    {
        if (_answered) return;   // после проверки слова не двигаем
        var chip = (Border)sender;

        if (WordsKeep.Children.Contains(chip))
        {
            WordsKeep.Children.Remove(chip);
            AnswerArea.Children.Add(chip);
        }
        else
        {
            AnswerArea.Children.Remove(chip);
            WordsKeep.Children.Add(chip);
        }

        CheckButton.IsEnabled = AnswerArea.Children.Count > 0;
    }

    private void CheckButton_Clicked(object sender, EventArgs e)
    {
        // вторая фаза: ответ уже проверен — переходим к следующему вопросу
        if (_answered)
        {
            _index++;
            if (_index >= _questions.Count) { ShowResult(); return; }
            ResetForNext();
            LoadQuestions();
            return;
        }

        // собираем предложение из слов в зоне ответа (по порядку)
        var assembled = string.Join(" ", AnswerArea.Children
            .OfType<Border>()
            .Select(b => ((Label)b.Content).Text));

        var q = _questions[_index];
        bool isCorrect = string.Equals(assembled.Trim(), q.Answer.Trim(),
            StringComparison.OrdinalIgnoreCase);

        _answered = true;
        if (isCorrect) _score++;
        TestProgress.Progress = (double)(_index + 1) / _questions.Count;

        Banner.IsVisible = true;
        if (isCorrect)
        {
            Banner.BackgroundColor = _lightgreen;
            BannerTitle.Text = "Верно!";
            BannerTitle.TextColor = _dark;
            Detail.IsVisible = false;
            Explain.TextColor = _dark;
            CheckButton.BackgroundColor = _lightgreen;
            CheckButton.TextColor = _dark;
        }
        else
        {
            Banner.BackgroundColor = _dark;
            BannerTitle.Text = "Правильный ответ:";
            BannerTitle.TextColor = _cream;
            Detail.Text = q.Answer;
            Detail.TextColor = _acidgreen;
            Detail.IsVisible = true;
            Explain.TextColor = _cream;
            CheckButton.BackgroundColor = _dark;
            CheckButton.TextColor = _cream;
        }

        // объяснение показываем в любом случае
        Explain.Text = "💡 " + q.Explanation;
        Explain.IsVisible = true;

        CheckButton.Text = "Продолжить";
        CheckButton.IsEnabled = true;
    }
    void ResetForNext()
    {
        _answered = false;
        Banner.IsVisible = false;
        Explain.IsVisible = false;
        CheckButton.Text = "Проверить";
        CheckButton.BackgroundColor = _lightgreen;
        CheckButton.IsEnabled = false;
    }

    void ShowResult()
    {
        QuizView.IsVisible = false;
        ResultView.IsVisible = true;
        ResultScore.Text = $"{_score} / {_questions.Count}";
        int pct = (int)Math.Round(100.0 * _score / _questions.Count);
        ResultPercent.Text = $"{pct}% правильных ответов";
        ResultEmoji.Text = pct > 80 ? "🎉" : pct > 50 ? "👌" : "✨";
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        _index = 0;
        _score  = 0;
        ResetForNext();
        ResultView.IsVisible = false;
        QuizView.IsVisible = true;
        LoadQuestions();
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