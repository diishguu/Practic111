namespace Pra;

using Microsoft.Maui.Controls.Shapes;

public partial class exercises : ContentPage
{
	public exercises()
	{
		InitializeComponent();
        // берем цвета из app.xaml
        _dark = (Color)Application.Current.Resources["Dark"];
        _lightgreen = (Color)Application.Current.Resources["LightGreen"];
        _acidgreen = (Color)Application.Current.Resources["AcidGreen"];
        _cream = (Color)Application.Current.Resources["Cream"];

        BuildQuestions(); // заполние списка вопросов 
        LoadQuestions(); // показ первого вопроса
	}

    // один вопрос это подсказка, верный ответяснения, слова ловушки
    class Question
    {
        public string Prompt { get; } // это русское предложение
        public string Answer { get; } // это правильный английскйи ответ
        public string Explanation { get; } // это объяснение грамматики 
        public List<string> Pool { get; } // это все слова что используются для ответа
        public Question(  string prompt, string answer, string explanation, params string[] distractors)
        {
                Prompt = prompt;
                Answer = answer;
                Explanation = explanation;
                var words = answer.Split(' ').ToList(); // разбивание ответа на слова
                words.AddRange(distractors); // добавление слов ловушек
                Pool = words;
        }
        
    }
        readonly List<Question> _questions = new(); // все вопросы
        int _index; // индекс текущего вопросв
        int _score; // счетчик верных ответов
        bool _answered; // проверка нажата ли кнопка ответа

    // цвета из реесурсов
    readonly Color _dark; 
    readonly Color _lightgreen;
    readonly Color _acidgreen;
    readonly Color _cream;

    // наполнение вопросов 
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
        var q = _questions[_index]; // текущий вопрос
        PromptLabel.Text = q.Prompt; // русская фраза
        CounterLabel.Text = $"{_index + 1} / {_questions.Count}"; // тут типа показ счетчика карточек
        TestProgress.Progress = (double)_index / _questions.Count;

        AnswerArea.Children.Clear(); // чистим зону ответа
        WordsKeep.Children.Clear(); // чистим хранение слов

        // слова в хранении в случайном порядке
        foreach (var w in q.Pool.OrderBy(_ => Guid.NewGuid()))
            WordsKeep.Children.Add(MakeChip(w)); // создание слова на которое можно тапнуть и собрать ответ

        CheckButton.IsEnabled = false; // если ответ не собран кнопка неактивна
    }

    // здес сам метод создания слова на которое можно тапнуть и собрать ответ
    Border MakeChip(string word)
    {
        // текст слова
        var label = new Label
        {
            Text = word,
            FontSize = 16,
            TextColor = _dark,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        // его рамка
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
        // обработчик тапа
        var tap = new TapGestureRecognizer();
        tap.Tapped += OnChipTapped;
        chip.GestureRecognizers.Add(tap);
        return chip;
    }

    // перенос слова в зону ответа и из зоны ответа
    void OnChipTapped(object sender, TappedEventArgs e)
    {
        if (_answered) return;   // после проверки слова двигать нельзя
        var chip = (Border)sender; // по какому именно слову тапнули

        // если это слово хранится 
        if (WordsKeep.Children.Contains(chip))
        {
            WordsKeep.Children.Remove(chip); // убираем
            AnswerArea.Children.Add(chip); // и кладем в зону ответа
        }
        else // иначе она в зоне ответа 
        {
            AnswerArea.Children.Remove(chip); // возвращаем из зоны ответа
            WordsKeep.Children.Add(chip); // в зону хранения
        }

        CheckButton.IsEnabled = AnswerArea.Children.Count > 0; // кнопка активна если хоть одно слово собрано
    }

    private void CheckButton_Clicked(object sender, EventArgs e)
    {
        // овтет уже проверен идём к следующему 
        if (_answered)
        {
            _index++;
            // если индекс достигает большее число чем вопросов то показываем результат, то есть вопросы кончились
            if (_index >= _questions.Count) { ShowResult(); return; }
            ResetForNext();
            LoadQuestions();
            return;
        }

        // собираем предложение из слов в зоне ответа (по порядку)
        var assembled = string.Join(" ", AnswerArea.Children
            .OfType<Border>() // только слова из зоны ответа 
            .Select(b => ((Label)b.Content).Text)); // достаём текст из каждого

        var q = _questions[_index];
        bool isCorrect = string.Equals(assembled.Trim(), q.Answer.Trim(),
            StringComparison.OrdinalIgnoreCase); // сравниваем без учета регистра 

        _answered = true;
        if (isCorrect) _score++; // если верно то + балл
        TestProgress.Progress = (double)(_index + 1) / _questions.Count;

        // окошки если ответил верно и неверно
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
    // сброс перед новым вопросом
    void ResetForNext()
    {
        _answered = false; // снова можно отвечать 
        Banner.IsVisible = false; // прячем окошко
        Explain.IsVisible = false; // пряем объяснение
        CheckButton.Text = "Проверить"; 
        CheckButton.BackgroundColor = _lightgreen;
        CheckButton.IsEnabled = false;
    }
    // экран итогов
    void ShowResult()
    {
        QuizView.IsVisible = false; //прячем текст
        ResultView.IsVisible = true; // показ результата
        ResultScore.Text = $"{_score} / {_questions.Count}";
        int pct = (int)Math.Round(100.0 * _score / _questions.Count); // подсчет верных ответов в процентах
        ResultPercent.Text = $"{pct}% правильных ответов";
        ResultEmoji.Text = pct > 80 ? "🎉" : pct > 50 ? "👌" : "✨"; // эмодзи по результату
    }
    // кнопка протйти заново
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